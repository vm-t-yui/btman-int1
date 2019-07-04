namespace NendUnityPlugin.AD.Native.Handlers
{
	using System;
	using UnityEngine;

	using Log = NendUnityPlugin.Common.NendAdLogger;

//	[RequireComponent (typeof (Renderer))]
//	[RequireComponent (typeof (Collider))]
//	[RequireComponent (typeof (Collider2D))]
	internal class ImpressionHandlerGO : ImpressionHandler
	{
		protected override bool IsViewable ()
		{
			var camera = Camera.main;
			var position = camera.WorldToScreenPoint (gameObject.transform.position);
			Log.D ("WorldToScreenPoint: " + position);
			try {
				var bounds = GetBounds ();
				Rect rect = GetObjectRect (bounds);
				Log.D ("The rect of {0}: " + rect, gameObject.name);

				var left = position.x - rect.width / 2;
				var right = position.x + rect.width / 2;
				var top = position.y + rect.height / 2;
				var bottom = position.y - rect.height / 2;
				return CheckViewablePercentage (new Corner (left, top, right, bottom));		
			} catch (InvalidOperationException e) {
				Log.W (e.Message);
				Destroy (this);
			}
			return false;
		}

		private Bounds GetBounds ()
		{ 
			var render = gameObject.GetComponent <Renderer> ();
			if (null != render) {
				return render.bounds;
			}
			var collider = gameObject.GetComponent <Collider> ();
			if (null != collider) {
				return collider.bounds;
			}
			var collider2D = gameObject.GetComponent <Collider2D> ();
			if (null != collider2D) {
				return collider2D.bounds;
			}
			throw new InvalidOperationException ("Couldn't get bounds of '" + gameObject.name + "'.");
		}

		private static Rect GetObjectRect (Bounds bounds)
		{
			Vector3 cen = bounds.center;
			Vector3 ext = bounds.extents;
			Vector2[] extentPoints = new Vector2[8] {
				WorldToScreenPoint (new Vector3 (cen.x - ext.x, cen.y - ext.y, cen.z - ext.z)),
				WorldToScreenPoint (new Vector3 (cen.x + ext.x, cen.y - ext.y, cen.z - ext.z)),
				WorldToScreenPoint (new Vector3 (cen.x - ext.x, cen.y - ext.y, cen.z + ext.z)),
				WorldToScreenPoint (new Vector3 (cen.x + ext.x, cen.y - ext.y, cen.z + ext.z)),
				WorldToScreenPoint (new Vector3 (cen.x - ext.x, cen.y + ext.y, cen.z - ext.z)),
				WorldToScreenPoint (new Vector3 (cen.x + ext.x, cen.y + ext.y, cen.z - ext.z)),
				WorldToScreenPoint (new Vector3 (cen.x - ext.x, cen.y + ext.y, cen.z + ext.z)),
				WorldToScreenPoint (new Vector3 (cen.x + ext.x, cen.y + ext.y, cen.z + ext.z))
			};
			Vector2 min = extentPoints [0];
			Vector2 max = extentPoints [0];
			foreach (Vector2 v in extentPoints) {
				min = Vector2.Min (min, v);
				max = Vector2.Max (max, v);
			}
			return new Rect (min.x, min.y, max.x - min.x, max.y - min.y);
		}

		private static Vector2 WorldToScreenPoint (Vector3 world)
		{
			Vector2 screenPoint = Camera.main.WorldToScreenPoint (world);
			screenPoint.y = (float)Screen.height - screenPoint.y;
			return screenPoint;
		}
	}
}
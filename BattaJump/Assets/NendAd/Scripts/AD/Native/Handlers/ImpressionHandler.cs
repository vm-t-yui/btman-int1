namespace NendUnityPlugin.AD.Native.Handlers
{
	using System;
	using UnityEngine;

	using Log = NendUnityPlugin.Common.NendAdLogger;

	internal abstract class ImpressionHandler : MonoBehaviour
	{
		protected struct Corner
		{
			internal float left;
			internal float top;
			internal float right;
			internal float bottom;

			internal Corner (float left, float top, float right, float bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}
		}
			
		private const float VIEWABLE_PERCENTAGE = .5f;
		private const float CHECK_INTERVAL = 1.0f;
		private float m_TimeElapsed = .0f;

		internal Action OnImpression;

		protected abstract bool IsViewable ();

		void Update ()
		{
			m_TimeElapsed += Time.deltaTime;
			if (m_TimeElapsed >= CHECK_INTERVAL) {
				CheckImpression ();
				m_TimeElapsed = .0f;
			}
		}

		void OnDestroy ()
		{
			Log.D ("ImpressionHandler#OnDestroy: {0}", gameObject.name);
		}

		private void CheckImpression ()
		{
			Log.I ("Checking whether the {0} is viewable.", gameObject.name);
			Vector3 cameraRelative = Camera.main.transform.InverseTransformPoint (gameObject.transform.position);
			if (0 < cameraRelative.z) {
				Log.D ("The {0} is in front of the camera.", gameObject.name);
			} else {
				Log.D ("The {0} is behind the camera.", gameObject.name);
				return;
			}
			if (IsViewable ()) {
				Log.I ("The {0} is viewable!", gameObject.name);
				OnImpression ();
			}
		}

		protected bool CheckViewablePercentage (Corner adCorner)
		{
			Rect screenSize = Camera.main.pixelRect;
			Log.D ("screenSize: {0}, left: {1}, top: {2}, right: {3}, bottom: {4}",
				screenSize, adCorner.left, adCorner.top, adCorner.right, adCorner.bottom);

			if (.0f > adCorner.right || screenSize.height < adCorner.bottom || screenSize.width < adCorner.left || .0f > adCorner.top) {
				Log.D ("{0} is out of the screen.", gameObject.name);
				return false;
			}

			var intersectWidth = 0.0f;
			var intersectHeight = 0.0f;

			if (.0f > adCorner.left && screenSize.width < adCorner.right) {
				Log.D ("{0}'s width is larger than width of camera.", gameObject.name);
				intersectWidth = screenSize.width;
			} else if (.0f > adCorner.left) {
				Log.D ("Left side of {0} is sticking out.", gameObject.name);
				intersectWidth = adCorner.right;
			} else if (screenSize.width < adCorner.right) {
				Log.D ("Right side of {0} is sticking out.", gameObject.name);
				intersectWidth = screenSize.width - adCorner.left;
			} else {
				intersectWidth = adCorner.right - adCorner.left;
			}

			if (.0f > adCorner.bottom && screenSize.height < adCorner.top) {
				Log.D ("{0}'s height is larger than height of camera.", gameObject.name);
				intersectHeight = screenSize.height;
			} else if (.0f > adCorner.bottom) {
				Log.D ("Bottom of {0} is sticking out.", gameObject.name);
				intersectHeight = adCorner.top;
			} else if (screenSize.height < adCorner.top) {
				Log.D ("Top of {0} is sticking out.", gameObject.name);
				intersectHeight = screenSize.height - adCorner.bottom;
			} else {
				intersectHeight = adCorner.top - adCorner.bottom;
			}

			var adWidth = adCorner.right - adCorner.left;
			var adHeight = adCorner.top - adCorner.bottom;
			float intersect = intersectWidth * intersectHeight;
			float adSize = adWidth * adHeight;
			Log.D ("IntersectArea: {0}, AdArea: {1}", intersect, adSize);

			return intersect >= adSize * VIEWABLE_PERCENTAGE;
		}
	}
}
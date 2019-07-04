namespace NendUnityPlugin.AD.Native.Handlers
{
	using System.Linq;
	using UnityEngine;

	using Log = NendUnityPlugin.Common.NendAdLogger;

	[RequireComponent (typeof (RectTransform))]
	internal class ImpressionHandlerUI : ImpressionHandler
	{
		private Camera m_Camera;
		private Canvas m_Canvas;
		private Vector3[] m_Corners = new Vector3[4];

		void Start ()
		{
			m_Canvas = GetComponentInParent<Canvas> ();
			m_Camera = m_Canvas.worldCamera;
			if (null == m_Camera) {
				m_Camera = Camera.main;
			}
		}

		protected override bool IsViewable ()
		{
			var rectTransform = gameObject.transform as RectTransform;
			if (((.0f >= rectTransform.sizeDelta.x || .0f >= rectTransform.sizeDelta.y) 
				&& (.0f >= rectTransform.rect.width || .0f >= rectTransform.rect.height))
				|| .0f >= rectTransform.localScale.x || .0f >= rectTransform.localScale.y) {
				Log.D ("{0}'s size or scale are less than or equal to zero.", gameObject.name);
				return false;
			}

			var group = rectTransform.GetComponent<CanvasGroup> ();
			if (null != group && .0f >= group.alpha) {
				Log.D ("{0}'s alpha is equal to zero.", gameObject.name);
				return false;
			}

			rectTransform.GetWorldCorners (m_Corners);
			Log.D ("WorldCorners: {0}, {1}, {2}, {3}, camera: {4}", m_Corners [0], m_Corners [1], m_Corners [2], m_Corners [3], m_Camera);
			Camera camera = (null != m_Canvas && RenderMode.ScreenSpaceOverlay == m_Canvas.renderMode) ? null : m_Camera;
			Vector2[] screenCorners = m_Corners.Select (v => RectTransformUtility.WorldToScreenPoint (camera, v)).ToArray ();
			Log.D ("ScreenCorners: {0}, {1}, {2}, {3}, camera: {4}", screenCorners [0], screenCorners [1], screenCorners [2], screenCorners [3], m_Camera);

			float left = Mathf.Min (screenCorners [0].x, screenCorners [1].x);
			float right = Mathf.Max (screenCorners [2].x, screenCorners [3].x);
			float top = Mathf.Max (screenCorners [1].y, screenCorners [2].y);
			float bottom = Mathf.Min (screenCorners [0].y, screenCorners [3].y);
			return CheckViewablePercentage (new Corner (left, top, right, bottom));		
		}
	}
}
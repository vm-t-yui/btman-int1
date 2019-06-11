namespace NendUnityPlugin.AD.Native.Handlers
{
	using UnityEngine;
	using UnityEngine.EventSystems;
	using System.Collections.Generic;

	internal abstract class ClickHandlerGO : ClickHandler
	{
		protected abstract bool IsClicked (Vector3 position);

		void Update ()
		{
			if (Input.GetMouseButtonUp (0)) {
				Vector3 position = Input.mousePosition;
				if (IsPointerOverUIObject (position)) {
					return;
				}
				if (IsClicked (position)) {
					CallbackClick ();
				}
			}
		}

		private static bool IsPointerOverUIObject (Vector3 position)
		{
			var pointer = new PointerEventData (EventSystem.current);
			pointer.position = position;
			var results = new List<RaycastResult> ();
			EventSystem.current.RaycastAll (pointer, results);
			return 0 < results.Count;
		}
	}
}
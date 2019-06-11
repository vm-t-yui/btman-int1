namespace NendUnityPlugin.AD.Native.Handlers
{
	using UnityEngine;

	[RequireComponent (typeof(Collider))]
	internal class ClickHandler3D : ClickHandlerGO
	{
		protected override bool IsClicked (Vector3 position)
		{
			Ray ray = Camera.main.ScreenPointToRay (position);
			var hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				if (gameObject == hit.collider.gameObject) {
					return true;
				}
			}
			return false;
		}
	}
}


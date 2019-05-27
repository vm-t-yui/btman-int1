namespace NendUnityPlugin.AD.Native.Handlers
{
	using UnityEngine;

	[RequireComponent (typeof(Collider2D))]
	internal class ClickHandler2D : ClickHandlerGO
	{
		protected override bool IsClicked (Vector3 position)
		{
			Ray ray = Camera.main.ScreenPointToRay (position);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction);
			return gameObject == hit.collider.gameObject;
		}
	}
}
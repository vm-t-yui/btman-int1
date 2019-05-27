namespace NendUnityPlugin.AD.Native.Handlers
{
	using UnityEngine.EventSystems;

	internal class ClickHandlerUI : ClickHandler, IPointerClickHandler
	{
		public void OnPointerClick (PointerEventData eventData)
		{
			CallbackClick ();
		}
	}
}
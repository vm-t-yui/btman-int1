namespace NendUnityPlugin.AD.Native.Handlers
{
	using System;
	using UnityEngine;

	using Log = NendUnityPlugin.Common.NendAdLogger;

	internal class ClickHandler : MonoBehaviour
	{
		internal Action OnClick;

		protected void CallbackClick ()
		{
			OnClick ();
		}

		void OnDestroy ()
		{
			Log.D ("ClickHandler#OnDestroy: {0}", gameObject.name);
		}
	}
}
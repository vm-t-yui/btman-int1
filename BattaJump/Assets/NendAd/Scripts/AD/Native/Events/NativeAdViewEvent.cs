namespace NendUnityPlugin.AD.Native.Events
{
	using System;
	using UnityEngine.Events;

	/// <summary>
	/// An event of NendAdNativeView.
	/// <param name="NendAdNativeView">An NendAdNativeView which event occured. </param>
	/// </summary>
	[Serializable]
	public class NativeAdViewEvent : UnityEvent <NendAdNativeView>
	{

	}
}


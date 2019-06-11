namespace NendUnityPlugin.AD.Native.Events
{
	using System;
	using UnityEngine.Events;

	/// <summary>
	/// An event of NendAdNativeView.
	/// <param name="NendAdNativeView">An NendAdNativeView which event occured.</param>
	/// <param name="int">An error code.</param>
	/// <param name="string">An error message.</param>
	/// </summary>
	[Serializable]
	public class NativeAdViewFailedToLoadEvent : UnityEvent <NendAdNativeView, int, string>
	{

	}
}


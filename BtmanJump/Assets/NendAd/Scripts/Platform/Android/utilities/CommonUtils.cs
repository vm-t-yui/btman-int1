#if UNITY_ANDROID
namespace NendUnityPlugin.Platform.Android
{
	using System;
	using UnityEngine;

	internal class CommonUtils
	{
		private const string UnityPlayerClassName = "com.unity3d.player.UnityPlayer";
		private AndroidJavaObject m_JavaObject;

		internal static AndroidJavaObject GetCurrentActivity ()
		{
			using (var unityPlayer = new AndroidJavaClass (UnityPlayerClassName)) {
				return unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
			}
		}
	}
}
#endif



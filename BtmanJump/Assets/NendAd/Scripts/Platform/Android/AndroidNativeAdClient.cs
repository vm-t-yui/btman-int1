#if UNITY_ANDROID
namespace NendUnityPlugin.Platform.Android
{
	using System;
	using UnityEngine;
	using System.Collections.Generic;
	using NendUnityPlugin.AD.Native;
	using Log = NendUnityPlugin.Common.NendAdLogger;

	internal class AndroidNativeAdClient : NativeAdClient
	{
		private const string NendAdNativeClientClassName = "net.nend.unity.plugin.NendUnityNativeAdClient";
		private const string UnityPlayerClassName = "com.unity3d.player.UnityPlayer";

		private AndroidJavaObject m_Client;

		internal AndroidNativeAdClient (string apiKey, string spotId) : base ()
		{
			using (var unityPlayer = new AndroidJavaClass (UnityPlayerClassName)) {
				using (var activity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity")) {
					m_Client = new AndroidJavaObject (NendAdNativeClientClassName, activity, int.Parse (spotId), apiKey);
					m_Client.Call ("setUnityAdListener", new Listener(onResponse));
				}
			}
		}

		~AndroidNativeAdClient ()
		{
			Dispose ();
		}

		public override void LoadNativeAd (Action<INativeAd, int, string> callback)
		{
			EnqueueCallback (callback);
			m_Client.Call ("loadAd");
		}

		public override void Dispose ()
		{
			base.Dispose ();
			Log.D ("Dispose AndroidNativeAdClient.");
			if (null != m_Client) {
				m_Client.Dispose ();
				m_Client = null;
			}
		}

		private void onResponse (AndroidJavaObject ad, int code, string message)
		{
			INativeAd nativeAd = null != ad ? new AndroidNativeAd (ad) : null;
			DeliverResponseOnMainThread (nativeAd, code, message);
		}

		private class Listener : AndroidJavaProxy {

			private const string NendAdNativeListenerClassName = "net.nend.unity.plugin.NendUnityNativeAdListener";

			private Action<AndroidJavaObject, int, string> m_Callback;

			internal Listener (Action<AndroidJavaObject, int, string> callback) : base (NendAdNativeListenerClassName)
			{
				m_Callback = callback;
			}

			void onResponse (AndroidJavaObject ad, int code, string message) 
			{
				m_Callback (ad, code, message);
			}
		}
	}
}
#endif
#if UNITY_ANDROID
namespace NendUnityPlugin.Platform.Android
{
	using System;
	using UnityEngine;
	using NendUnityPlugin.AD.FullBoard;

	internal class AndroidFullBoardAd : NendAdFullBoard
	{
		private const string NendAdFullBoardAdClassName = "net.nend.unity.plugin.NendUnityFullBoardAd";
		private const string UnityPlayerClassName = "com.unity3d.player.UnityPlayer";
		private AndroidJavaObject m_JavaObject;

		internal AndroidFullBoardAd (string spotId, string apiKey) : base ()
		{
			using (var unityPlayer = new AndroidJavaClass (UnityPlayerClassName)) {
				using (var activity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity")) {
					m_JavaObject = new AndroidJavaObject (NendAdFullBoardAdClassName, activity, int.Parse (spotId), apiKey);
				}
			}
		}

		internal override void LoadInternal ()
		{
			Listener listener = new Listener (onResponse);
			m_JavaObject.Call ("loadAd", listener);
		}

		internal override void ShowInternal ()
		{
			using (var unityPlayer = new AndroidJavaClass (UnityPlayerClassName)) {
				using (var activity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity")) {
					Listener listener = new Listener (onResponse);
					m_JavaObject.Call ("show", activity, listener);
				}
			}
		}

		private void onResponse (FullBoardAdCallbackType code)
		{
			CallBack (code);
		}

		private class Listener : AndroidJavaProxy
		{
			private const string NendAdFullBoardListenerClassName = "net.nend.unity.plugin.NendUnityFullBoardAdListener";
			private Action<FullBoardAdCallbackType> m_callback;

			internal Listener (Action<FullBoardAdCallbackType> callback) : base (NendAdFullBoardListenerClassName)
			{
				m_callback = callback;
			}

			void onLoaded ()
			{
				m_callback (FullBoardAdCallbackType.LoadSuccess);
				m_callback = null;
			}

			void onFailedToLoad (int code)
			{
				m_callback ((FullBoardAdCallbackType)code);
				m_callback = null;
			}

			void onShown ()
			{
				m_callback (FullBoardAdCallbackType.AdShown);
			}

			void onClicked ()
			{
				m_callback (FullBoardAdCallbackType.AdClicked);
			}

			void onDismissed ()
			{
				m_callback (FullBoardAdCallbackType.AdDismissed);
				m_callback = null;
			}
		}

		~AndroidFullBoardAd ()
		{
			Dispose ();
		}

		public override void Dispose ()
		{
			NendUnityPlugin.Common.NendAdLogger.D ("Dispose AndroidFullBoardAd.");
			if (null != m_JavaObject) {
				m_JavaObject.Dispose ();
				m_JavaObject = null;
			}
		}
	}
}
#endif


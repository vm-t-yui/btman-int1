#if UNITY_ANDROID
namespace NendUnityPlugin.Platform.Android
{
	using System;
	using UnityEngine;
	using NendUnityPlugin.AD.Video;
	using NendUnityPlugin.Platform.Android;

	internal class AndroidVideoAdListener : AndroidJavaProxy
	{
		private Action<AndroidInterstitialVideoAd.VideoAdCallbackType, object> m_callback;

		internal static AndroidVideoAdListener NewListener (Action<AndroidInterstitialVideoAd.VideoAdCallbackType, object> callback)
		{
			return new AndroidVideoAdListener (callback, "net.nend.unity.plugin.NendUnityVideoAdListener");
		}

		internal static AndroidVideoAdListener NewRewardedListener (Action<AndroidRewardedVideoAd.VideoAdCallbackType, object> callback)
		{
			return new AndroidVideoAdListener (callback, "net.nend.unity.plugin.NendUnityRewardedVideoAdListener");
		}

		public void ReleaseCallback ()
		{
			m_callback = null;
		}

		private AndroidVideoAdListener (Action<AndroidInterstitialVideoAd.VideoAdCallbackType, object> callback, string listenerClassName) : base (listenerClassName)
		{
			m_callback = callback;
		}

		void onLoaded ()
		{
			m_callback (AndroidInterstitialVideoAd.VideoAdCallbackType.LoadSuccess, null);
		}

		void onFailedToLoad (int errorCode)
		{
			m_callback (AndroidInterstitialVideoAd.VideoAdCallbackType.FailedToLoad, errorCode);
		}

		void onFailedToPlay ()
		{
			m_callback (AndroidInterstitialVideoAd.VideoAdCallbackType.FailedToPlay, null);
		}

		void onShown ()
		{
			m_callback (AndroidInterstitialVideoAd.VideoAdCallbackType.AdShown, null);
		}

		void onStarted ()
		{
			m_callback (AndroidInterstitialVideoAd.VideoAdCallbackType.AdStarted, null);
		}

		void onStopped ()
		{
			m_callback (AndroidInterstitialVideoAd.VideoAdCallbackType.AdStopped, null);
		}

		void onCompleted ()
		{
			m_callback (AndroidInterstitialVideoAd.VideoAdCallbackType.AdCompleted, null);
		}

		void onAdClicked ()
		{
			m_callback (AndroidInterstitialVideoAd.VideoAdCallbackType.AdClicked, null);
		}

		void onInformationClicked ()
		{
			m_callback (AndroidInterstitialVideoAd.VideoAdCallbackType.InformationClicked, null);
		}

		void onClosed ()
		{
			m_callback (AndroidInterstitialVideoAd.VideoAdCallbackType.AdClosed, null);
		}

		void onRewarded (AndroidJavaObject rewardItemObj)
		{
			string currencyName = rewardItemObj.Call<string> ("getCurrencyName");
			int currencyAmount = rewardItemObj.Call<int> ("getCurrencyAmount");
			m_callback (AndroidRewardedVideoAd.VideoAdCallbackType.Rewarded, new NendAdRewardedItem(currencyName, currencyAmount));
		}

	}
}
#endif



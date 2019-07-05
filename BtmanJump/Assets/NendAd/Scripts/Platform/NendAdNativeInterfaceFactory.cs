using NendUnityPlugin.AD.Video;

﻿namespace NendUnityPlugin.Platform
{
	internal class NendAdNativeInterfaceFactory
	{
		internal static NendAdLoggerInterface CreateLoggerInterface ()
		{
            #if UNITY_IOS && !UNITY_EDITOR
            return new NendUnityPlugin.Platform.iOS.IOSLoggerInterface();
            #elif UNITY_ANDROID && !UNITY_EDITOR
            return new NendUnityPlugin.Platform.Android.AndroidLoggerInterface();
            #else
            return null;
            #endif
		}

		internal static NendAdBannerInterface CreateBannerAdInterface ()
		{
			#if UNITY_IOS && !UNITY_EDITOR
			return new NendUnityPlugin.Platform.iOS.IOSBannerInterface();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			return new NendUnityPlugin.Platform.Android.AndroidBannerInterface();
			#else
			return new BannerStub ();
			#endif
		}

		#if UNITY_ANDROID
		internal static NendAdIconInterface CreateIconAdInterface ()
		{
            #if !UNITY_EDITOR
			return new NendUnityPlugin.Platform.Android.AndroidIconInterface();
     	    #else
			return new IconStub ();
		    #endif
		}
		#endif

		internal static NendAdInterstitialInterface CreateInterstitialAdInterface ()
		{
			#if UNITY_IOS && !UNITY_EDITOR
			return new NendUnityPlugin.Platform.iOS.IOSInterstitialInterface();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			return new NendUnityPlugin.Platform.Android.AndroidInterstitialInterface();
			#else
			return new InterstitialStub ();
			#endif
		}

		internal static NendAdInterstitialVideo CreateInterstitialVideoAd (string spotId, string apiKey)
		{
			#if UNITY_IOS && !UNITY_EDITOR
			return new NendUnityPlugin.Platform.iOS.IOSInterstitialVideoAd(spotId, apiKey);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			return new NendUnityPlugin.Platform.Android.AndroidInterstitialVideoAd(spotId, apiKey);
			#else
			return new StubInterstitialVideo ();
			#endif
		}

		internal static NendAdRewardedVideo CreateRewardedVideoAd (string spotId, string apiKey)
		{
			#if UNITY_IOS && !UNITY_EDITOR
			return new NendUnityPlugin.Platform.iOS.IOSRewardedVideoAd(spotId, apiKey);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			return new NendUnityPlugin.Platform.Android.AndroidRewardedVideoAd(spotId, apiKey);
			#else
			return new StubRewardedVideo ();
			#endif
		}

		private class BannerStub : NendAdBannerInterface
		{
			public void TryCreateBanner (string paramString)
			{
				UnityEngine.Debug.Log ("TryCreateBanner: " + paramString);
			}

			public void ShowBanner (string gameObject)
			{
				UnityEngine.Debug.Log ("ShowBanner: " + gameObject);
			}

			public void HideBanner (string gameObject)
			{
				UnityEngine.Debug.Log ("HideBanner: " + gameObject);
			}

			public void ResumeBanner (string gameObject)
			{
				UnityEngine.Debug.Log ("ResumeBanner: " + gameObject);
			}

			public void PauseBanner (string gameObject)
			{
				UnityEngine.Debug.Log ("PauseBanner: " + gameObject);
			}

			public void DestroyBanner (string gameObject)
			{
				UnityEngine.Debug.Log ("DestroyBanner: " + gameObject);
			}

			public void LayoutBanner (string gameObject, string paramString)
			{
				UnityEngine.Debug.Log ("LayoutBanner: " + gameObject + ", " + paramString);
			}
		}

		#if UNITY_ANDROID
		private class IconStub : NendAdIconInterface
		{
			public void TryCreateIcons (string paramString)
			{
				UnityEngine.Debug.Log ("TryCreateIcons: " + paramString);
			}

			public void ShowIcons (string gameObject)
			{
				UnityEngine.Debug.Log ("ShowIcons: " + gameObject);
			}

			public void HideIcons (string gameObject)
			{
				UnityEngine.Debug.Log ("HideIcons: " + gameObject);
			}

			public void ResumeIcons (string gameObject)
			{
				UnityEngine.Debug.Log ("ResumeIcons: " + gameObject);
			}

			public void PauseIcons (string gameObject)
			{
				UnityEngine.Debug.Log ("PauseIcons: " + gameObject);
			}

			public void DestroyIcons (string gameObject)
			{
				UnityEngine.Debug.Log ("DestroyIcons: " + gameObject);
			}

			public void LayoutIcons (string gameObject, string paramString)
			{
				UnityEngine.Debug.Log ("LayoutIcons: " + gameObject + ", " + paramString);
			}
		}
		#endif

		private class InterstitialStub : NendAdInterstitialInterface
		{
			public void LoadInterstitialAd (string apiKey, string spotId, bool isOutputLog)
			{
				UnityEngine.Debug.Log ("LoadInterstitialAd: " + apiKey + ", " + spotId + ", " + isOutputLog);
			}

			public void ShowInterstitialAd (string spotId)
			{
				UnityEngine.Debug.Log ("ShowInterstitialAd: " + spotId);
			}

			public void DismissInterstitialAd ()
			{
				UnityEngine.Debug.Log ("DismissInterstitialAd");
			}

			public void SetAutoReloadEnabled (bool enabled)
			{
				UnityEngine.Debug.Log ("SetAutoReloadEnabled: " + enabled);
			}
		}

		private class StubInterstitialVideo : NendAdInterstitialVideo
		{
			internal override void LoadInternal ()
			{
				UnityEngine.Debug.Log ("LoadInternal");
			}

			internal override bool IsLoadedInternal ()
			{
				UnityEngine.Debug.Log ("IsLoadedInternal");
				return false;
			}

			internal override void ShowInternal ()
			{
				UnityEngine.Debug.Log ("ShowInternal");
			}

			internal override void ReleaseInternal ()
			{
				UnityEngine.Debug.Log ("ReleaseInternal");
			}

			internal override void SetMediationNameInternal (string mediationName)
			{
				UnityEngine.Debug.Log ("SetMediationNameInternal: " + mediationName);
			}

			internal override void SetUserIdInternal (string userId)
			{
				UnityEngine.Debug.Log ("SetUserIdInternal: " + userId);
			}

			internal override void SetUserFeatureInternal (NendAdUserFeature userFeature)
			{
				UnityEngine.Debug.Log ("SetUserFeatureInternal: " + userFeature);
			}

            internal override void SetLocationEnabledInternal(bool enabled)
            {
                UnityEngine.Debug.Log ("SetLocationEnabledInternal: " + enabled);
            }

			#if UNITY_IOS
			internal override void SetOutputLog (bool logSetting)
			{
				UnityEngine.Debug.Log ("SetOutputLog: " + logSetting);
			}
			#endif

			internal override void AddFallbackFullboardInternal (string spotId, string apiKey, float r, float g, float b, float a)
			{
				UnityEngine.Debug.Log ("AddFallbackFullboardInternal: " + apiKey + ", " + spotId + ", " + r + ", " + g + ", " + b + ", " + a);
			}

            internal override void SetMuteStartPlayingInternal(bool mute)
            {
                UnityEngine.Debug.Log("SetMuteStartPlayingInternal: " + mute);
            }

            public override void Dispose ()
			{
				UnityEngine.Debug.Log ("Dispose");
			}
		}

		private class StubRewardedVideo : NendAdRewardedVideo
		{
			internal override void LoadInternal ()
			{
				UnityEngine.Debug.Log ("LoadInternal");
			}

			internal override bool IsLoadedInternal ()
			{
				UnityEngine.Debug.Log ("IsLoadedInternal");
				return false;
			}

			internal override void ShowInternal ()
			{
				UnityEngine.Debug.Log ("ShowInternal");
			}

			internal override void ReleaseInternal ()
			{
				UnityEngine.Debug.Log ("ReleaseInternal");
			}

			internal override void SetMediationNameInternal (string mediationName)
			{
				UnityEngine.Debug.Log ("SetMediationNameInternal: " + mediationName);
			}

			internal override void SetUserIdInternal (string userId)
			{
				UnityEngine.Debug.Log ("SetUserIdInternal: " + userId);
			}

			internal override void SetUserFeatureInternal (NendAdUserFeature userFeature)
			{
				UnityEngine.Debug.Log ("SetUserFeatureInternal: " + userFeature);
			}

            internal override void SetLocationEnabledInternal(bool enabled)
            {
                UnityEngine.Debug.Log ("SetLocationEnabledInternal: " + enabled);
            }

			#if UNITY_IOS
			internal override void SetOutputLog (bool logSetting)
			{
				UnityEngine.Debug.Log ("SetOutputLog: " + logSetting);
			}
			#endif

			public override void Dispose ()
			{
				UnityEngine.Debug.Log ("Dispose");
			}
		}
	}
}

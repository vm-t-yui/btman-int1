#if UNITY_ANDROID
namespace NendUnityPlugin.Platform.Android
{
	using System;
	using UnityEngine;
	using NendUnityPlugin.AD.Video;

	internal class AndroidRewardedVideoAd : NendAdRewardedVideo
	{
		private const string NendAdVideoClassName = "net.nend.unity.plugin.NendUnityRewardedVideoAd";
		private AndroidJavaObject m_JavaObject;
		private AndroidVideoAdListener m_Listener;

		internal AndroidRewardedVideoAd (string spotId, string apiKey) : base ()
		{
			using (var activity = CommonUtils.GetCurrentActivity()) {
				m_JavaObject = new AndroidJavaObject (NendAdVideoClassName, activity, int.Parse (spotId), apiKey);
			}
			m_Listener = AndroidVideoAdListener.NewRewardedListener (onResponse);
		}

		internal override void LoadInternal ()
		{
			VideoMethodUtils.LoadAd(m_JavaObject, m_Listener);
		}

		internal override bool IsLoadedInternal ()
		{
			return VideoMethodUtils.IsLoaded(m_JavaObject);
		}

		internal override void ShowInternal ()
		{
			VideoMethodUtils.ShowAd(m_JavaObject);
		}

		internal override void ReleaseInternal ()
		{
			if (null != m_JavaObject) {
				if (null != m_Listener) {
					m_Listener.ReleaseCallback ();
					m_Listener = null;
				}
				VideoMethodUtils.ReleaseAd(m_JavaObject);
				m_JavaObject.Dispose ();
				m_JavaObject = null;
			}
		}

		internal override void SetMediationNameInternal (string mediationName) {
			VideoMethodUtils.SetMediationName(m_JavaObject, mediationName);
		}

		internal override void SetUserIdInternal (string userId) {
			VideoMethodUtils.SetUserId(m_JavaObject, userId);
		}

		internal override void SetUserFeatureInternal (NendAdUserFeature userFeature) {
			VideoMethodUtils.SetUserFeature(m_JavaObject, userFeature);
		}

        internal override void SetLocationEnabledInternal(bool enabled)
        {
            VideoMethodUtils.SetLocationEnabled(m_JavaObject, enabled);
        }

        private void onResponse (VideoAdCallbackType type, object args)
		{
			switch (type) {
			case VideoAdCallbackType.Rewarded:
				NendAdRewardedItem item = (NendAdRewardedItem)args;
				CallBack (new RewardedVideoAdCallbackArgments (type, item));
				break;
			case VideoAdCallbackType.FailedToLoad:
				CallBack (new ErrorVideoAdCallbackArgments(type, (int)args));
				break;
			default:
				CallBack (new VideoAdCallbackArgments (type));
				break;
			}
		}

		~AndroidRewardedVideoAd ()
		{
			Dispose ();
		}

		public override void Dispose ()
		{
			NendUnityPlugin.Common.NendAdLogger.D ("Dispose AndroidVideoAd.");
			ReleaseInternal ();
		}
	}
}
#endif



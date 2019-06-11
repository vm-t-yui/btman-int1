#if UNITY_IOS
namespace NendUnityPlugin.Platform.iOS
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;
	using NendUnityPlugin.AD.Video;

	internal class IOSRewardedVideoAd : NendAdRewardedVideo
	{
		private IntPtr m_iOSRewardedVideoAdPtr;
		private IntPtr m_selfPtr;
		private delegate void NendUnityVideoAdNormalCallback (IntPtr selfPtr, VideoAdCallbackType type);
		private delegate void NendUnityVideoAdErrorCallback (IntPtr selfPtr, VideoAdCallbackType type, int errorCode);
		private delegate void NendUnityRewardedVideoAdCallback (
			IntPtr selfPtr, VideoAdCallbackType type, string currencyName, int currencyAmount);

		internal IOSRewardedVideoAd (string spotId, string apiKey) : base ()
		{
			m_selfPtr = (IntPtr)GCHandle.Alloc (this);
			m_iOSRewardedVideoAdPtr = _CreateRewardedVideoAd (spotId, apiKey);
		}

		internal override void LoadInternal ()
		{
			_LoadRewardedVideoAd (m_selfPtr, m_iOSRewardedVideoAdPtr, NormalRewardedCallback, ErrorRewardedCallback);
		}

		internal override bool IsLoadedInternal ()
		{
			return _IsLoadedRewarded (m_iOSRewardedVideoAdPtr);
		}

		internal override void ShowInternal ()
		{
			_ShowRewardedVideoAd (m_iOSRewardedVideoAdPtr, RewardedVideoAdCallback);
		}

		internal override void ReleaseInternal ()
		{
			_ReleaseRewardedVideoAd (m_iOSRewardedVideoAdPtr);
			if (IntPtr.Zero != m_selfPtr) {
				GCHandle handle = (GCHandle)m_selfPtr;
				handle.Free ();
				m_selfPtr = IntPtr.Zero;
			}
		}

		internal override void SetMediationNameInternal (string mediationName) {
			_SetRewardedMediationName (m_iOSRewardedVideoAdPtr, mediationName);
		}

		internal override void SetUserIdInternal (string userId) {
			_SetRewardedUserId (m_iOSRewardedVideoAdPtr, userId);
		}

		internal override void SetUserFeatureInternal (NendAdUserFeature userFeature) {
			IOSUserFeature iOSUserFeatureObj = (IOSUserFeature)userFeature;
			_SetRewardedUserFeature (m_iOSRewardedVideoAdPtr, iOSUserFeatureObj.BuildNendUserFeature(userFeature));
		}

        internal override void SetLocationEnabledInternal(bool enabled)
        {
            _SetRewardedLocationEnabled(m_iOSRewardedVideoAdPtr, enabled);
        }

        internal override void SetOutputLog (bool isOutputLog) {
			_SetRewardedOutputLog (isOutputLog);
		}

		[AOT.MonoPInvokeCallbackAttribute (typeof(NendUnityVideoAdNormalCallback))]
		private static void NormalRewardedCallback (IntPtr selfPtr, VideoAdCallbackType type)
		{
			GCHandle handle = (GCHandle)selfPtr;
			IOSRewardedVideoAd instance = (IOSRewardedVideoAd)handle.Target;

			instance.CallBack (new VideoAdCallbackArgments(type));
		}

		[AOT.MonoPInvokeCallbackAttribute (typeof(NendUnityVideoAdErrorCallback))]
		private static void ErrorRewardedCallback (IntPtr selfPtr, VideoAdCallbackType type, int errorCode)
		{
			GCHandle handle = (GCHandle)selfPtr;
			IOSRewardedVideoAd instance = (IOSRewardedVideoAd)handle.Target;

			instance.CallBack (new ErrorVideoAdCallbackArgments(type, errorCode));
		}

		[AOT.MonoPInvokeCallbackAttribute (typeof(NendUnityRewardedVideoAdCallback))]
		private static void RewardedVideoAdCallback (IntPtr selfPtr, VideoAdCallbackType type, string currencyName, int currencyAmount)
		{
			GCHandle handle = (GCHandle)selfPtr;
			IOSRewardedVideoAd instance = (IOSRewardedVideoAd)handle.Target;

			instance.CallBack (new RewardedVideoAdCallbackArgments(type, new NendAdRewardedItem (currencyName, currencyAmount)));
		}

		~IOSRewardedVideoAd ()
		{
			Dispose ();
		}

		public override void Dispose ()
		{
			ReleaseInternal ();
			_DisposeRewardedVideoAd (m_iOSRewardedVideoAdPtr);
			if (IntPtr.Zero != m_iOSRewardedVideoAdPtr) {
				GCHandle handle = (GCHandle)m_iOSRewardedVideoAdPtr;
				handle.Free ();
				m_iOSRewardedVideoAdPtr = IntPtr.Zero;
			}
			NendUnityPlugin.Common.NendAdLogger.D ("Dispose IOSRewardedVideoAd.");
		}

		[DllImport ("__Internal")]
		private static extern IntPtr _CreateRewardedVideoAd (string spotId, string apiKey);

		[DllImport ("__Internal")]
		private static extern void _LoadRewardedVideoAd (IntPtr self, IntPtr iOSVideoPtr, NendUnityVideoAdNormalCallback normalCallback, NendUnityVideoAdErrorCallback errorCallback);

		[DllImport ("__Internal")]
		private static extern bool _IsLoadedRewarded (IntPtr iOSVideoPtr);

		[DllImport ("__Internal")]
		private static extern void _ShowRewardedVideoAd (IntPtr iOSVideoPtr, NendUnityRewardedVideoAdCallback rewardCallback);

		[DllImport ("__Internal")]
		private static extern void _ReleaseRewardedVideoAd (IntPtr iOSVideoPtr);

		[DllImport ("__Internal")]
		private static extern void _SetRewardedMediationName (IntPtr iOSVideoPtr, string mediationName);

		[DllImport ("__Internal")]
		private static extern void _SetRewardedUserId (IntPtr iOSVideoPtr, string userId);

		[DllImport ("__Internal")]
		private static extern void _SetRewardedUserFeature (IntPtr iOSVideoPtr, IntPtr iOSUserFeaturePtr);

        [DllImport("__Internal")]
        private static extern void _SetRewardedLocationEnabled(IntPtr iOSVideoPtr, bool enabled);

        [DllImport ("__Internal")]
		private static extern void _SetRewardedOutputLog (bool isOutputLog);

		[DllImport ("__Internal")]
		private static extern void _DisposeRewardedVideoAd (IntPtr iOSVideoPtr);
	}
}
#endif

#if UNITY_IOS
namespace NendUnityPlugin.Platform.iOS
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;
	using NendUnityPlugin.AD.Video;

	internal class IOSInterstitialVideoAd : NendAdInterstitialVideo
	{
		private delegate void NendUnityVideoAdNormalCallback (IntPtr selfPtr, VideoAdCallbackType type);
		private delegate void NendUnityVideoAdErrorCallback (IntPtr selfPtr, VideoAdCallbackType type, int errorCode);

		private IntPtr m_iOSInterstitialVideoAdPtr;
		private IntPtr m_selfPtr;

		internal IOSInterstitialVideoAd (string spotId, string apiKey) : base ()
		{
			m_selfPtr = (IntPtr)GCHandle.Alloc (this);
			m_iOSInterstitialVideoAdPtr = _CreateInterstitialVideoAd (spotId, apiKey);
		}

		internal override void LoadInternal ()
		{
			_LoadInterstitialVideoAd (m_selfPtr, m_iOSInterstitialVideoAdPtr, NormalInterstitialCallback, ErrorInterstitialCallback);
		}

		internal override bool IsLoadedInternal ()
		{
			return _IsLoadedInterstitial (m_iOSInterstitialVideoAdPtr);
		}

		internal override void ShowInternal ()
		{
			_ShowInterstitialVideoAd (m_iOSInterstitialVideoAdPtr);
		}

		internal override void ReleaseInternal ()
		{
			_ReleaseInterstitialVideoAd (m_iOSInterstitialVideoAdPtr);
			if (IntPtr.Zero != m_selfPtr) {
				GCHandle handle = (GCHandle)m_selfPtr;
				handle.Free ();
				m_selfPtr = IntPtr.Zero;
			}
		}

		internal override void SetMediationNameInternal (string mediationName) {
			_SetInterstitialMediationName (m_iOSInterstitialVideoAdPtr, mediationName);
		}

		internal override void SetUserIdInternal (string userId) {
			_SetInterstitialUserId (m_iOSInterstitialVideoAdPtr, userId);
		}

		internal override void SetUserFeatureInternal (NendAdUserFeature userFeature) {
			IOSUserFeature iOSUserFeatureObj = (IOSUserFeature)userFeature;
			_SetInterstitialUserFeature (m_iOSInterstitialVideoAdPtr, iOSUserFeatureObj.BuildNendUserFeature(userFeature));
		}

        internal override void SetLocationEnabledInternal(bool enabled)
        {
            _SetInterstitialLocationEnabled(m_iOSInterstitialVideoAdPtr, enabled);
        }

        internal override void SetOutputLog (bool isOutputLog) {
			_SetInterstitialOutputLog (isOutputLog);
		}

        internal override void AddFallbackFullboardInternal (string spotId, string apiKey, float r, float g, float b, float a) {
			_AddFallbackFullboard (m_iOSInterstitialVideoAdPtr, spotId, apiKey, r, g, b, a);
		}

        internal override void SetMuteStartPlayingInternal(bool mute)
        {
            _SetMuteStartPlaying(m_iOSInterstitialVideoAdPtr, mute);
        }

        [AOT.MonoPInvokeCallbackAttribute (typeof(NendUnityVideoAdNormalCallback))]
		private static void NormalInterstitialCallback (IntPtr selfPtr, VideoAdCallbackType type)
		{
			GCHandle handle = (GCHandle)selfPtr;
			IOSInterstitialVideoAd instance = (IOSInterstitialVideoAd)handle.Target;

			instance.CallBack (new VideoAdCallbackArgments(type));
		}

		[AOT.MonoPInvokeCallbackAttribute (typeof(NendUnityVideoAdErrorCallback))]
		private static void ErrorInterstitialCallback (IntPtr selfPtr, VideoAdCallbackType type, int errorCode)
		{
			GCHandle handle = (GCHandle)selfPtr;
			IOSInterstitialVideoAd instance = (IOSInterstitialVideoAd)handle.Target;

			instance.CallBack (new ErrorVideoAdCallbackArgments(type, errorCode));
		}

		~IOSInterstitialVideoAd ()
		{
			Dispose ();
		}

		public override void Dispose ()
		{
			ReleaseInternal ();
			_DisposeInterstitialVideoAd (m_iOSInterstitialVideoAdPtr);
			if (IntPtr.Zero != m_iOSInterstitialVideoAdPtr) {
				GCHandle handle = (GCHandle)m_iOSInterstitialVideoAdPtr;
				handle.Free ();
				m_iOSInterstitialVideoAdPtr = IntPtr.Zero;
			}
			NendUnityPlugin.Common.NendAdLogger.D ("Dispose IOSInterstitialVideoAd.");
		}

		[DllImport ("__Internal")]
		private static extern IntPtr _CreateInterstitialVideoAd (string spotId, string apiKey);

		[DllImport ("__Internal")]
		private static extern void _LoadInterstitialVideoAd (IntPtr self, IntPtr iOSVideoPtr, NendUnityVideoAdNormalCallback normalCallback, NendUnityVideoAdErrorCallback errorCallback);

		[DllImport ("__Internal")]
		private static extern bool _IsLoadedInterstitial (IntPtr iOSVideoPtr);

		[DllImport ("__Internal")]
		private static extern void _ShowInterstitialVideoAd (IntPtr iOSVideoPtr);

		[DllImport ("__Internal")]
		private static extern void _ReleaseInterstitialVideoAd (IntPtr iOSVideoPtr);

		[DllImport ("__Internal")]
		private static extern void _SetInterstitialMediationName (IntPtr iOSVideoPtr, string mediationName);

		[DllImport ("__Internal")]
		private static extern void _SetInterstitialUserId (IntPtr iOSVideoPtr, string userId);

		[DllImport ("__Internal")]
		private static extern void _SetInterstitialUserFeature (IntPtr iOSVideoPtr, IntPtr iOSUserFeaturePtr);

        [DllImport("__Internal")]
        private static extern void _SetInterstitialLocationEnabled(IntPtr iOSVideoPtr, bool enabled);

        [DllImport ("__Internal")]
		private static extern void _SetInterstitialOutputLog (bool isOutputLog);

		[DllImport ("__Internal")]
		private static extern void _AddFallbackFullboard (IntPtr iOSVideoPtr, string spotId, string apiKey, float r, float g, float b, float a);

        [DllImport("__Internal")]
        private static extern void _SetMuteStartPlaying(IntPtr iOSVideoPtr, bool mute);

        [DllImport ("__Internal")]
		private static extern void _DisposeInterstitialVideoAd (IntPtr iOSVideoPtr);
	}
}
#endif

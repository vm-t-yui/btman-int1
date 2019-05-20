#if UNITY_ANDROID
namespace NendUnityPlugin.Platform.Android
{
    using System;
    using UnityEngine;

	using NendUnityPlugin.AD.Native;
	using Log = NendUnityPlugin.Common.NendAdLogger;
	
    internal class AndroidNativeAd : NativeAd
	{
		private const string UnityPlayerClassName = "com.unity3d.player.UnityPlayer";
		private AndroidJavaObject m_NativeAd;

		internal AndroidNativeAd (AndroidJavaObject nativeAd)
		{
			m_NativeAd = nativeAd;
		}

		~AndroidNativeAd ()
		{
			Dispose ();
		}

		public override void Dispose ()
		{
			base.Dispose ();
            Log.D ("Dispose AndroidNativeAd.");
			if (null != m_NativeAd) {
				m_NativeAd.Dispose ();
				m_NativeAd = null;
			}
		}

		internal override void SendImpression ()
		{
			m_NativeAd.Call ("sendImpression");
		}

		internal override void PerformAdClick ()
		{
			using (var unityPlayer = new AndroidJavaClass (UnityPlayerClassName)) {
				using (var activity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity")) {
					m_NativeAd.Call ("performAdClick", activity);
				}
			}
		}

		internal override void PerformInformationClick ()
		{
			using (var unityPlayer = new AndroidJavaClass (UnityPlayerClassName)) {
				using (var activity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity")) {
					m_NativeAd.Call ("performInformationClick", activity);
				}
			}
		}

		public override string GetAdvertisingExplicitlyText (AdvertisingExplicitly advertisingExplicitly)
		{
			return m_NativeAd.Call<string> ("getAdvertisingExplicitlyText", (int)advertisingExplicitly);
		}

		public override string ShortText {
			get {
				return m_NativeAd.Call<string> ("getShortText");
			}
		}

		public override string LongText {
			get {
				return m_NativeAd.Call<string> ("getLongText");
			}
		}

		public override string PromotionUrl {
			get {
				return m_NativeAd.Call<string> ("getPromotionUrl");
			}
		}

		public override string PromotionName {
			get {
				return m_NativeAd.Call<string> ("getPromotionName");
			}
		}

		public override string ActionButtonText {
			get {
				return m_NativeAd.Call<string> ("getActionButtonText");
			}
		}

		public override string AdImageUrl {
			get {
				return BridgeGettingNonNullUrl("getAdImageUrl");
			}
		}
			
		public override string LogoImageUrl {
			get {
				return BridgeGettingNonNullUrl("getLogoImageUrl");
			}
		}

		private string BridgeGettingNonNullUrl(string methodName) {
			var obj = m_NativeAd.Call<AndroidJavaObject>(methodName);
			if (obj == null) {
				return "";
			} else {
				return obj.Call<string>("toString");
			}
		}
	}
}
#endif
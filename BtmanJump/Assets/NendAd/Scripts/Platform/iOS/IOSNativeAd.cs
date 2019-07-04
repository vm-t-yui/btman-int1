#if UNITY_IOS
namespace NendUnityPlugin.Platform.iOS
{
	using System;
	using System.Runtime.InteropServices;

	using NendUnityPlugin.AD.Native;
	using Log = NendUnityPlugin.Common.NendAdLogger;

	internal class IOSNativeAd : NativeAd
	{
		private IntPtr m_NativeAdPtr;

		internal IOSNativeAd (IntPtr nativeAd)
		{
			m_NativeAdPtr = nativeAd;
		}

		~IOSNativeAd ()
		{
			Dispose ();
		}

		public override void Dispose ()
		{
			base.Dispose ();
			Log.D ("Dispose IOSNativeAd.");
			_ReleaseNativeAd (m_NativeAdPtr);
			if (IntPtr.Zero != m_NativeAdPtr) {
				GCHandle handle = (GCHandle)m_NativeAdPtr;
				handle.Free ();
				m_NativeAdPtr = IntPtr.Zero;
			}
		}

		internal override void SendImpression ()
		{
			_SendImpression (m_NativeAdPtr);
		}

		internal override void PerformAdClick ()
		{
			_PerformAdClick (m_NativeAdPtr);
		}

		internal override void PerformInformationClick ()
		{
			_PerformInformationClick (m_NativeAdPtr);
		}

		public override string GetAdvertisingExplicitlyText (AdvertisingExplicitly advertisingExplicitly)
		{
			return _GetAdvertisingExplicitlyText (m_NativeAdPtr, (int)advertisingExplicitly);
		}

		public override string ShortText {
			get {
				return _GetShortText (m_NativeAdPtr);
			}
		}

		public override string LongText {
			get {
				return _GetLongText (m_NativeAdPtr);
			}
		}

		public override string PromotionUrl {
			get {
				return _GetPromotionUrl (m_NativeAdPtr);
			}
		}

		public override string PromotionName {
			get {
				return _GetPromotionName (m_NativeAdPtr);
			}
		}

		public override string ActionButtonText {
			get {
				return _GetActionButtonText (m_NativeAdPtr);
			}
		}

		public override string AdImageUrl {
			get {
				return _GetAdImageUrl (m_NativeAdPtr);
			}
		}

		public override string LogoImageUrl {
			get {
				return _GetLogoImageUrl (m_NativeAdPtr);
			}
		}
			
		[DllImport ("__Internal")]
		private static extern string _GetShortText (IntPtr nativeAd);

		[DllImport ("__Internal")]
		private static extern string _GetLongText (IntPtr nativeAd);

		[DllImport ("__Internal")]
		private static extern string _GetPromotionName (IntPtr nativeAd);

		[DllImport ("__Internal")]
		private static extern string _GetPromotionUrl (IntPtr nativeAd);

		[DllImport ("__Internal")]
		private static extern string _GetActionButtonText (IntPtr nativeAd);

		[DllImport ("__Internal")]
		private static extern string _GetAdImageUrl (IntPtr nativeAd);

		[DllImport ("__Internal")]
		private static extern string _GetLogoImageUrl (IntPtr nativeAd);

		[DllImport ("__Internal")]
		private static extern string _GetAdvertisingExplicitlyText (IntPtr nativeAd, int unityAdvertisingExplicitly);

		[DllImport ("__Internal")]
		private static extern void _PerformAdClick (IntPtr nativeAd);

		[DllImport ("__Internal")]
		private static extern void _PerformInformationClick (IntPtr nativeAd);

		[DllImport ("__Internal")]
		private static extern void _SendImpression (IntPtr nativeAd);

		[DllImport ("__Internal")]
		private static extern void _ReleaseNativeAd (IntPtr nativeAd);
	}
}
#endif
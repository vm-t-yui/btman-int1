namespace NendUnityPlugin.AD.Native
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	/// <summary>
	/// Native ad client factory.
	/// </summary>
	public class NativeAdClientFactory
	{
		/// <summary>
		/// Create the new client instance.
		/// </summary>
		/// <returns>The client.</returns>
		/// <param name="spotId">Spot identifier.</param>
		/// <param name="apiKey">API key.</param>
		/// \warning Call this on the main thread.
		public static INativeAdClient NewClient (string spotId, string apiKey)
		{
			#if !UNITY_EDITOR && UNITY_IOS
			return new NendUnityPlugin.Platform.iOS.IOSNativeAdClient(apiKey, spotId);
			#elif !UNITY_EDITOR && UNITY_ANDROID
			return new NendUnityPlugin.Platform.Android.AndroidNativeAdClient(apiKey, spotId);
			#else
			return null;
			#endif
		}

		#region Test

		#if !(!UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID))

		/// <summary>
		/// The type of native ad.
		/// </summary>
		/// \warning Can use only on UnityEditor.
		public enum NativeAdType
		{
			/// <summary>
			/// Contains 80x80 image.
			/// </summary>
			SmallSquare,
			/// <summary>
			/// Contains 300x180 image.
			/// </summary>
			LargeWide,
			/// <summary>
			/// Text only.
			/// </summary>
			TextOnly
		}

		/// <summary>
		/// Create the new client instance for test.
		/// </summary>
		/// <returns>The client.</returns>
		/// <param name="type">Type.</param>
		/// \warning Can use only on UnityEditor.
		public static INativeAdClient NewClient (NativeAdType type) {
			return new NativeClientStub (type);
		}

		private class NativeClientStub : NativeAdClient
		{
			private NativeAdType m_AdType;

			internal NativeClientStub (NativeAdType adType)
			{
				m_AdType = adType;
			}

			public override void LoadNativeAd (Action<INativeAd, int, string> callback)
			{
				m_Callbacks.Enqueue (callback);
				DeliverResponseOnMainThread (new DummyNativeAd (m_AdType), 200, "");
			}
		}

		private class DummyNativeAd : NativeAd
		{
			private NativeAdType m_AdType;

			internal DummyNativeAd (NativeAdType adType)
			{
				m_AdType = adType;
			}

			public override string ToString ()
			{
				return "DummyNativeAd: " + GetHashCode ();
			}

			internal override void SendImpression ()
			{
				Debug.Log ("SendImpression");
			}

			internal override void PerformAdClick ()
			{
				Debug.Log ("PerformAdClick");
			}

			internal override void PerformInformationClick ()
			{
				Debug.Log ("PerformInformationClick");
			}

			public override string GetAdvertisingExplicitlyText (AdvertisingExplicitly advertisingExplicitly)
			{
				switch (advertisingExplicitly) {
				case AdvertisingExplicitly.PR:
					return "PR";
				case AdvertisingExplicitly.Sponsored:
					return "Sponsored";
				case AdvertisingExplicitly.AD:
					return "広告";
				case AdvertisingExplicitly.Promotion:
					return "プロモーション";
				default:
					return "";
				}
			}

			public override string ShortText {
				get {
					return "国内・海外の旅行予約はnendトラベル";
				}
			}

			public override string LongText {
				get {
					return "国内旅行・海外旅行のツアーやホテル予約が簡単。日程や条件から細かく検索できます";
				}
			}

			public override string PromotionUrl {
				get {
					return "nend.net";
				}
			}

			public override string PromotionName {
				get {
					return "nendトラベル";
				}
			}

			public override string ActionButtonText {
				get {
					return "サイトへ行く";
				}
			}

			public override string AdImageUrl {
				get {
					switch (m_AdType) {
					case NativeAdType.SmallSquare:
						return "https://img1.nend.net/img/banner/329/71102/1307028_d8e566a7fdee9126923c663f98de6ea523145629156cdab72ff39418.png";
					case NativeAdType.LargeWide:
						return "https://img1.nend.net/img/banner/329/71105/1307070_5ff07f10c1914bae2c528ef2bbc6346e064b78bcf0ab0dd12086d94f.png";
					default:
						return null;
					}
				}
			}

			public override  string LogoImageUrl {
				get {
					if (NativeAdType.LargeWide == m_AdType) {
						return "https://img1.nend.net/img/banner/329/71105/1307071_e1bc7aa5907934f00d1cd9de5cb9f8a60582864570a8221ca27b0c44.png";
					} else {
						return null;
					}
				}
			}
		}
		#endif

		#endregion
	}
}

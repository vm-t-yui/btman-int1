namespace NendUnityPlugin.AD.Native
{
	using System;
	using UnityEngine;
	using UnityEngine.UI;

	using NendUnityPlugin.AD.Native.Events;
	using NendUnityPlugin.AD.Native.Utils;

	using Log = NendUnityPlugin.Common.NendAdLogger;

	/// <summary>
	/// Native ad view for Inspector.
	/// </summary>
	[RequireComponent (typeof(Mask))]
	[RequireComponent (typeof(RectTransform))]
	public sealed class NendAdNativeView : MonoBehaviour
	{
		[SerializeField] private int viewTag;
		[SerializeField] private bool renderWhenLoaded = true;
		[SerializeField] private bool renderWhenActivated = true;
		[SerializeField] private AdvertisingExplicitly advertisingExplicitly;
		[SerializeField] private Text prText;
		[SerializeField] private Text shortText;
		[SerializeField] private Text longText;
		[SerializeField] private Text promotionNameText;
		[SerializeField] private Text promotionUrlText;
		[SerializeField] private Text actionButtonText;
		[SerializeField] private RawImage adImage;
		[SerializeField] private RawImage logoImage;

		#region AdEvents

		/// <summary>
		/// The ad shown.
		/// </summary>
		/// \deprecated Will be removed in the future.
		[Obsolete ("Will be removed in the future.", false)]
		public NativeAdViewEvent AdShown;

		/// <summary>
		/// The ad failed to show.
		/// </summary>
		/// \deprecated Not used anymore.
		[Obsolete ("Not used anymore.", false)]
		public NativeAdViewEvent AdFailedToShow;

		/// <summary>
		/// The ad clicked.
		/// </summary>
		public NativeAdViewEvent AdClicked;

		#endregion

		private INativeAd m_LoadedNativeAd = null;
		private INativeAd m_ShowingNativeAd = null;

		/// <summary>
		/// Gets or sets the view tag.
		/// </summary>
		/// <value>The view tag.</value>
		public int ViewTag {
			get {
				return viewTag;
			}
			set {
				viewTag = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="NendUnityPlugin.AD.Native.NendAdNativeView"/> is loaded.
		/// </summary>
		/// <value><c>true</c> if loaded; otherwise, <c>false</c>.</value>
		public bool Loaded { 
			get {
				return null != m_LoadedNativeAd;
			}
		}
			
		private INativeAd ShowingNativeAd {
			set {
				if (null != m_ShowingNativeAd && m_ShowingNativeAd != value) {
					Log.D ("Remove old click event.");
					m_ShowingNativeAd.AdClicked -= OnClickAd;
				}

				m_ShowingNativeAd = value;

				UIRenderer.TryRenderText (prText, m_ShowingNativeAd.GetAdvertisingExplicitlyText (advertisingExplicitly));
				UIRenderer.TryRenderText (shortText, m_ShowingNativeAd.ShortText);
				UIRenderer.TryRenderText (longText, m_ShowingNativeAd.LongText);
				UIRenderer.TryRenderText (promotionNameText, m_ShowingNativeAd.PromotionName);
				UIRenderer.TryRenderText (promotionUrlText, m_ShowingNativeAd.PromotionUrl);
				UIRenderer.TryRenderText (actionButtonText, m_ShowingNativeAd.ActionButtonText);
				UIRenderer.TryRenderImage (this, adImage, m_ShowingNativeAd.AdImageUrl);
				UIRenderer.TryRenderImage (this, logoImage, m_ShowingNativeAd.LogoImageUrl);

				m_ShowingNativeAd.Activate (gameObject, prText.gameObject);
				m_ShowingNativeAd.AdClicked += OnClickAd;

				PostAdShown ();
			}
		}

		void Awake ()
		{
			Clear ();
		}
			
		void OnEnable ()
		{
			if (Loaded && renderWhenActivated) {
				RenderAd ();
			}
		}

		void OnDisable ()
		{
			Clear ();
		}

		void OnDestroy ()
		{
			Log.D ("OnDestroy: {0}", name);
			m_LoadedNativeAd = null;
			m_ShowingNativeAd = null;
		}

		#region Public

		/// <summary>
		/// Renders the ad.
		/// </summary>
		/// <returns><c>true</c>, if ad was rendered, <c>false</c> otherwise.</returns>
		public bool RenderAd ()
		{
			if (Loaded) {
				ShowingNativeAd = m_LoadedNativeAd;
				return true;
			} else {
				return false;
			}
		}

		#endregion

		#region Internal

		internal void OnLoadAd (INativeAd ad)
		{
			m_LoadedNativeAd = ad;
			if (renderWhenLoaded) {
				RenderAd ();
			}
		}

		internal void RenderAd (INativeAd ad)
		{
			m_LoadedNativeAd = ad;
			RenderAd ();
		}

		private void Clear ()
		{
			UIRenderer.TryClearText (prText);
			UIRenderer.TryClearText (shortText);
			UIRenderer.TryClearText (longText);
			UIRenderer.TryClearText (promotionNameText);
			UIRenderer.TryClearText (promotionUrlText);
			UIRenderer.TryClearText (actionButtonText);
			UIRenderer.TryClearImage (adImage);
			UIRenderer.TryClearImage (logoImage);
		}

		internal void OnClickAd (object sender, EventArgs e) 
		{
			PostAdClicked ();
		}
			
		#endregion

		#region Notifications

		private void PostAdShown ()
		{
			#pragma warning disable 618
			if (null != AdShown) {
				AdShown.Invoke (this);
			}
			#pragma warning restore 618
		}

		private void PostAdClicked ()
		{
			if (null != AdClicked) {
				AdClicked.Invoke (this);
			}
		}

		#endregion
	}
}
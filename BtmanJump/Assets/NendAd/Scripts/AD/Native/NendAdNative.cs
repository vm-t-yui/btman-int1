namespace NendUnityPlugin.AD.Native
{
	using System;
	using System.Linq;
	using UnityEngine;
	using UnityEngine.EventSystems;
	using UnityEngine.Events;

	using NendUnityPlugin.Common;
	using NendUnityPlugin.AD.Native.Events;

	using Log = NendUnityPlugin.Common.NendAdLogger;
	using Timer = NendUnityPlugin.AD.Native.Utils.SimpleTimer;

	/// <summary>
	/// Native ad for Inspector.
	/// </summary>
	public sealed class NendAdNative : MonoBehaviour
	{
		[SerializeField] private NendUtils.Account account;
		[SerializeField] private bool loadWhenActivated = true;
		[SerializeField] private bool enableAutoReload = false;
		[SerializeField] private double reloadInterval = 0.0;
		[SerializeField] private NendAdNativeView[] views;

		#region AdEvents

		/// <summary>
		/// The ad loaded.
		/// </summary>
		public NativeAdViewEvent AdLoaded;

		/// <summary>
		/// The ad failed to receive.
		/// </summary>
		public NativeAdViewFailedToLoadEvent AdFailedToReceive;

		#endregion

		#if !(!UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID))
		[SerializeField] private NativeAdClientFactory.NativeAdType type;
		#endif

		private INativeAdClient m_Client;

		private INativeAdClient Client { get { return m_Client ?? (m_Client = CreateClient ()); } }

		private Timer m_Timer;

		// Use this for initialization
		void Start ()
		{
			m_Timer = new Timer ();
			m_Timer.OnFireEvent = () => {
				LoadAd ();
			};

			if (loadWhenActivated) {
				LoadAd ();
			}

			if (enableAutoReload) {
				EnableAutoReload (reloadInterval);
			}
		}

		void OnDestroy ()
		{
			Log.D ("OnDestroy: {0}", name);
			if (null != m_Timer) {
				m_Timer.Dispose ();
				m_Timer = null;
			}
			m_Client = null;
			views = null;
		}

		#region Public

		/// <summary>
		/// Gets the list of NendAdNativeView.
		/// </summary>
		/// <value>The list of NendAdNativeView.</value>
		public NendAdNativeView[] Views {
			get {
				return views;
			}
		}

		/// <summary>
		/// Registers the NendAdNativeView.
		/// </summary>
		/// <param name="view">NendAdNativeView</param>
		public void RegisterAdView (NendAdNativeView view)
		{
			if (null != view && views.All (v => v != view)) {
				views = views.Concat (new NendAdNativeView[] { view }).ToArray ();
			}
		}

		/// <summary>
		/// Unregisters the NendAdNativeView.
		/// </summary>
		/// <param name="view">NendAdNativeView</param>
		public void UnregisterAdView (NendAdNativeView view)
		{
			if (null != view) {
				var index = Array.IndexOf (views, view);
				if (-1 != index) {
					Array.Clear (views, index, 1);
				}
			}
		}

		/// <summary>
		/// Loads the ad.
		/// </summary>
		public void LoadAd ()
		{
			if (null != views && 0 < views.Length) {
				LoadAd (views.Where (v => null != v).DefaultIfEmpty ().ToArray ());
			} else {
				Log.W ("views are empty.");
			}
		}

		/// <summary>
		/// Loads the ad.
		/// </summary>
		/// <param name="tag">Tag of NendAdNativeView.</param>
		public void LoadAd (int tag)
		{
			if (null != views && 0 < views.Length) {
				LoadAd (views.Where (v => null != v && v.ViewTag == tag).DefaultIfEmpty ().ToArray ());
			} else {
				Log.W ("views are empty.");
			}
		}

		/// <summary>
		/// Enables the auto reload.
		/// </summary>
		/// <param name="interval">Reload Interval（millisecond）.</param>
		/// \note Reload interval is valid for more than 30 seconds.
		public void EnableAutoReload (double interval)
		{
			if (NativeAdClient.MinimumReloadInterval <= interval) {
				m_Timer.Start (interval);
			} else {
				Log.W ("A reload interval is less than 30 seconds.");
			}
		}

		/// <summary>
		/// Disables the auto reload.
		/// </summary>
		public void DisableAutoReload ()
		{
			m_Timer.Stop ();
		}

		#endregion

		#region Internal

		private void LoadAd (NendAdNativeView[] views)
		{
			foreach (var view in views) {
				if (null == view) {
					continue;
				}
				var nativeAdView = view; // Important
				Client.LoadNativeAd ((nativeAd, code, message) => {
					if (null != nativeAd) {
						Log.I ("Load native ad. tag: {0}", nativeAdView.ViewTag);
						nativeAdView.OnLoadAd (nativeAd);
						PostAdLoaded (nativeAdView);
					} else {
						Log.E ("Failed to load ad. code: {0}, message: {1}", code, message);
						PostAdFailedToReceive (nativeAdView, code, message);
					}
				});
			}
		}

		private INativeAdClient CreateClient ()
		{
			#if UNITY_IOS && !UNITY_EDITOR
			return NativeAdClientFactory.NewClient (account.iOS.spotID.ToString (), account.iOS.apiKey);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			return NativeAdClientFactory.NewClient (account.android.spotID.ToString (), account.android.apiKey);
			#else
			return NativeAdClientFactory.NewClient (type);
			#endif
		}

		#endregion

		#region Events

		private void PostAdLoaded (NendAdNativeView view)
		{
			if (null != AdLoaded) {
				AdLoaded.Invoke (view);
			}
		}

		private void PostAdFailedToReceive (NendAdNativeView view, int code, string message)
		{
			if (null != AdFailedToReceive) {
				AdFailedToReceive.Invoke (view, code, message);
			}
		}

		#endregion
	}
}

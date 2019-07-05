namespace NendUnityPlugin.AD.Native
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	using Callback = System.Action<NendUnityPlugin.AD.Native.INativeAd, int, string>;
	using Timer = NendUnityPlugin.AD.Native.Utils.SimpleTimer;
	using Log = NendUnityPlugin.Common.NendAdLogger;
	using Worker = NendUnityPlugin.Common.NendAdMainThreadWorker;

	internal class NativeAdClient : INativeAdClient, IDisposable
	{
		internal const double MinimumReloadInterval = 30.0 * 1000.0;

		private Timer m_Timer;
		private Callback m_ReloadCallback;
		protected Queue<Callback> m_Callbacks = new Queue<Callback> ();

		private const int ERROR_CODE_UNSUPPORTED_IMAGE_FORMAT = 342;
		private const string ERROR_MESSAGE_UNSUPPORTED_IMAGE_FORMAT = "Gif files are not supported by Unity.";

		protected NativeAdClient ()
		{
			Worker.Prepare ();
			m_Timer = new Timer ();
			m_Timer.OnFireEvent = () => {
				LoadNativeAd (m_ReloadCallback);
			};
		}

		~NativeAdClient ()
		{
			Dispose ();
		}

		public virtual void Dispose ()
		{
			Log.D ("Dispose NativeAdClient.");
			m_Callbacks.Clear ();
			if (null != m_Timer) {
				m_Timer.Dispose ();
				m_Timer = null;
			}
		}

		public virtual void LoadNativeAd (Callback callback)
		{
			throw new NotImplementedException ();
		}

		public void EnableAutoReload (double interval, Callback callback)
		{
			if (MinimumReloadInterval <= interval) {
				m_ReloadCallback = callback;
				m_Timer.Start (interval);
			} else {
				Log.W ("A reload interval is less than 30 seconds.");
			}
		}

		public void DisableAutoReload ()
		{
			m_Timer.Stop ();
		}

		protected void EnqueueCallback (Callback callback)
		{
			if (null != callback) {
				lock (m_Callbacks) {
					m_Callbacks.Enqueue (callback);
				}
			}
		}

		protected void DeliverResponseOnMainThread (INativeAd ad, int code, string message)
		{
			lock (m_Callbacks) {
				if (0 < m_Callbacks.Count) {
					if (ad != null && (IsGifImage (ad.AdImageUrl) || IsGifImage (ad.LogoImageUrl))) {
						ad = null;
						code = ERROR_CODE_UNSUPPORTED_IMAGE_FORMAT;
						message = ERROR_MESSAGE_UNSUPPORTED_IMAGE_FORMAT;
					}
					var callback = m_Callbacks.Dequeue ();
					Worker.Instance.Post (() => {
						callback (ad, code, message);
					});
				}
			}
		}

		private static bool IsGifImage (string imageUrl)
		{
			return !string.IsNullOrEmpty (imageUrl) && imageUrl.ToLower ().EndsWith (".gif");
		}
	}
}
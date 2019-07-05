using System;
using UnityEngine;

using NendUnityPlugin.Platform;
using NendUnityPlugin.Common;

namespace NendUnityPlugin.AD
{
	/// <summary>
	/// Interstitial ad.
	/// </summary>
	public class NendAdInterstitial : MonoBehaviour
	{
		[SerializeField]
		bool outputLog = false;//Note: NendAdLoggerへ置き換え完了したら削除予定

		private static NendAdInterstitial _instance = null;
		private bool m_IsAutoReloadEnabled = true;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <returns>This instance.</returns>
		/// \warning When GameObject that added a NendAdInterstitial is not loaded, it returns null.
		public static NendAdInterstitial Instance {
			get {
				return _instance;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is auto reload enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is auto reload enabled; otherwise, <c>false</c>.</value>
		public bool IsAutoReloadEnabled {
			set {
				m_IsAutoReloadEnabled = value;
				Interface.SetAutoReloadEnabled (m_IsAutoReloadEnabled);
			}
			get {
				return m_IsAutoReloadEnabled;
			}
		}

		private NendAdInterstitialInterface _interface = null;

		private NendAdInterstitialInterface Interface {
			get {
				if (null == _interface) {
					_interface = NendAdNativeInterfaceFactory.CreateInterstitialAdInterface ();
				}
				return _interface;
			}
		}

		#region EventHandlers

		/// <summary>
		/// Occurs when ad loaded.
		/// </summary>
		/// \sa NendUnityPlugin.AD.NendAdInterstitialLoadEventArgs
		public event EventHandler<NendAdInterstitialLoadEventArgs> AdLoaded;

		/// <summary>
		/// Occurs when ad clicked.
		/// </summary>
		/// \sa NendUnityPlugin.AD.NendAdInterstitialClickEventArgs
		public event EventHandler<NendAdInterstitialClickEventArgs> AdClicked;

		/// <summary>
		/// Occurs when ad is being displayed.
		/// </summary>
		/// \sa NendUnityPlugin.AD.NendAdInterstitialShowEventArgs
		public event EventHandler<NendAdInterstitialShowEventArgs> AdShown;

		#endregion

		void Awake ()
		{			
			if (null == _instance) {
				_instance = this;
				gameObject.hideFlags = HideFlags.HideAndDontSave;
				DontDestroyOnLoad (gameObject);
			} else {
				Destroy (gameObject);
			}
		}

		/// <summary>
		/// Load interstitial ad.
		/// </summary>
		/// <param name="apiKey">API key.</param>
		/// <param name="spotId">Spot id.</param>
		public void Load (string apiKey, string spotId)
		{
			Interface.LoadInterstitialAd (apiKey, spotId, outputLog);
		}

		/// <summary>
		/// Show interstitial ad.
		/// </summary>
		/// \note Show interstitial ad to the ad space which is loaded at last.
		public void Show ()
		{
			Interface.ShowInterstitialAd ("");
		}

		/// <summary>
		/// Show interstitial ad on specific ad space.
		/// </summary>
		/// <param name="spotId">Spot id.</param>
		public void Show (string spotId)
		{
			Interface.ShowInterstitialAd (spotId);
		}

		/// <summary>
		/// Dismiss interstitial ad from the screen.
		/// </summary>
		public void Dismiss ()
		{
			Interface.DismissInterstitialAd ();
		}

		void NendAdInterstitial_OnFinishLoad (string message)
		{
			string[] array = message.Split (':');
			if (2 != array.Length) {
				return;
			}
			var status = (NendAdInterstitialStatusCode)int.Parse (array [0]);
			string spotId = array [1];
			EventHandler<NendAdInterstitialLoadEventArgs> handler = AdLoaded;
			if (null != handler) {
				var args = new NendAdInterstitialLoadEventArgs ();
				args.StatusCode = status;
				args.SpotId = spotId;
				handler (this, args);
			}
		}

		void NendAdInterstitial_OnClickAd (string message)
		{
			string[] array = message.Split (':');
			if (2 != array.Length) {
				return;
			}
			var type = (NendAdInterstitialClickType)int.Parse (array [0]);
			string spotId = array [1];
			EventHandler<NendAdInterstitialClickEventArgs> handler = AdClicked;
			if (null != handler) {
				var args = new NendAdInterstitialClickEventArgs ();
				args.ClickType = type;
				args.SpotId = spotId;
				handler (this, args);
			}
		}

		void NendAdInterstitial_OnShowAd (string message)
		{
			string[] array = message.Split (':');
			if (2 != array.Length) {
				return;
			}
			var result = (NendAdInterstitialShowResult)int.Parse (array [0]);
			string spotId = array [1];
			EventHandler<NendAdInterstitialShowEventArgs> handler = AdShown;
			if (null != handler) {
				var args = new NendAdInterstitialShowEventArgs ();
				args.ShowResult = result;
				args.SpotId = spotId;
				handler (this, args);
			}
		}
	}

	/// <summary>
	/// Information of load event.
	/// </summary>
	public class NendAdInterstitialLoadEventArgs : EventArgs
	{
		/// <summary>
		/// Status of ad load.
		/// </summary>
		public NendAdInterstitialStatusCode StatusCode { get; set; }

		/// <summary>
		/// Spot id which event occurred.
		/// </summary>
		public String SpotId { get; set; }
	}

	/// <summary>
	/// Information of show event.
	/// </summary>
	public class NendAdInterstitialShowEventArgs : EventArgs
	{
		/// <summary>
		/// Result of ad show.
		/// </summary>
		public NendAdInterstitialShowResult ShowResult { get; set; }

		/// <summary>
		/// Spot id which event occurred.
		/// </summary>
		public String SpotId { get; set; }
	}

	/// <summary>
	/// Information of click event.
	/// </summary>
	public class NendAdInterstitialClickEventArgs : EventArgs
	{
		/// <summary>
		/// Type of ad click.
		/// </summary>
		public NendAdInterstitialClickType ClickType { get; set; }

		/// <summary>
		/// Spot id which event occurred.
		/// </summary>
		public String SpotId { get; set; }
	}
}
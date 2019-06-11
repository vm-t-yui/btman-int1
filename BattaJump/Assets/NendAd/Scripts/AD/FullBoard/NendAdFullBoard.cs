namespace NendUnityPlugin.AD.FullBoard
{
	using UnityEngine;
	using System;
	using Log = NendUnityPlugin.Common.NendAdLogger;

	/// <summary>
	/// FullBoard ad.
	/// </summary>
	public abstract class NendAdFullBoard : IDisposable
	{
		/// <summary>
		/// Delegate of ad loaded.
		/// </summary>
		public delegate void NendAdFullBoardLoaded (NendAdFullBoard instance);

		/// <summary>
		/// Delegate of failed to load ad.
		/// </summary>
		public delegate void NendAdFullBoardFailedToLoad (NendAdFullBoard instance, FullBoardAdErrorType error);

		/// <summary>
		/// Delegate of ad shown.
		/// </summary>
		public delegate void NendAdFullBoardShown (NendAdFullBoard instance);

		/// <summary>
		/// Delegate of ad clicked.
		/// </summary>
		public delegate void NendAdFullBoardClick (NendAdFullBoard instance);

		/// <summary>
		/// Delegate of ad Dismissed.
		/// </summary>
		public delegate void NendAdFullBoardDismiss (NendAdFullBoard instance);

		/// <summary>
		/// Occurs when ad loaded.
		/// </summary>
		public event NendAdFullBoardLoaded AdLoaded;
		/// <summary>
		/// Occurs when failed to load ad.
		/// </summary>
		public event NendAdFullBoardFailedToLoad AdFailedToLoad;
		/// <summary>
		/// Occurs when ad shown.
		/// </summary>
		public event NendAdFullBoardShown AdShown;
		/// <summary>
		/// Occurs when ad clicked.
		/// </summary>
		public event NendAdFullBoardClick AdClicked;
		/// <summary>
		/// Occurs when ad Dismissed.
		/// </summary>
		public event NendAdFullBoardDismiss AdDismissed;

		private bool m_isLoading = false;
		private bool m_isShowing = false;
		private bool m_isLoadSuccess = false;

		protected void CallBack (FullBoardAdCallbackType type)
		{
			switch (type) {
			case FullBoardAdCallbackType.LoadSuccess:
				m_isLoadSuccess = true;
				m_isLoading = false;
				if (null != AdLoaded) {
					AdLoaded (this);
				}
				break;
			case FullBoardAdCallbackType.AdShown:
				if (null != AdShown) {
					AdShown (this);
				}
				break;
			case FullBoardAdCallbackType.AdClicked:
				if (null != AdClicked) {
					AdClicked (this);
				}
				break;
			case FullBoardAdCallbackType.AdDismissed:
				m_isShowing = false;
				if (null != AdDismissed) {
					AdDismissed (this);
				}
				break;
			default:
				m_isLoadSuccess = false;
				m_isLoading = false;
				if (null != AdFailedToLoad) {
					AdFailedToLoad (this, (FullBoardAdErrorType)type);
				}
				break;
			}
		}

		protected enum FullBoardAdCallbackType : int
		{
			LoadSuccess = 0,
			LoadErrorFailedAdRequest,
			LoadErrorInvalidAdSpaces,
			LoadErrorFailedDownloadImage,
			AdShown,
			AdClicked,
			AdDismissed
		}

		/// <summary>
		/// Error type of load.
		/// </summary>
		public enum FullBoardAdErrorType : int
		{
			FailedAdRequest = 1,
			InvalidAdSpaces,
			FailedDownloadImage
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <param name="spotId">Spot id.</param>
		/// <param name="apiKey">API key.</param>
		public static NendAdFullBoard NewFullBoardAd (string spotId, string apiKey)
		{
			#if !UNITY_EDITOR && UNITY_IOS
			return new NendUnityPlugin.Platform.iOS.IOSFullBoardAd(spotId, apiKey);
			#elif !UNITY_EDITOR && UNITY_ANDROID
			return new NendUnityPlugin.Platform.Android.AndroidFullBoardAd(spotId, apiKey);
			#else
			return null;
			#endif
		}

		/// <summary>
		/// Load fullboard ad.
		/// </summary>
		public void Load ()
		{
			if (m_isLoading) {
				Log.W ("An ad is already loading.");
				return;
			}

			LoadInternal ();
			m_isLoading = true;
		}

		/// <summary>
		/// Show fullboard ad on the screen.
		/// </summary>
		public void Show ()
		{
			if (m_isShowing) {
				Log.W ("An ad is already showing.");
				return;
			}
			if (!m_isLoadSuccess) {
				Log.W ("There is no ad to show.");
				return;
			}

			ShowInternal ();
			m_isShowing = true;
		}

		private Color backgroundColor = Color.black;
		public Color IOSBackgroundColor
		{
			get {
				return backgroundColor;
			}
			set {
				backgroundColor = value;
			}
		}

		internal abstract void LoadInternal ();

		internal abstract void ShowInternal ();

		/// <summary>
		/// Dispose fullboard ad.
		/// </summary>
		public abstract void Dispose ();
	}
}


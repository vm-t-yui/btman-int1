namespace NendUnityPlugin.AD.Video
{
	using System;
	using Log = NendUnityPlugin.Common.NendAdLogger;

	/// <summary>
	/// Video ad.
	/// </summary>
	public abstract class NendAdVideo : IDisposable
	{
		/// <summary>
		/// Delegate of ad loaded.
		/// </summary>
		public delegate void NendAdVideoLoaded (NendAdVideo instance);

		/// <summary>
		/// Delegate of failed to load ad.
		/// </summary>
		public delegate void NendAdVideoFailedToLoad (NendAdVideo instance, int errorCode);

		/// <summary>
		/// Delegate of failed to play ad.
		/// </summary>
		public delegate void NendAdVideoFailedToPlay (NendAdVideo instance);

		/// <summary>
		/// Delegate of ad shown.
		/// </summary>
		public delegate void NendAdVideoShown (NendAdVideo instance);

		/// <summary>
		/// Delegate of ad started.
		/// </summary>
		public delegate void NendAdVideoStarted (NendAdVideo instance);

		/// <summary>
		/// Delegate of ad stopped.
		/// </summary>
		public delegate void NendAdVideoStopped (NendAdVideo instance);

		/// <summary>
		/// Delegate of ad completed.
		/// </summary>
		public delegate void NendAdVideoCompleted (NendAdVideo instance);

		/// <summary>
		/// Delegate of ad clicked.
		/// </summary>
		public delegate void NendAdVideoClick (NendAdVideo instance);

		/// <summary>
		/// Delegate of information clicked.
		/// </summary>
		public delegate void NendAdVideoInformationClick (NendAdVideo instance);

		/// <summary>
		/// Delegate of ad Dismissed.
		/// </summary>
		public delegate void NendAdVideoClosed (NendAdVideo instance);

		/// <summary>
		/// Occurs when ad loaded.
		/// </summary>
		public event NendAdVideoLoaded AdLoaded;
		/// <summary>
		/// Occurs when failed to load ad.
		/// </summary>
		public event NendAdVideoFailedToLoad AdFailedToLoad;
		/// <summary>
		/// Occurs when failed to play ad.
		/// </summary>
		public event NendAdVideoFailedToPlay AdFailedToPlay;
		/// <summary>
		/// Occurs when ad shown.
		/// </summary>
		public event NendAdVideoShown AdShown;
		/// <summary>
		/// Occurs when ad started.
		/// </summary>
		public event NendAdVideoClick AdStarted;
		/// <summary>
		/// Occurs when ad stopped.
		/// </summary>
		public event NendAdVideoClick AdStopped;
		/// <summary>
		/// Occurs when ad completed.
		/// </summary>
		public event NendAdVideoClick AdCompleted;
		/// <summary>
		/// Occurs when ad clicked.
		/// </summary>
		public event NendAdVideoClick AdClicked;
		/// <summary>
		/// Occurs when information clicked.
		/// </summary>
		public event NendAdVideoClick InformationClicked;
		/// <summary>
		/// Occurs when ad Dismissed.
		/// </summary>
		public event NendAdVideoClosed AdClosed;

		protected virtual void CallBack (VideoAdCallbackArgments args)
		{
			switch (args.videoAdCallbackType) {
			case VideoAdCallbackType.LoadSuccess:
				if (null != AdLoaded) {
					AdLoaded (this);
				}
				break;
			case VideoAdCallbackType.FailedToLoad:
				if (null != AdFailedToLoad) {
					ErrorVideoAdCallbackArgments errorArg = (ErrorVideoAdCallbackArgments)args; 
					Log.E ("FailedToLoad errorCode = " + errorArg.errorCode);
					AdFailedToLoad (this, errorArg.errorCode);
				}
				break;
			case VideoAdCallbackType.FailedToPlay:
				if (null != AdFailedToPlay) {
					AdFailedToPlay (this);
				}
				break;
			case VideoAdCallbackType.AdShown:
				if (null != AdShown) {
					AdShown (this);
				}
				break;
			case VideoAdCallbackType.AdClosed:
				if (null != AdClosed) {
					AdClosed (this);
				}
				break;
			case VideoAdCallbackType.AdStarted:
				if (null != AdStarted) {
					AdStarted (this);
				}
				break;
			case VideoAdCallbackType.AdStopped:
				if (null != AdStopped) {
					AdStopped (this);
				}
				break;
			case VideoAdCallbackType.AdCompleted:
				if (null != AdCompleted) {
					AdCompleted (this);
				}
				break;
			case VideoAdCallbackType.AdClicked:
				if (null != AdClicked) {
					AdClicked (this);
				}
				break;
			case VideoAdCallbackType.InformationClicked:
				if (null != InformationClicked) {
					InformationClicked (this);
				}
				break;
			}
		}

		internal enum VideoAdCallbackType : int
		{
			//Basic
			LoadSuccess = 0,
			FailedToLoad,
			FailedToPlay,
			AdShown,
			AdClosed,
			AdStarted,
			AdStopped,
			AdCompleted,
			AdClicked,
			InformationClicked,
			END,

			//Rewarded
			Rewarded = VideoAdCallbackType.END,
			ENDREWARD

			//Interstital
			//nothing
		}

		/// <summary>
		/// Load video ad.
		/// </summary>
		public void Load ()
		{
			LoadInternal ();
		}

		/// <summary>
		/// Check loading is completed.
		/// </summary>
		public bool IsLoaded ()
		{
			return IsLoadedInternal ();
		}

		/// <summary>
		/// Show video ad on the screen.
		/// </summary>
		public void Show ()
		{
			ShowInternal ();
		}

		/// <summary>
		/// Release video ad object.
		/// </summary>
		public void Release ()
		{
			ReleaseInternal ();
		}

		/// <summary>
		/// Set mediation name.
		/// </summary>
		public string MediationName
		{
			set {
				SetMediationNameInternal (value);
			}
		}

		/// <summary>
		/// Set user id.
		/// </summary>
		public string UserId
		{
			set {
				SetUserIdInternal (value);
			}
		}

		public NendAdUserFeature UserFeature
		{
			set {
				SetUserFeatureInternal (value);
			}
		}

        public bool IsLocationEnabled
        {
            set {
                SetLocationEnabledInternal(value);
            }
        }

		#if UNITY_IOS
		/// <summary>
		/// Set log put for iOS.
		/// </summary>
		//Note: NendAdLoggerへ置き換え完了したら削除予定
		public bool IsOutputLog
		{
			set {
				SetOutputLog (value);
			}
		}
		#endif

		internal abstract void LoadInternal ();
		internal abstract bool IsLoadedInternal ();
		internal abstract void ShowInternal ();
		internal abstract void ReleaseInternal ();
		internal abstract void SetMediationNameInternal (string mediationName);
		internal abstract void SetUserIdInternal (string userId);
		internal abstract void SetUserFeatureInternal (NendAdUserFeature userFeature);
        internal abstract void SetLocationEnabledInternal(bool enabled);
		#if UNITY_IOS
		internal abstract void SetOutputLog (bool logSetting);
		#endif

		/// <summary>
		/// Dispose video ad.
		/// </summary>
		public abstract void Dispose ();
	}
}


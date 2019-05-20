#if UNITY_IOS
namespace NendUnityPlugin.Platform.iOS
{
	using System;
	using System.Runtime.InteropServices;
	using NendUnityPlugin.AD.FullBoard;

	internal class IOSFullBoardAd : NendAdFullBoard
	{
		private delegate void NendUnityFullBoardAdCallback (IntPtr selfPtr, FullBoardAdCallbackType type);

		private IntPtr m_iOSFullBoardAdPtr;

		internal IOSFullBoardAd (string spotId, string apiKey) : base ()
		{
			m_iOSFullBoardAdPtr = _CreateFullBoardAd (spotId, apiKey);
		}

		internal override void LoadInternal ()
		{
			IntPtr selfPtr = (IntPtr)GCHandle.Alloc (this);
			_LoadFullBoardAd (selfPtr, m_iOSFullBoardAdPtr, FullBoardAdCallback);
		}

		internal override void ShowInternal ()
		{
			IntPtr selfPtr = (IntPtr)GCHandle.Alloc (this);
			_SetFullBoardAdBackgroundColor (m_iOSFullBoardAdPtr, IOSBackgroundColor.r, IOSBackgroundColor.g, IOSBackgroundColor.b, IOSBackgroundColor.a);
			_ShowFullBoardAd (selfPtr, m_iOSFullBoardAdPtr);
		}

		[AOT.MonoPInvokeCallbackAttribute (typeof(NendUnityFullBoardAdCallback))]
		private static void FullBoardAdCallback (IntPtr selfPtr, FullBoardAdCallbackType type)
		{
			GCHandle handle = (GCHandle)selfPtr;
			IOSFullBoardAd instance = (IOSFullBoardAd)handle.Target;

			switch (type) {
			case FullBoardAdCallbackType.LoadSuccess:
			case FullBoardAdCallbackType.LoadErrorFailedAdRequest:
			case FullBoardAdCallbackType.LoadErrorInvalidAdSpaces:
			case FullBoardAdCallbackType.LoadErrorFailedDownloadImage:
			case FullBoardAdCallbackType.AdDismissed:
				handle.Free ();
				break;
			default:
				break;
			}

			instance.CallBack (type);
		}

		~IOSFullBoardAd ()
		{
			Dispose ();
		}

		public override void Dispose ()
		{
			_ReleaseFullBoardAd (m_iOSFullBoardAdPtr);
			NendUnityPlugin.Common.NendAdLogger.D ("Dispose IOSFullBoardAd.");
		}

		[DllImport ("__Internal")]
		private static extern IntPtr _CreateFullBoardAd (string spotId, string apiKey);

		[DllImport ("__Internal")]
		private static extern void _LoadFullBoardAd (IntPtr self, IntPtr iOSFullBoardPtr, NendUnityFullBoardAdCallback callback);

		[DllImport ("__Internal")]
		private static extern void _ShowFullBoardAd (IntPtr self, IntPtr iOSFullBoardPtr);

		[DllImport ("__Internal")]
		private static extern void _SetFullBoardAdBackgroundColor (IntPtr iOSFullBoardPtr, float r, float g, float b, float a);

		[DllImport ("__Internal")]
		private static extern void _ReleaseFullBoardAd (IntPtr iOSFullBoardPtr);
	}
}
#endif


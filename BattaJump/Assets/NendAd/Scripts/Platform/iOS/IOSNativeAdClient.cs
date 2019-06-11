#if UNITY_IOS
namespace NendUnityPlugin.Platform.iOS
{
	using System;
	using System.Runtime.InteropServices;
	using System.Collections.Generic;
	using NendUnityPlugin.AD.Native;
	using Log = NendUnityPlugin.Common.NendAdLogger;

	internal class IOSNativeAdClient : NativeAdClient
	{
		internal delegate void NendUnityNativeAdCallback (IntPtr client, IntPtr nativeAd, int code, string message);

		private IntPtr m_ClientPtr;

		internal IOSNativeAdClient (string apiKey, string spotId) : base ()
		{
			m_ClientPtr = _CreateNativeAdClient (apiKey, spotId);
		}

		~IOSNativeAdClient ()
		{
			Dispose ();
		}

		public override void Dispose ()
		{
			base.Dispose ();
			Log.D ("Dispose IOSNativeAdClient.");
			_ReleaseNativeAdClient (m_ClientPtr);
			if (IntPtr.Zero != m_ClientPtr) {
				GCHandle handle = (GCHandle)m_ClientPtr;
				handle.Free ();
				m_ClientPtr = IntPtr.Zero;
			}
		}

		public override void LoadNativeAd (Action<INativeAd, int, string> callback)
		{
			EnqueueCallback (callback);
			IntPtr selfPtr = (IntPtr)GCHandle.Alloc (this);
			_LoadNativeAd (selfPtr, m_ClientPtr, NativeAdCallback);
		}

		[AOT.MonoPInvokeCallbackAttribute (typeof(NendUnityNativeAdCallback))]
		private static void NativeAdCallback (IntPtr client, IntPtr nativeAd, int code, string message)
		{
			GCHandle handle = (GCHandle)client;
			IOSNativeAdClient self = handle.Target as IOSNativeAdClient;
			INativeAd ad = (200 == code) ? new IOSNativeAd (nativeAd) : null;
			self.DeliverResponseOnMainThread (ad, code, message);
			handle.Free ();
		}

		[DllImport ("__Internal")]
		private static extern IntPtr _CreateNativeAdClient (string apiKey, string spotId);

		[DllImport ("__Internal")]
		private static extern void _ReleaseNativeAdClient (IntPtr iosClient);

		[DllImport ("__Internal")]
		private static extern void _LoadNativeAd (IntPtr self, IntPtr iosClient, NendUnityNativeAdCallback callback);
	}
}
#endif
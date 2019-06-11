#if UNITY_IOS
namespace NendUnityPlugin.Platform.iOS
{
	using System.Runtime.InteropServices;

	internal class IOSLoggerInterface : NendAdLoggerInterface
	{
		public void SetLogLevel (int level)
		{
			_SetLogLevel (level);
		}

		[DllImport ("__Internal")]
		private static extern void _SetLogLevel (int level);
	}
}
#endif

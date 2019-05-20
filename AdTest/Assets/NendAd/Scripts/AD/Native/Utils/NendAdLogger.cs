namespace NendUnityPlugin.AD.Native.Utils
{
	using System;

	[Obsolete ("Use `NendUnityPlugin.Common.NendAdLogger` instead.", false)]
	public class NendAdLogger
	{
		[Obsolete ("Use `NendUnityPlugin.Common.NendAdLogger.NendAdLogLevel` instead.", false)]
		public enum NendAdLogLevel : int
		{
			Debug = 0,
			Info = 1,
			Warn = 2,
			Error = 3,
			None = int.MaxValue
		}

		[Obsolete ("Use `NendUnityPlugin.Common.NendAdLogger.LogLevel` instead.", false)]
		public static NendAdLogLevel LogLevel {
			set {
				NendUnityPlugin.Common.NendAdLogger.LogLevel = (NendUnityPlugin.Common.NendAdLogger.NendAdLogLevel)value;
			}
		}
	}
}
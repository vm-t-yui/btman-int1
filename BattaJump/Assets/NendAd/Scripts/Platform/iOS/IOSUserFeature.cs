#if UNITY_IOS
namespace NendUnityPlugin.Platform.iOS
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;
	using NendUnityPlugin.AD.Video;

	internal class IOSUserFeature : NendAdUserFeature
	{
		private IntPtr m_iOSUserFeaturePtr;

		internal IOSUserFeature () : base ()
		{
			m_iOSUserFeaturePtr = _InitNendUserFeatureUserFeatureObj ();
		}

		internal IntPtr BuildNendUserFeature(NendAdUserFeature userFeature) {
			_SetNendUserFeatureGender (m_iOSUserFeaturePtr, userFeature.gender.GetHashCode());
			_SetNendUserFeatureBirthday (m_iOSUserFeaturePtr, userFeature.YearOfBirth, userFeature.MonthOfBirth, userFeature.DayOfBirth);
			_SetNendUserFeatureAge (m_iOSUserFeaturePtr, userFeature.age);

			foreach(KeyValuePair<string, int> pair in userFeature.CustomFeaturesInt){
				_AddNendUserFeatureCustomFeatureInt(m_iOSUserFeaturePtr, pair.Key, pair.Value);
			}
			foreach(KeyValuePair<string, double> pair in userFeature.CustomFeaturesDouble){
				_AddNendUserFeatureCustomFeatureDouble(m_iOSUserFeaturePtr, pair.Key, pair.Value);
			}
			foreach(KeyValuePair<string, string> pair in userFeature.CustomFeaturesString){
				_AddNendUserFeatureCustomFeatureString(m_iOSUserFeaturePtr, pair.Key, pair.Value);
			}
			foreach(KeyValuePair<string, bool> pair in userFeature.CustomFeaturesBool){
				_AddNendUserFeatureCustomFeatureBool(m_iOSUserFeaturePtr, pair.Key, pair.Value);
			}

			return m_iOSUserFeaturePtr;
		}

		~IOSUserFeature ()
		{
			if (m_iOSUserFeaturePtr != IntPtr.Zero) {
				_DisposeNendUserFeaturePtr (m_iOSUserFeaturePtr);
				m_iOSUserFeaturePtr = IntPtr.Zero;
			}
		}

		[DllImport ("__Internal")]
		private static extern IntPtr _InitNendUserFeatureUserFeatureObj ();

		[DllImport ("__Internal")]
		private static extern void _SetNendUserFeatureGender (IntPtr iOSUserFeaturePtr, int gender);

		[DllImport ("__Internal")]
		private static extern void _SetNendUserFeatureBirthday (IntPtr iOSUserFeaturePtr, int yearOfBirth, int monthOfBirth, int dayOfBirth);

		[DllImport ("__Internal")]
		private static extern void _SetNendUserFeatureAge (IntPtr iOSUserFeaturePtr, int age);

		[DllImport ("__Internal")]
		private static extern void _AddNendUserFeatureCustomFeatureInt (IntPtr iOSUserFeaturePtr, string key, int value);

		[DllImport ("__Internal")]
		private static extern void _AddNendUserFeatureCustomFeatureDouble (IntPtr iOSUserFeaturePtr, string key, double value);

		[DllImport ("__Internal")]
		private static extern void _AddNendUserFeatureCustomFeatureString (IntPtr iOSUserFeaturePtr, string key, string value);

		[DllImport ("__Internal")]
		private static extern void _AddNendUserFeatureCustomFeatureBool (IntPtr iOSUserFeaturePtr, string key, bool value);

		[DllImport ("__Internal")]
		private static extern void _DisposeNendUserFeaturePtr (IntPtr iOSUserFeaturePtr);
	}
}
#endif

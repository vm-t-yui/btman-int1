#if UNITY_ANDROID
namespace NendUnityPlugin.Platform.Android
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using NendUnityPlugin.AD.Video;

	internal class VideoMethodUtils
	{
		internal static void LoadAd (AndroidJavaObject androidJavaObj, AndroidVideoAdListener listener)
		{
			androidJavaObj.Call ("loadAd", listener);
		}

		internal static bool IsLoaded (AndroidJavaObject androidJavaObj)
		{
			return androidJavaObj.Call <bool>("isLoaded");
		}

		internal static void ShowAd (AndroidJavaObject androidJavaObj)
		{
			using (var activity = CommonUtils.GetCurrentActivity()) {
				androidJavaObj.Call ("showAd", activity);
			}
		}

		internal static void ReleaseAd (AndroidJavaObject androidJavaObj)
		{
			androidJavaObj.Call ("releaseAd");
		}

		internal static void SetMediationName (AndroidJavaObject androidJavaObj, string mediationName) {
			androidJavaObj.Call ("setMediationName", mediationName);
		}

		internal static void SetUserId (AndroidJavaObject androidJavaObj, string userId) {
			androidJavaObj.Call ("setUserId", userId);
		}

		internal static void SetUserFeature (AndroidJavaObject androidJavaObj, NendAdUserFeature userFeature) {
			AndroidJavaObject builderObj = new AndroidJavaObject("net.nend.android.NendAdUserFeature$Builder");
			SetAndroidJavaGender (builderObj, userFeature.gender);
			builderObj.Call<AndroidJavaObject>("setAge", userFeature.age);

			builderObj.Call<AndroidJavaObject>("setBirthday", 
				userFeature.YearOfBirth, userFeature.MonthOfBirth, userFeature.DayOfBirth);

			foreach(KeyValuePair<string, int> pair in userFeature.CustomFeaturesInt){
				builderObj.Call<AndroidJavaObject>("addCustomFeature", pair.Key, pair.Value);
			}
			foreach(KeyValuePair<string, double> pair in userFeature.CustomFeaturesDouble){
				builderObj.Call<AndroidJavaObject>("addCustomFeature", pair.Key, pair.Value);
			}
			foreach(KeyValuePair<string, string> pair in userFeature.CustomFeaturesString){
				builderObj.Call<AndroidJavaObject>("addCustomFeature", pair.Key, pair.Value);
			}
			foreach(KeyValuePair<string, bool> pair in userFeature.CustomFeaturesBool){
				builderObj.Call<AndroidJavaObject>("addCustomFeature", pair.Key, pair.Value);
			}

			androidJavaObj.Call ("setUserFeature", builderObj.Call<AndroidJavaObject>("build"));
		}

        internal static void SetLocationEnabled(AndroidJavaObject androidJavaObj, bool enabled)
        {
            androidJavaObj.Call("setLocationEnabled", enabled);
        }

        internal static void SetMuteStartPlaying(AndroidJavaObject androidJavaObj, bool enabled)
        {
            androidJavaObj.Call("setMuteStartPlaying", enabled);
        }

        private static void SetAndroidJavaGender(AndroidJavaObject builderObj, NendAdUserFeature.Gender gender) {
			string genderName;
			switch (gender) {
			case NendAdUserFeature.Gender.Female:
				genderName = "FEMALE";
				break;
			case NendAdUserFeature.Gender.Male:
				genderName = "MALE";
				break;
			default:
				//Do NOTHING!
				return;
			}
			builderObj.Call<AndroidJavaObject>("setGender", new AndroidJavaClass("net.nend.android.NendAdUserFeature$Gender").GetStatic<AndroidJavaObject>(genderName));
		}

		internal static void AddFallbackFullboard (AndroidJavaObject androidJavaObj, string spotId, string apiKey) {
			androidJavaObj.Call ("addFallbackFullboard", int.Parse (spotId), apiKey);
		}
	}
}
#endif



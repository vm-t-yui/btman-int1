namespace NendUnityPlugin.AD.Video
{
	using System;
	using System.Collections.Generic;

	public class NendAdUserFeature {
		public Gender gender;
		private int dayOfBirth;
		private int monthOfBirth;
		private int yearOfBirth;
		public int age = -1;
		private Dictionary<string, int> customFeaturesIntDic = new Dictionary<string, int>();
		private Dictionary<string, double> customFeaturesDoubleDic = new Dictionary<string, double>();
		private Dictionary<string, string> customFeaturesStringDic = new Dictionary<string, string>();
		private Dictionary<string, bool> customFeaturesBoolDic = new Dictionary<string, bool>();

		private const int GENDER_START_INDEX = 1;
		public enum Gender {
			Male = GENDER_START_INDEX, 
			Female
		}

		public void SetBirthday(int year, int month, int day) {
			yearOfBirth = year;
			monthOfBirth = month;
			dayOfBirth = day;
		}

		internal int YearOfBirth
		{
			get
			{
				return yearOfBirth;
			}
		}

		internal int MonthOfBirth
		{
			get
			{
				return monthOfBirth;
			}
		}

		internal int DayOfBirth
		{
			get
			{
				return dayOfBirth;
			}
		}

		public void AddCustomFeature(string key, int value) {
			customFeaturesIntDic[key] = value;
		}

		public void AddCustomFeature(string key, double value) {
			customFeaturesDoubleDic[key] = value;
		}

		public void AddCustomFeature(string key, string value) {
			customFeaturesStringDic[key] = value;
		}

		public void AddCustomFeature(string key, bool value) {
			customFeaturesBoolDic[key] = value;
		}

		internal Dictionary<string, int> CustomFeaturesInt
		{
			get
			{
				return customFeaturesIntDic;
			}
		}

		internal Dictionary<string, double> CustomFeaturesDouble
		{
			get
			{
				return customFeaturesDoubleDic;
			}
		}

		internal Dictionary<string, string> CustomFeaturesString
		{
			get
			{
				return customFeaturesStringDic;
			}
		}

		internal Dictionary<string, bool> CustomFeaturesBool
		{
			get
			{
				return customFeaturesBoolDic;
			}
		}

		protected NendAdUserFeature() {
		}

		/// <summary>
		/// Gets the instance.
		public static NendAdUserFeature NewNendAdUserFeature ()
		{
			#if !UNITY_EDITOR && UNITY_IOS
			return new NendUnityPlugin.Platform.iOS.IOSUserFeature();
			#else
			return new NendAdUserFeature();
			#endif
		}
	}
}


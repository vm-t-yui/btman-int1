using System;
using UnityEngine;

using NendUnityPlugin.AD;

namespace NendUnityPlugin.Common
{
	/// <summary>
	/// Utility class for this plugin.
	/// </summary>
	public class NendUtils
	{
		[SerializableAttribute]
		internal class NendID
		{
			[SerializeField]
			internal string apiKey;
			[SerializeField]
			internal int spotID;
		}

		[SerializableAttribute]
		internal class Account
		{
			[SerializeField]
			internal NendID android;
			[SerializeField]
			internal NendID iOS;
		}

		/// <summary>
		/// Gets NendAdBanner instance.
		/// </summary>
		/// <returns>NendAdBanner instance.</returns>
		/// <param name="gameObjectName">Name of GameObject which added NendAdBanner script.</param>
		/// <example>
		/// Code example.
		/// <code>
		/// NendAdBanner banner = NendUtils.GetBannerComponent ("your GameObject name");
		/// </code>
		/// </example>
		/// \sa NendUnityPlugin.AD.NendAdBanner
		public static NendAdBanner GetBannerComponent (string gameObjectName)
		{
			return GetNendComponent<NendAdBanner> (gameObjectName);
		}

#if UNITY_ANDROID
		/// <summary>
		/// Gets NendAdIcon instance.
		/// </summary>
		/// <returns>NendAdIcon instance.</returns>
		/// <param name="gameObjectName">Name of GameObject which added NendAdIcon script.</param>
		/// <example>
		/// Code example.
		/// <code>
		/// NendAdIcon icon = NendUtils.GetIconComponent ("your GameObject name");
		/// </code>
		/// </example>
		/// \sa NendUnityPlugin.AD.NendAdIcon
		public static NendAdIcon GetIconComponent (string gameObjectName)
		{
			return GetNendComponent<NendAdIcon> (gameObjectName);
		}
#endif

		private static T GetNendComponent<T> (string gameObjectName) where T : NendAd
		{
			var gameObject = GameObject.Find (gameObjectName);
			if (null != gameObject) {
				return gameObject.GetComponent<T> ();
			} else {
				return null;
			}
		}
	}
}

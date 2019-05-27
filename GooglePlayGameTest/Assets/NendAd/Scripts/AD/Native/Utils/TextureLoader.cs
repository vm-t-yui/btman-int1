namespace NendUnityPlugin.AD.Native.Utils
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	using Log = NendUnityPlugin.Common.NendAdLogger;

	internal class TextureLoader
	{
		internal static IEnumerator LoadTexture (string url, Action<Texture2D> callback)
		{
			Texture2D texture = TextureCache.Instance.Get (url);
			if (null != texture) {
				callback (texture);
				yield break;
			}
			using (var www = new WWW (url)) {
				yield return www;
				if (string.IsNullOrEmpty (www.error)) {
					texture = www.texture;
					TextureCache.Instance.Put (url, texture);
				} else {
					Log.E ("Failed to download image: {0}", www.error);
				}
				callback (texture);
			}
		}

		private class TextureCache
		{
			private static TextureCache s_Instance = null;
			private Dictionary<string, Texture2D> m_Cache;

			internal static TextureCache Instance {
				get {
					return s_Instance ?? (s_Instance = new TextureCache ());
				}
			}

			private TextureCache ()
			{
				m_Cache = new Dictionary<string, Texture2D> ();
			}

			internal Texture2D Get (string url)
			{
				if (m_Cache.ContainsKey (url)) {
					Log.D ("Get texture from cache.");
					return m_Cache [url];
				} else {
					return null;
				}
			}

			internal void Put (string url, Texture2D texture)
			{
				if (null != texture) {
					m_Cache [url] = texture;
				}
			}
		}
	}
}
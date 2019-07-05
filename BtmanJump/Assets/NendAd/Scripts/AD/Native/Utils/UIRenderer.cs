namespace NendUnityPlugin.AD.Native.Utils
{
	using UnityEngine;
	using UnityEngine.UI;

	internal class UIRenderer
	{
		internal static void TryRenderText (Text target, string text)
		{
			if (null != target) {
				target.text = text;
			}
		}

		internal static void TryRenderImage (MonoBehaviour behaviour, RawImage target, string url)
		{
			if (null != target && !string.IsNullOrEmpty (url) && behaviour.isActiveAndEnabled) {
				behaviour.StartCoroutine (TextureLoader.LoadTexture (url, texture => {
					target.texture = texture;
				}));
			}
		}

		internal static void TryClearText (Text target)
		{
			TryRenderText (target, string.Empty);
		}

		internal static void TryClearImage (RawImage target)
		{
			if (null != target) {
				target.texture = null;
			}
		}
	}
}

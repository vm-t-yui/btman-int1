namespace NendUnityPlugin.AD.Native
{
	/// <summary>
	/// The text for clarification of ad.
	/// </summary>
	public enum AdvertisingExplicitly : int
	{
		/// <summary>
		/// Displayed with the "PR".
		/// </summary>
		PR = 0,
		/// <summary>
		/// Displayed with the "Sponsored".
		/// </summary>
		Sponsored,
		/// <summary>
		/// Displayed with the "広告".
		/// </summary>
		AD,
		/// <summary>
		/// Displayed with the "プロモーション".
		/// </summary>
		Promotion
	}

	/// <summary>
	/// Native ad.
	/// </summary>
	public interface INativeAd
	{
		/// <summary>
		/// Gets the short text.
		/// </summary>
		/// <value>The short text.</value>
		string ShortText { 
			get;
		}

		/// <summary>
		/// Gets the long text.
		/// </summary>
		/// <value>The long text.</value>
		string LongText { 
			get; 
		}

		/// <summary>
		/// Gets the promotion URL.
		/// </summary>
		/// <value>The promotion URL.</value>
		string PromotionUrl { 
			get;
		}

		/// <summary>
		/// Gets the name of the promotion.
		/// </summary>
		/// <value>The name of the promotion.</value>
		string PromotionName {
			get;
		}

		/// <summary>
		/// Gets the action button text.
		/// </summary>
		/// <value>The action button text, for example 'Install'.</value>
		string ActionButtonText {
			get;
		}

		/// <summary>
		/// Gets the ad image URL.
		/// </summary>
		/// <value>The ad image URL.</value>
		string AdImageUrl {
			get;
		}

		/// <summary>
		/// Gets the logo image URL.
		/// </summary>
		/// <value>The logo image URL.</value>
		string LogoImageUrl {
			get;
		}
			
		/// <summary>
		/// Gets the advertising explicitly text.
		/// </summary>
		/// <returns>The advertising explicitly text.</returns>
		/// <param name="advertisingExplicitly">AdvertisingExplicitly.</param>
		string GetAdvertisingExplicitlyText (AdvertisingExplicitly advertisingExplicitly);

		/// <summary>
		/// Loads the ad image.
		/// </summary>
		/// <returns>The ad image.</returns>
		/// <param name="callback">Callback.</param>
		System.Collections.IEnumerator LoadAdImage(System.Action<UnityEngine.Texture2D> callback);

		/// <summary>
		/// Loads the logo image.
		/// </summary>
		/// <param name="callback">Callback.</param>
		System.Collections.IEnumerator LoadLogoImage(System.Action<UnityEngine.Texture2D> callback);

		/// <summary>
		/// Activate the specified adGameObject and prGameObject.
		/// </summary>
		/// <param name="adGameObject">Ad game object.</param>
		/// <param name="prGameObject">Pr game object.</param>
		void Activate (UnityEngine.GameObject adGameObject, UnityEngine.GameObject prGameObject);

		/// <summary>
		/// Into the specified adView.
		/// </summary>
		/// <param name="adView">Ad view.</param>
		void Into (NendUnityPlugin.AD.Native.NendAdNativeView adView);

		/// <summary>
		/// Occurs when ad clicked.
		/// </summary>
		event System.EventHandler AdClicked;
	}
}
namespace NendUnityPlugin.AD.Native
{
	/// <summary>
	/// Native ad client.
	/// </summary>
	public interface INativeAdClient
	{
		/// <summary>
		/// Loads the native ad.
		/// </summary>
		/// <param name="callback">Callback.</param>
		void LoadNativeAd (System.Action<INativeAd, int, string> callback);

		/// <summary>
		/// Enables the auto reload.
		/// </summary>
		/// <param name="interval">Reload Interval（millisecond）.</param>
		/// <param name="callback">Callback.</param>
		/// \note Reload interval is valid for more than 30 seconds.
		void EnableAutoReload (double interval, System.Action<INativeAd, int, string> callback);

		/// <summary>
		/// Disables the auto reload.
		/// </summary>
		void DisableAutoReload ();
	}
}
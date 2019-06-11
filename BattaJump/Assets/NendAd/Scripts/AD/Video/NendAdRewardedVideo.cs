using NendUnityPlugin.Platform;

namespace NendUnityPlugin.AD.Video
{
	/// <summary>
	/// Video ad.
	/// </summary>
	public abstract class NendAdRewardedVideo : NendAdVideo
	{
		/// <summary>
		/// Delegate of ad Rewarded.
		/// </summary>
		public delegate void NendAdVideoRewarded (NendAdVideo instance, NendAdRewardedItem item);

		/// <summary>
		/// Occurs when ad Rewarded.
		/// </summary>
		public event NendAdVideoRewarded Rewarded;

		protected virtual void CallBack (RewardedVideoAdCallbackArgments args)
		{
			switch (args.videoAdCallbackType) {
			case VideoAdCallbackType.Rewarded:
				if (null != Rewarded) {
					Rewarded (this, args.rewardedItem);
				}
				break;
			default:
				base.CallBack (args);
				break;
			}
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <param name="spotId">Spot id.</param>
		/// <param name="apiKey">API key.</param>
		public static NendAdRewardedVideo NewVideoAd (string spotId, string apiKey)
		{
			return NendAdNativeInterfaceFactory.CreateRewardedVideoAd (spotId, apiKey);
		}
	}
}


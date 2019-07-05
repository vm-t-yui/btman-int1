namespace NendUnityPlugin.AD.Video
{
	public class RewardedVideoAdCallbackArgments : VideoAdCallbackArgments
	{
		public NendAdRewardedItem rewardedItem;

		internal RewardedVideoAdCallbackArgments(NendAdVideo.VideoAdCallbackType type, NendAdRewardedItem item) : base(type)
		{
			rewardedItem = item;
		}
	}
}



namespace NendUnityPlugin.AD.Video
{
	/// <summary>
	/// Video ad.
	/// </summary>
	public class NendAdRewardedItem
	{
		public readonly string currencyName;
		public readonly int currencyAmount;

		internal NendAdRewardedItem (string name, int amount)
		{
			currencyName = name;
			currencyAmount = amount;
		}
	}
}



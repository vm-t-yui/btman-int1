using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;    // Google AdMob広告用

/// <summary>
/// 広告管理クラス
/// </summary>
public class AdManager : MonoBehaviour
{
    [SerializeField]
    AdBannerController         adBanner         = default;            // バナー広告コントロールクラス
    [SerializeField]
    AdInterstitialController   adInterstitial   = default;            // インタースティシャル広告コントロールクラス
    [SerializeField]
    NendInterstitialController nendInterstitial = default;            // nendインタースティシャル広告コントロールクラス
    [SerializeField]
    OwnCompAdInterstitialController ownCompInterstitial = default;    // 自社アプリインタースティシャル広告コントロールクラス

    int showCount = 0;                                                // インタースティシャル用表示回数
    const string ShowCountKey = "ShowCount";                          // 表示回数データのキー

    const int OwnCompAdCount = 4;                                     // 自社広告使用時の表示回数
    const int AdMobCount     = 3;                                     // AdMob使用時の表示回数
    const int RewardCount    = 5;                                     // 動画リワード使用時の表示回数

    const string AppId =                                              // アプリID
#if UNITY_ANDROID
        "ca-app-pub-7073050807259252~7297201289";
#elif UNITY_IOS
        "ca-app-pub-7073050807259252~7875785788";
#else
        "unexpected_platform";
#endif

    public bool IsAdView { get; private set; }                        // 広告表示してるかどうか 

    bool isOnline = false;                                            // オンラインかどうか

    [SerializeField]
    bool isResult = false;

    /// <summary>
    /// ロード完了検知
    /// </summary>
    /// <returns></returns>
    public bool IsLoaded()
    {
        // バナー、インタースティシャルどちらもロードが完了したらtrueを返す、オフラインの場合も同様
        if (adBanner.IsLoaded && (adInterstitial.IsLoaded() || !isOnline))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 広告生成
    /// </summary>
    void OnEnable()
    {
        // オンラインかどうか判断
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            isOnline = false;
        }
        else
        {
            isOnline = true;
        }

        // オフラインなら処理を抜ける
        if (!isOnline) { return; }

        // Google Mobile Ads SDKを設定したアプリIDで初期化
        MobileAds.Initialize(AppId);

        // バナー広告を生成
        adBanner.RequestBanner();

	    // インタースティシャル広告を生成
		adInterstitial.RequestInterstitial();
		nendInterstitial.Load();

        // 一度バナー広告を非表示にする
        HideBanner();
    }

    /// <summary>
    /// バナー広告表示
    /// </summary>
    public void ShowBanner()
    {
        // オフラインなら処理を抜ける
        if (!isOnline) { return; }

        adBanner.Show();
    }

    /// <summary>
    /// バナー広告非表示
    /// </summary>
    public void HideBanner()
    {
        // オフラインなら処理を抜ける
        if (!isOnline) { return; }

        adBanner.Hide();
    }

    /// <summary>
    /// インタースティシャル広告表示
    /// </summary>
    public void ShowInterstitial()
    {
        // オフラインなら処理を抜ける
        if (!isOnline) { return; }

        // 表示回数をロード
        showCount = PlayerPrefs.GetInt(ShowCountKey, 1);

        // 4回毎に自社広告を使用
		if (showCount % OwnCompAdCount == 0)
		{
            ownCompInterstitial.enabled = true;
		}
        // 3回毎にAdMobを使用
		else if (showCount % AdMobCount == 0)
		{
			// 閉じているなら表示する
			if (adInterstitial.IsClosed)
			{
				adInterstitial.Show();
			}
		}
        // 上記以外ならnendを使用、5回毎の動画リワードを出す際は表示しない
        else if (showCount % RewardCount != 0)
        {
            nendInterstitial.Show();
        }

        // 表示回数をカウント
        showCount++;
        // 5回毎(動画リワードの番が来るたび)に初期化
        if (showCount > RewardCount)
        {
            showCount = 1;
        }

        // 表示回数をセーブ
        PlayerPrefs.SetInt(ShowCountKey, showCount);
        PlayerPrefs.Save();
    }
}

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
    AdBannerController adBanner = default;                            // バナー広告テストクラス
    [SerializeField]
    AdInterstitialController adInterstitial = default;                // インタースティシャル広告テストクラス

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

    /// <summary>
    /// ロード完了検知
    /// </summary>
    /// <returns></returns>
    public bool IsLoaded()
    {
        // バナー、インタースティシャルどちらもロードが完了したらtrueを返す、オフラインの場合も同様
        if ((adBanner.IsLoaded && adInterstitial.IsLoaded()) || !isOnline)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// インタースティシャル広告が閉じているかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsInterstitialClosed()
    {
        return adInterstitial.IsClosed;
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

        // Google Mobile Ads SDKを設定したアプリIDで初期化.
        MobileAds.Initialize(AppId);

        // バナー広告を生成
        adBanner.RequestBanner();

        // インタースティシャル広告を生成
        adInterstitial.RequestInterstitial();

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

        // 閉じているなら表示する
        if (adInterstitial.IsClosed)
        {
            adInterstitial.Show();
        }
    }
}

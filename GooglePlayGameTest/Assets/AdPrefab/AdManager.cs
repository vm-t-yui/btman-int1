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
    AdBannerControll adBanner = default;                              // バナー広告テストクラス
    [SerializeField]
    AdInterstitialControll adInterstitial = default;                  // インタースティシャル広告テストクラス

    const string AppId = "ca-app-pub-3824454621992610~6537798789";    // アプリID（テスト用）

    public bool IsAdView { get; private set; }                        // 広告表示してるかどうか                

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
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
        adBanner.Show();
    }

    /// <summary>
    /// バナー広告非表示
    /// </summary>
    public void HideBanner()
    {
        adBanner.Hide();
    }

    /// <summary>
    /// インタースティシャル広告表示
    /// </summary>
    public void ShowInterstitial()
    {
        // ロードが終わっていて閉じているなら表示する
        if (adInterstitial.IsLoaded && adInterstitial.IsClosed)
        {
            adInterstitial.Show();
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        // 広告表示されていないなら
        if (!IsAdView)
        {
            // バナー広告を表示
            ShowBanner();

            IsAdView = true;
        }
    }
}

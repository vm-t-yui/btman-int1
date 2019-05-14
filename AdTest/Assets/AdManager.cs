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
    GameObject bannerObj, interstitialObj;       // 各広告オブジェクト

    AdBannerTest adBanner;                       // バナー広告テストクラス
    AdInterstitialTest adInterstitial;           // インタースティシャル広告テストクラス

    public bool IsAdView { get; private set; }   // 広告表示してるかどうか                

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // アプリID、 これはテスト用
        string appId = "ca-app-pub-3824454621992610~6537798789";

        // Google Mobile Ads SDKを設定したアプリIDで初期化.
        MobileAds.Initialize(appId);

        // 各広告クラスのコンポーネントを取得
        adBanner = bannerObj.GetComponent<AdBannerTest>();
        adInterstitial = interstitialObj.GetComponent<AdInterstitialTest>();

        // バナー広告を生成
        adBanner.RequestBanner();

        // インタースティシャル広告を生成
        adInterstitial.RequestInterstitial();
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
    /// バナー広告生成
    /// </summary>
    void Update()
    {
        if (!IsAdView)
        {
            // バナー広告を表示
            adBanner.Show();

            IsAdView = true;
        }
    }
}

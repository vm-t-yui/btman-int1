using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;    // Google AdMob広告用

/// <summary>
/// 広告管理クラス
/// </summary>
public class AdManage : MonoBehaviour
{
    const string AppId = "ca-app-pub-3940256099942544~3347511713";   // アプリID（テスト用ID）

    [SerializeField]
    GameObject bannerObj, interstitialObj;       // 各広告オブジェクト

    AdBannerControll adBanner;                   // バナー広告クラス
    AdInterstitialControll adInterstitial;       // インタースティシャル広告クラス

    public bool IsAdView { get; private set; }   // 広告表示してるかどうか

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // Google Mobile Ads SDKを設定したアプリIDで初期化.
        MobileAds.Initialize(AppId);

        // 各広告クラスのコンポーネントを取得
        adBanner = bannerObj.GetComponent<AdBannerControll>();
        adInterstitial = interstitialObj.GetComponent<AdInterstitialControll>();

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

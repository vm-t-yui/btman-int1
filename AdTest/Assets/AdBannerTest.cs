using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;    // Google AdMob広告用

/// <summary>
/// バナー広告テストクラス
/// </summary>
public class AdBannerTest : MonoBehaviour
{
    BannerView bannerView;  // バナー広告制御クラス

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 処理なし
    }

    /// <summary>
    /// バナー広告生成
    /// </summary>
    public void RequestBanner()
    {
        // 広告ユニットID これはテスト用
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";

        // サイズ320 x 50、画面上部表示の設定で初期化
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // 空の広告リクエストを作成
        AdRequest request = new AdRequest.Builder().Build();

        // bannerViewにrequestをロード
        bannerView.LoadAd(request);

        // 表示状態で生成されるので非表示にする
        bannerView.Hide();
    }

    /// <summary>
    /// 表示
    /// </summary>
    public void ShowBanner()
    {
        bannerView.Show();
    }

    /// <summary>
    /// 非表示
    /// </summary>
    public void HideBanner()
    {
        bannerView.Hide();
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 処理なし
    }
}

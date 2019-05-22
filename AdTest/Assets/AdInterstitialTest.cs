using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;    // Google AdMob広告用

/// <summary>
/// インタースティシャル広告テストクラス
/// </summary>
public class AdInterstitialTest : MonoBehaviour
{
    InterstitialAd interstitialAd;  　　　　　　　　                    // インタースティシャル広告制御クラス

    const string AdUnitId = "ca-app-pub-3940256099942544/1033173712";   // 広告ユニットID（テスト用ID）

    public bool IsLoaded { get; private set; }　　　                    // ロードし終わったかどうか
    public bool IsClosed { get; private set; }                          // 広告を閉じているかどうか

    /// <summary>
    /// インタースティシャル広告生成
    /// </summary>
    public void RequestInterstitial()
    {
        IsLoaded = false;
        IsClosed = true;

        // interstitialAdを初期化
        interstitialAd = new InterstitialAd(AdUnitId);

        // 空の広告リクエストを作成
        AdRequest request = new AdRequest.Builder().Build();

        // interstitialAdにrequestをロード
        this.interstitialAd.LoadAd(request);

        //// InterstitialAdの破棄と再読み込み
        interstitialAd.OnAdClosed += HandleAdClosed;
    }

    /// <summary>
    /// インタースティシャル広告表示
    /// </summary>
    public void Show()
    {
        interstitialAd.Show();

        IsClosed = false;
    }

    /// <summary>
    /// InterstitialAdの破棄と再読み込み
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void HandleAdClosed(object sender, System.EventArgs e)
    {
        interstitialAd.Destroy();
        RequestInterstitial();
        IsClosed = true;
    }

    /// <summary>
    /// バナー広告生成
    /// </summary>
    void Update()
    {
        if (interstitialAd.IsLoaded())
        {
            IsLoaded = true;
        }
    }
}

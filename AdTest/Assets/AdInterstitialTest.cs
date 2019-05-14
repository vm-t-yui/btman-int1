using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;    // Google AdMob広告用

/// <summary>
/// インタースティシャル広告テストクラス
/// </summary>
public class AdInterstitialTest : MonoBehaviour
{
    InterstitialAd interstitialAd;  // インタースティシャル広告制御クラス

    public bool isLoaded { get; private set; }

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 処理なし
    }

    /// <summary>
    /// インタースティシャル広告生成
    /// </summary>
    public void RequestInterstitial()
    {
        isLoaded = false;

        // 広告ユニットID これはテスト用
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";

        // interstitialAdを初期化
        interstitialAd = new InterstitialAd(adUnitId);

        // 空の広告リクエストを作成
        AdRequest request = new AdRequest.Builder().Build();

        // interstitialAdにrequestをロード
        this.interstitialAd.LoadAd(request);

        //// InterstitialAdの破棄と再読み込み
        //interstitialAd.OnAdClosed += HandleAdClosed;
    }

    /// <summary>
    /// インタースティシャル広告表示
    /// </summary>
    public void ShowInterstitial()
    {
        interstitialAd.Show();
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
    }

    /// <summary>
    /// バナー広告生成
    /// </summary>
    void Update()
    {
        if (interstitialAd.IsLoaded())
        {
            isLoaded = true;
        }
    }
}

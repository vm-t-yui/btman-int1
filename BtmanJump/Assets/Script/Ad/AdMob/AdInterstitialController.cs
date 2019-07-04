using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;    // Google AdMob広告用

/// <summary>
/// インタースティシャル広告コントロールクラス
/// </summary>
public class AdInterstitialController : MonoBehaviour
{
    InterstitialAd interstitialAd;                                      // インタースティシャル広告制御クラス

    const string AdUnitId =                                             // 広告ユニットID（テスト用ID）
#if UNITY_ANDROID
        "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IOS
        "ca-app-pub-3940256099942544/4411468910";
#else
        "unexpected_platform";
#endif

    public bool IsClosed { get; private set; }                          // 広告を閉じているかどうか

    /// <summary>
    /// ロード完了検知
    /// </summary>
    /// <returns></returns>
    public bool IsLoaded()
    {
        // 広告のロードが完了したらtrueを返す
        if (interstitialAd.IsLoaded())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// インタースティシャル広告生成
    /// </summary>
    public void RequestInterstitial()
    {
        IsClosed = true;

        // interstitialAdを初期化
        interstitialAd = new InterstitialAd(AdUnitId);

        // 空の広告リクエストを作成
        AdRequest request = new AdRequest.Builder().Build();

        // interstitialAdにrequestをロード
        interstitialAd.LoadAd(request);

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
}

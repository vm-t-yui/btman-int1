using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;

//---------------------------------------------------------------
//バナー広告テストクラス
//---------------------------------------------------------------
public class AdBannerTest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

        // アプリID、 これはテスト用
        string appId = "ca-app-pub-3940256099942544~3347511713";

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        RequestBanner();
    }

    /// <summary>
    /// バナー広告生成
    /// </summary>
    private void RequestBanner()
    {

        // 広告ユニットID これはテスト用
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";

        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

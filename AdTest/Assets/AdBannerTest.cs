using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;
using GoogleMobileAds.Android;

public class AdBannerTest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // アプリID
        string appId = "ca-app-pub-3940256099942544~3347511713";

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        RequestBanner();
    }
    private void RequestBanner()
    {

        // 広告ユニットID これはテスト用
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";

        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

        bannerView.Show();
    }


    // Update is called once per frame
    void Update()
    {

    }
}

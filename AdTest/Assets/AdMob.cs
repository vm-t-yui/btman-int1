using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;

public class AdMob : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // アプリID
        string appId = "ca-app-pub-3824454621992610~6537798789";

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        RequestBanner();
    }

    private void RequestBanner()
    {

        // 広告ユニットID
        string adUnitId = "ca-app-pub-3824454621992610/4790915926";

        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

        // Create a 320x50 banner at the top of the screen.
        //bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

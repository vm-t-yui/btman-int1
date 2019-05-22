﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;    // Google AdMob広告用

/// <summary>
/// バナー広告コントロールクラス
/// </summary>
public class AdBannerControll : MonoBehaviour
{
    BannerView bannerView;                                              // バナー広告制御クラス

    const string AdUnitId = "ca-app-pub-3940256099942544/6300978111";   // 広告ユニットID（テスト用ID）

    /// <summary>
    /// バナー広告生成
    /// </summary>
    public void RequestBanner()
    {
        // サイズ320 x 50、画面上部表示の設定で初期化
        bannerView = new BannerView(AdUnitId, AdSize.Banner, AdPosition.Top);

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
    public void Show()
    {
        bannerView.Show();
    }

    /// <summary>
    /// 非表示
    /// </summary>
    public void Hide()
    {
        bannerView.Hide();
    }
}
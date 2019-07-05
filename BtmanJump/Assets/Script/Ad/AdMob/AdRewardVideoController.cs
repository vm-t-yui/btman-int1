using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using GoogleMobileAds.Api;    // Google AdMob広告用

/// <summary>
/// 動画リワード広告コントロールクラス
/// </summary>
public class AdRewardVideoController : MonoBehaviour
{
    RewardBasedVideoAd rewardBasedVideo;                       // 動画リワード広告制御クラス

    const string AdUnitId =                                    // 広告ユニットID（テスト用）
#if UNITY_ANDROID
        /*"ca-app-pub-3940256099942544/5224354917"*/"ca-app-pub-7073050807259252/3829988740";
#elif UNITY_IPHONE
        /*"ca-app-pub-3940256099942544/1712485313"*/"ca-app-pub-7073050807259252/9181128596";
#else
        "unexpected_platform";
#endif

    public bool IsLoaded { get; private set; } = false;        // ロード完了フラグ
    public bool IsFailedLoad { get; private set; } = false;    // ロード失敗フラグ
    public bool IsStarted { get; private set; } = false;       // 再生開始フラグ
    public bool IsSkipped { get; private set; } = false;       // スキップフラグ
    public bool IsCompleted { get; private set; } = false;     // 再生完了フラグ
    public bool IsClosed { get; private set; } = false;        // 広告閉じたフラグ

    /// <summary>
    /// 動画リワード広告生成
    /// </summary>
    public void RequestRewardVideo()
    {
        // 動画リワード広告ベースの参照を取得
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        // 広告リクエストが正常に読み込まれたときに呼び出される
        rewardBasedVideo.OnAdLoaded += AdLoaded;
        // 広告リクエストの読み込みに失敗したときに呼び出される
        rewardBasedVideo.OnAdFailedToLoad += FailedToAdLoad;
        // 広告の再生が開始されたときに呼び出される
        rewardBasedVideo.OnAdStarted += AdStarted;
        // ユーザーがビデオを見たことに対して報酬が与えられるべきときに呼び出される
        rewardBasedVideo.OnAdRewarded += AdCompleted;
        // 広告が閉じたときに呼び出される
        rewardBasedVideo.OnAdClosed += AdClosed;

        // 空の広告リクエストを作成
        AdRequest request = new AdRequest.Builder().Build();
        // リクエストを受け取った動画広告をロード
        rewardBasedVideo.LoadAd(request, AdUnitId);
    }

    /// <summary>
    /// 動画リワード広告再生
    /// </summary>
    public void Play()
    {
        rewardBasedVideo.Show();
    }

    /// <summary>
    /// ロード完了時コールバック関数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    void AdLoaded(object sender, EventArgs args)
    {
        IsLoaded = true;
    }

    /// <summary>
    /// ロード失敗時コールバック関数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    void FailedToAdLoad(object sender, AdFailedToLoadEventArgs args)
    {
        IsFailedLoad = true;
    }

    /// <summary>
    /// 動画広告再生時コールバック関数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    void AdStarted(object sender, EventArgs args)
    {
        IsStarted = true;
    }

    /// <summary>
    /// 動画広告再生完了時コールバック関数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    void AdCompleted(object sender, Reward args)
    {
        IsCompleted = true;
    }

    /// <summary>
    /// 動画広告閉じた時のコールバック関数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    void AdClosed(object sender, EventArgs args)
    {
        // 再生完了していなければスキップしたとみなす
        if (IsCompleted)
        {
            IsSkipped = false;
        }

        IsClosed = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 動画広告勧誘クラス
/// </summary>
public class AdVideoRecommender : MonoBehaviour
{
    [SerializeField]
    AdRewardVideoController adMobVideo = default;             //AdMob動画リワード広告クラス

    [SerializeField]
    UnityAdsRewardController unityAdsVideo = default;         // UnityAds動画リワード広告クラス

    [SerializeField]
    PlayDataManager playData = default;                       // プレイデータ管理クラス

    [SerializeField]
    GameObject recommendWindow = default;                     // 勧誘用カンバス

    bool isAble = false;                                      // 勧誘許可フラグ
    public bool IsRecommend { get; private set; } = false;    // 勧誘済みフラグ
    public bool IsVideoSkip { get; private set; } = false;    // 広告スキップフラグ
    public bool IsEnd { get; private set; } = false;          // 処理終了フラグ
    
    const int RecommendInterval = 5;                          // 勧誘を行うプレイ回数間隔

    bool isAdMob = false;                                     // AdMob使用フラグ（UnityAdsと交互に使用するため）
    string IsAdMobKey = "IsAdMob";                            // AdMob使用フラグのデータキー

    /// <summary>
    /// 初期化
    /// </summary>
    public void Init()
    {
        // プレイ回数が指定した値で割り切れたら
        if (playData.PlayCount > 0 && playData.PlayCount % RecommendInterval == 0)
        {
            isAdMob = PlayerPrefs.GetInt(IsAdMobKey, 0) == 1 ? true : false;

            // AdMobとUnityAdsを交互に表示
            if (isAdMob)
            {
                // AdMob動画リワード広告を生成
                adMobVideo.RequestRewardVideo();
                isAdMob = false;
            }
            else
            {
                isAdMob = true;
            }

            // AdMob使用フラグを更新してセーブ
            PlayerPrefs.SetInt(IsAdMobKey, isAdMob ? 1 : 0);
            PlayerPrefs.Save();

            // 勧誘を許可
            isAble = true;
        }
    }

    /// <summary>
    /// 動画広告再生
    /// </summary>
    public void PlayAdVideo()
    {
        // AdMob再生
        if (isAdMob)
        {
            adMobVideo.Play();
        }
        // UnityAds再生
        else
        {
            unityAdsVideo.Play();
        }
    }

    /// <summary>
    /// 動画広告勧誘
    /// </summary>
    public void Recommend()
    {
        // 勧誘が許可されているなら
        if (isAble)
        {
            // 専用カンバスを表示
            recommendWindow.SetActive(true);
        }

        // 勧誘済みにする
        IsRecommend = true;
    }

    /// <summary>
    /// 動画広告勧誘ウィンドウ削除
    /// </summary>
    public void Cancel()
    {
        recommendWindow.SetActive(false);
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 勧誘していなければ処理を抜ける
        if (!IsRecommend) { return; }

        // 動画広告をスキップしたらスキップフラグを立てる
        if ((adMobVideo.IsSkipped && adMobVideo.IsClosed) || unityAdsVideo.IsSkipped)
        {
            IsVideoSkip = true;

            playData.SetIsReward(false);
        }

        // 動画広告を閉じたら処理終了
        if ((adMobVideo.IsCompleted && adMobVideo.IsClosed) || unityAdsVideo.IsFinished)
        {
            IsEnd = true;

            playData.SetIsReward(true);
        }
    }
}

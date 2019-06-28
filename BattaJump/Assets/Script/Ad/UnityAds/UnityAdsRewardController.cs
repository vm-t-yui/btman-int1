using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

/// <summary>
/// UnityAdsリワード広告コントロールクラス
/// </summary>
public class UnityAdsRewardController : MonoBehaviour
{
    public bool IsFailed { get; private set; } = false;      // 再生失敗フラグ
    public bool IsSkipped { get; private set; } = false;     // 広告スキップフラグ
    public bool IsFinished { get; private set; } = false;    // 再生完了フラグ

    /// <summary>
    /// 動画広告再生
    /// </summary>
    public void Play()
    {
        // 準備ができていれば再生
        if (Advertisement.IsReady("rewardedVideo"))
        {
            // コールバック関数を設定
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    /// <summary>
    /// 各イベントコールバック関数
    /// </summary>
    /// <param name="result"></param>
    void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:      // 再生失敗時
                IsFailed = true;
                break;

            case ShowResult.Skipped:     // 広告スキップ時
                IsSkipped = true;
                break;

            case ShowResult.Finished:    // 再生完了時
                IsFinished = true;
                break;
        }
    }
}

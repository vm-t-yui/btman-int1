using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 以下の3つはGooglePlayGamesの機能を使用するためのもの
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

/// <summary>
/// リーダーボード管理クラス
/// </summary>
public class LeaderboardController : MonoBehaviour
{
    readonly string LeaderboardID =    // リーダーボードID
#if UNITY_ANDROID
        "CgkI_vDQ3ZYdEAIQBg";
#elif UNITY_IOS
        "maxdistance";
#endif

    long score = 0;                    // リーダーボードへ登録するスコア

    /// <summary>
    /// リーダーボードへスコアを登録
    /// </summary>
    /// <param name="score">登録するスコア</param>
    public void RegisterScoreToLeaderboard(int score)
    {
        // スコア登録処理（成功時になにか処理をしたりはしないので空にしてます）
        Social.ReportScore(score, LeaderboardID, (bool success) => {});
    }

    /// <summary>
    /// リーダーボード表示
    /// </summary>
    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
}

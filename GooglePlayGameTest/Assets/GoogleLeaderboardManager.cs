using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 以下の3つはGooglePlayGamesの機能を使用するためのもの
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

/// <summary>
/// GooglePlayリーダーボード管理クラス
/// </summary>
public class GoogleLeaderboardManager : MonoBehaviour
{
    [SerializeField]
    InputField inputField;                                   // 仮でスコアを登録するための入力欄

    readonly string LeaderboardID = "CgkI_vDQ3ZYdEAIQBg";    // リーダーボードID

    long score = 0;                                          // リーダーボードへ登録するスコア

    /// <summary>
    /// リーダーボードへスコアを登録
    /// </summary>
    public void RegisterScoreToLeaderboard()
    {
        score = long.Parse(inputField.text);
        
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

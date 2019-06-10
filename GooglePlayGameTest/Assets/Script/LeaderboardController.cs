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
    [SerializeField]
    InputField inputField = default;                         // 仮でスコアを登録するための入力欄

    readonly string LeaderboardID =                          // リーダーボードID
#if UNITY_ANDROID
        "CgkI_vDQ3ZYdEAIQBg";
#elif UNITY_IOS
        "maxdistance";
#endif

    long score = 0;                                          // リーダーボードへ登録するスコア

    /// <summary>
    /// リーダーボードへスコアを登録
    /// </summary>
    public void RegisterScoreToLeaderboard()
    {
        // 入力された値をlong型に変換
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

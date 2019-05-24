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
    GameObject inputFieldObj;                                // 仮でスコアを登録するための入力欄
    InputField inputField;

    readonly string LeaderboardID = "CgkI_vDQ3ZYdEAIQBg";    // リーダーボードID

    long score = 0;                                          // リーダーボードへ登録するスコア

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        inputField = inputFieldObj.GetComponent<InputField>();
    }

    /// <summary>
    /// リーダーボードへスコアを登録
    /// </summary>
    public void RegisterScoreToLeaderboard()
    {
        score = long.Parse(inputField.text);
        Social.ReportScore(score, LeaderboardID, (bool success) => {
            
        });
    }

    /// <summary>
    /// リーダーボード表示
    /// </summary>
    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
}

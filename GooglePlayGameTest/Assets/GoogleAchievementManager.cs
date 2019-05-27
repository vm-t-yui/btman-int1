﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 以下の3つはGooglePlayGamesの機能を使用するためのもの
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

#if UNITY_ANDROID
/// <summary>
/// GooglePlay実績管理クラス
/// </summary>
public class GoogleAchievementManager : MonoBehaviour
{
    [SerializeField]
    GoogleServiceManager googleService;                      // GoogleService機能管理クラス

    const int AchievementNum = 5;                            // 実績の総数

    readonly string[] AchievementIDs = {                     // 実績ID群
        "CgkI_vDQ3ZYdEAIQAQ",                                // 実績１：初ログイン
        "CgkI_vDQ3ZYdEAIQAg",                                // 実績２：タップ 
        "CgkI_vDQ3ZYdEAIQAw",                                // 実績３：2本指タップ 
        "CgkI_vDQ3ZYdEAIQBA",                                // 実績４：3本指タップ 
        "CgkI_vDQ3ZYdEAIQBQ"                                 // 実績５：4本指タップ 
    };

    bool[] isReleased = new bool[AchievementNum];            // 実績解除状況

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 実績解除状況初期化
        for (int i = 0; i < AchievementNum; i++)
        {
            isReleased[i] = false;
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // ログインに成功したら実績１解除
        if (googleService.IsSignIn && !isReleased[0])
        {
            ReleaseAchievement(0, AchievementIDs[0], 100.0f);
        }

        // タップしたら実績２解除
        if (Input.touchCount >= 1 && !isReleased[1])
        {
            ReleaseAchievement(1, AchievementIDs[1], 100.0f);
        }

        // 2本指でタップしたら実績３解除
        if (Input.touchCount >= 2 && !isReleased[2])
        {
            ReleaseAchievement(2, AchievementIDs[2], 100.0f);
        }

        // 3本指でタップしたら実績４解除
        if (Input.touchCount >= 3 && !isReleased[3])
        {
            ReleaseAchievement(3, AchievementIDs[3], 100.0f);
        }

        // 4本指でタップしたら実績５解除
        if (Input.touchCount >= 4 && !isReleased[4])
        {
            ReleaseAchievement(4, AchievementIDs[4], 100.0f);
        }
    }

    /// <summary>
    /// 実績解除
    /// </summary>
    /// <param name="num">実績の番号</param>
    /// <param name="id">実績ID</param>
    /// <param name="progress">実績の進捗（0で非表示解除、100で実績解除）</param>
    void ReleaseAchievement(int num, string id, float progress)
    {
        // 解除処理
        Social.ReportProgress(id, progress, (bool success) => {
            if (success)
            {
                // 解除に成功したら解除状況を更新
                isReleased[num] = true;
            }
        });
    }

    /// <summary>
    /// 実績一覧表示
    /// </summary>
    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }
}
#endif
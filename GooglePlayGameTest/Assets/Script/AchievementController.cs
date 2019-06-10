using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 以下の3つはGooglePlayGamesの機能を使用するためのもの
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

/// <summary>
/// GooglePlay実績管理クラス
/// </summary>
public class AchievementController : MonoBehaviour
{
    const int AchievementNum = 7;                            // 実績の総数

    readonly string[] AchievementIDs = {                     // 実績ID群
#if UNITY_ANDROID
        "CgkI_vDQ3ZYdEAIQAQ",                                // 実績１：初ジャンプ
        "CgkI_vDQ3ZYdEAIQAg",                                // 実績２：宇宙到達
        "CgkI_vDQ3ZYdEAIQAw",                                // 実績３：太陽系制覇
        "CgkI_vDQ3ZYdEAIQBA",                                // 実績４：開始後、放置
        "CgkI_vDQ3ZYdEAIQBQ",                                // 実績５：15回ジャンプ
        "CgkI_vDQ3ZYdEAIQBw",                                // 実績６：30回ジャンプ
        "CgkI_vDQ3ZYdEAIQCA"                                 // 実績７：アイテムコンプリート
#elif UNITY_IOS
        "firstjump",                                         // 実績１：初ジャンプ
        "spacecame",                                         // 実績２：宇宙到達
        "planetarycature",                                   // 実績３：太陽系制覇
        "pleasefly",                                         // 実績４：開始後、放置
        "fifteenjump",                                       // 実績５：15回ジャンプ
        "thirtyjump"                                         // 実績６：30回ジャンプ
        "itemComplete",                                      // 実績７：アイテムコンプリート
#endif
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
    /// 実績解除
    /// </summary>
    /// <param num="id">実績の番号</param>
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

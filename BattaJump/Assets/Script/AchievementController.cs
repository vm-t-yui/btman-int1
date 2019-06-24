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
    /// <summary>
    /// 実績の種類
    /// </summary>
    public enum AchievementType
    {
        FirstJump = 0,
        ReachedUniverse,
        ConqueredSolarSystem,
        NotJump,
        JumpedFifteen,
        JumpedThirty,
        ItemComplete,
    }

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
        "thirtyjump",                                        // 実績６：30回ジャンプ
        "itemComplete",                                      // 実績７：アイテムコンプリート
#endif
    };

    [SerializeField]
    PlayDataManager playData = default;            // プレイデータ管理クラス

    [SerializeField]
    ItemDataManager itemData = default;            // アイテムデータ管理クラス

    public const int AchievementNum = 7;           // 実績の総数

    // 実績解除用の各値
    const int FirstJumpNum = 1;                    // 初ジャンプ
    const int ReachedUniverseNum = 60000;          // 宇宙到達
    const int ConqueredSolarSystemNum = 100000;    // 太陽系制覇
    const int JumpedFifteenNum = 15;               // 15回ジャンプ
    const int JumpedThirtyNum = 30;                // 30回ジャンプ
    const int ItemCompleteNum = 38;                // アイテムコンプリート

    /// <summary>
    /// 実績解除
    /// </summary>
    /// <param type="id">実績の種類</param>
    /// <param name="id">実績ID</param>
    /// <param name="progress">実績の進捗（0で非表示解除、100で実績解除）</param>
    void ReleaseAchievement(AchievementType type, string id, float progress)
    {
        // 解除処理
        Social.ReportProgress(id, progress, (bool success) => {
            if (success)
            {
                // 解除に成功したら解除状況を更新
                playData.SetAchievementStatus(type);
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

    /// <summary>
    /// 実績解除チェック
    /// </summary>
    public void CheckRelease()
    {
        // 各実績が解除できる状態なら、解除する
        // 初ジャンプ
        if (playData.PlayCount == FirstJumpNum && !playData.AchievementStatus[(int)AchievementType.FirstJump])
        {
            ReleaseAchievement(AchievementType.FirstJump, AchievementIDs[(int)AchievementType.FirstJump], 100);
        }
        // 宇宙到達
        if (playData.GetNowScore() >= ReachedUniverseNum && !playData.AchievementStatus[(int)AchievementType.ReachedUniverse])
        {
            ReleaseAchievement(AchievementType.ReachedUniverse, AchievementIDs[(int)AchievementType.ReachedUniverse], 100);
        }
        // 太陽系制覇
        if (playData.GetNowScore() >= ConqueredSolarSystemNum && !playData.AchievementStatus[(int)AchievementType.ConqueredSolarSystem])
        {
            ReleaseAchievement(AchievementType.ConqueredSolarSystem, AchievementIDs[(int)AchievementType.ConqueredSolarSystem], 100);
        }
        // 15回ジャンプ
        if (playData.PlayCount == JumpedFifteenNum && !playData.AchievementStatus[(int)AchievementType.JumpedFifteen])
        {
            ReleaseAchievement(AchievementType.JumpedFifteen, AchievementIDs[(int)AchievementType.JumpedFifteen], 100);
        }
        // 30回ジャンプ
        if (playData.PlayCount == JumpedThirtyNum && !playData.AchievementStatus[(int)AchievementType.JumpedThirty])
        {
            ReleaseAchievement(AchievementType.JumpedThirty, AchievementIDs[(int)AchievementType.JumpedThirty], 100);
        }

        int haveItem = 0;                                  // アイテム所持数
        bool[] isHaveItem = itemData.GetHaveItemFlag();    // アイテム所持フラグ
        for (int i = 0; i < isHaveItem.Length; i++)
        {
            // アイテムを所持しているなら所持数を加算
            if (isHaveItem[i])
            {
                haveItem++;
            }
        }
        // アイテムコンプリート
        if (haveItem == ItemCompleteNum && !playData.AchievementStatus[(int)AchievementType.ItemComplete])
        {
            ReleaseAchievement(AchievementType.ItemComplete, AchievementIDs[(int)AchievementType.ItemComplete], 100);
        }
    }
}

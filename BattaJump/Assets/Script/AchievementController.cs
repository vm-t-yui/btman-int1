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
    string[] achievementIDs = new string[AchievementScriptableObject.AchievementNum];    // 実績ID群

    [SerializeField]
    PlayDataManager playData = default;            // プレイデータ管理クラス

    [SerializeField]
    ItemDataManager itemData = default;            // アイテムデータ管理クラス

    float putTime = 0;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 実績IDを取得
        achievementIDs =
#if UNITY_ANDROID
        AchievementScriptableObject.Instance.GetAndroidIDs();
#elif UNITY_IOS
        AchievementScriptableObject.Instance.GetIosIDs();
#else
        null;
#endif
    }

    /// <summary>
    /// 実績解除
    /// </summary>
    /// <param type="id">実績の種類</param>
    /// <param name="id">実績ID</param>
    /// <param name="progress">実績の進捗（0で非表示解除、100で実績解除）</param>
    void ReleaseAchievement(AchievementScriptableObject.AchievementType type, string id, float progress)
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
        // IDがnullなら処理を抜ける
        if (achievementIDs == null) { return; }

        Social.ShowAchievementsUI();
    }

    /// <summary>
    /// 放置の実績解除チェック
    /// </summary>
    public void CheckPutTime()
    {
        // IDがnullなら処理を抜ける
        if (achievementIDs == null) { return; }

        // 放置時間を計測
        putTime += Time.deltaTime;
        
        // 放置時間が指定した値を超えたら実績解除
        if (putTime >= AchievementScriptableObject.Instance.GetNotJumpNum() && !playData.AchievementStatus[(int)AchievementScriptableObject.AchievementType.NotJump])
        {
            ReleaseAchievement(AchievementScriptableObject.AchievementType.NotJump, achievementIDs[(int)AchievementScriptableObject.AchievementType.NotJump], 100);
        }
    }

    /// <summary>
    /// 実績解除チェック
    /// </summary>
    public void CheckRelease()
    {
        // IDがnullなら処理を抜ける
        if (achievementIDs == null) { return; }

        // インスタンスを取得
        AchievementScriptableObject achievementData = AchievementScriptableObject.Instance;

        // 各実績が解除できる状態なら、解除する
        // 初ジャンプ
        if (playData.PlayCount == achievementData.GetFirstJumpNum() && !playData.AchievementStatus[(int)AchievementScriptableObject.AchievementType.FirstJump])
        {
            ReleaseAchievement(AchievementScriptableObject.AchievementType.FirstJump, achievementIDs[(int)AchievementScriptableObject.AchievementType.FirstJump], 100);
        }
        // 宇宙到達
        if (playData.GetNowScore() >= achievementData.GetReachedUniverseNum() && !playData.AchievementStatus[(int)AchievementScriptableObject.AchievementType.ReachedUniverse])
        {
            ReleaseAchievement(AchievementScriptableObject.AchievementType.ReachedUniverse, achievementIDs[(int)AchievementScriptableObject.AchievementType.ReachedUniverse], 100);
        }
        // 太陽系制覇
        if (playData.GetNowScore() >= achievementData.GetConqueredSolarSystemNum() && !playData.AchievementStatus[(int)AchievementScriptableObject.AchievementType.ConqueredSolarSystem])
        {
            ReleaseAchievement(AchievementScriptableObject.AchievementType.ConqueredSolarSystem, achievementIDs[(int)AchievementScriptableObject.AchievementType.ConqueredSolarSystem], 100);
        }
        // 15回ジャンプ
        if (playData.PlayCount == achievementData.GetJumpedFifteenNum() && !playData.AchievementStatus[(int)AchievementScriptableObject.AchievementType.JumpedFifteen])
        {
            ReleaseAchievement(AchievementScriptableObject.AchievementType.JumpedFifteen, achievementIDs[(int)AchievementScriptableObject.AchievementType.JumpedFifteen], 100);
        }
        // 30回ジャンプ
        if (playData.PlayCount == achievementData.GetJumpedThirtyNum() && !playData.AchievementStatus[(int)AchievementScriptableObject.AchievementType.JumpedThirty])
        {
            ReleaseAchievement(AchievementScriptableObject.AchievementType.JumpedThirty, achievementIDs[(int)AchievementScriptableObject.AchievementType.JumpedThirty], 100);
        }

        int haveItem = 0;                                  // アイテム所持数
        bool[] isHaveItem = itemData.GetIsHasItem();    // アイテム所持フラグ
        for (int i = 0; i < isHaveItem.Length; i++)
        {
            // アイテムを所持しているなら所持数を加算
            if (isHaveItem[i])
            {
                haveItem++;
            }
        }
        // アイテムコンプリート
        if (haveItem == achievementData.GetItemCompleteNum() && !playData.AchievementStatus[(int)AchievementScriptableObject.AchievementType.ItemComplete])
        {
            ReleaseAchievement(AchievementScriptableObject.AchievementType.ItemComplete, achievementIDs[(int)AchievementScriptableObject.AchievementType.ItemComplete], 100);
        }
    }
}

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
public class GoogleAchievementManager : MonoBehaviour
{
    [SerializeField]
    GameObject googleServiceObj;                             // GoogleService機能管理オブジェクト
    GoogleServiceManager googleService;                      // GoogleService機能管理クラス

    const int AchievementNum = 5;                            // 実績の総数

    string[] achievementIDs = new string[AchievementNum];    // 実績ID群

    bool[] isReleased = new bool[AchievementNum];            // 実績解除状況

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // GoogleService機能管理クラス取得
        googleService = googleServiceObj.GetComponent<GoogleServiceManager>();

        // 実績IDをセット
        SetAchievementIDs();

        // 実績解除状況初期化
        for (int i = 0; i < 5; i++)
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
            ReleaseAchievement(achievementIDs[0], 100.0f);
            isReleased[0] = true;
        }

        // タップしたら実績２解除
        if (Input.touchCount >= 1 && !isReleased[1])
        {
            ReleaseAchievement(achievementIDs[1], 100.0f);
            isReleased[1] = true;
        }

        // 2本指でタップしたら実績３解除
        if (Input.touchCount >= 2 && !isReleased[2])
        {
            ReleaseAchievement(achievementIDs[2], 100.0f);
            isReleased[2] = true;
        }

        // 3本指でタップしたら実績４解除
        if (Input.touchCount >= 3 && !isReleased[3])
        {
            ReleaseAchievement(achievementIDs[3], 100.0f);
            isReleased[3] = true;
        }

        // 4本指でタップしたら実績５解除
        if (Input.touchCount >= 4 && !isReleased[4])
        {
            ReleaseAchievement(achievementIDs[4], 100.0f);
            isReleased[4] = true;
        }
    }

    /// <summary>
    /// 実績解除
    /// </summary>
    /// <param name="id">実績ID</param>
    /// <param name="progress">実績の進捗（0で非表示解除、100で実績解除）</param>
    void ReleaseAchievement(string id, float progress)
    {
        Social.ReportProgress(id, progress, (bool success) => {
            if (success)
            {
                ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);
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
    /// 実績IDセット
    /// </summary>
    void SetAchievementIDs()
    {
        achievementIDs[0] = "CgkI_vDQ3ZYdEAIQAQ";    // 実績１：初ログイン
        achievementIDs[1] = "CgkI_vDQ3ZYdEAIQAg";    // 実績２：タップ
        achievementIDs[2] = "CgkI_vDQ3ZYdEAIQAw";    // 実績３：2本指タップ
        achievementIDs[3] = "CgkI_vDQ3ZYdEAIQBA";    // 実績４：3本指タップ
        achievementIDs[4] = "CgkI_vDQ3ZYdEAIQBQ";    // 実績５：4本指タップ
    }
}

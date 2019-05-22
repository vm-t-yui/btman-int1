using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System.Collections;

/// <summary>
/// GameCenterの管理クラス
/// </summary>
public class GameCenterManager : MonoBehaviour
{
    [SerializeField] InputField inputField;     //リーダーボードの点数入力用のInputField
    
    const string leaderBoardId = "maxdistance"; // リーダボードID
    const string achievementID = "firstjump";   // 達成項目ID
    
    float firstAchievementsPoint = 100;         //最初の実績を取るためのポイント
    
    //プラットフxームがUnityの時のみ使用可能
    #if UNITY_IPHONE
    
    /// <summary>
    /// 開始
    /// </summary>
    void Awake()
    {
        // 初期化処理
        Social.localUser.Authenticate(ProcessAuthentication);
    }
    
    /// <summary>
    /// 認証に成功したかどうか
    /// </summary>
    /// <param name="success">If set to <c>true</c> success.</param>
    void ProcessAuthentication(bool success)
    {
        if (success)
        {
            // 処理なし
        }
        else
        {
            // 処理なし
        }
    }
    
    /// <summary>
    /// リーダーボード表示　Inspector上のLeaderBoardを押すと反応
    /// </summary>
    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
    
    /// <summary>
    /// リーダーボード用のハイスコア送信　Inspector上のScoreButtonを押すと反応
    /// </summary>
    public void SendLeaderboardScore()
    {
        int score = 0;
        if (int.TryParse(inputField.text, out score))
        {
            Social.ReportScore(score, leaderBoardId, success => {
                if (success)
                {
                    // 処理なし
                }
                else
                {
                    // 処理なし
                }
            });
        }
    }
    
    /// <summary>
    /// 達成項目の表示　Inspector上のAchievementsを押すと反応
    /// </summary>
    public static void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }
    
    /// <summary>
    /// 達成項目用のポイント送信　Inspector上のSendPointを押すと反応
    /// </summary>
    public void SendAchievementsPoint()
    {
        Social.ReportProgress(achievementID, firstAchievementsPoint, success => {
            
            // 処理なし
        });
    }
    #endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイデータ管理クラス
/// </summary>
public class PlayDataManager : MonoBehaviour
{
    public int PlayCount { get; private set; } = 0;            // プレイ回数
    public int HighScore { get; private set; } = 0;            // ハイスコア
    public bool[] AchievementStatus { get; private set; } =    // 実績解除状況
           new bool[AchievementScriptableObject.AchievementNum];

    static int nowScore = 0;                                   // 現在のスコア

    readonly string PlayCountKey = "PlayCount";                // プレイ回数データキー
    readonly string HighScoreKey = "HighScore";                // ハイスコアデータキー
    readonly string AchievementStatusKey = "Achivement";       // 実績解除状況データキー
                                                               // NOTE:for文でループする際に末尾にiを付け加えて使用

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Awake()
    {
        // データをロード
        LoadData();
    }

    /// <summary>
    /// プレイ回数加算
    /// </summary>
    public void IncreasePlayCount()
    {
        PlayCount++;
    }
    
    /// <summary>
    /// 現在のスコアに獲得したスコアをセット
    /// </summary>
    /// <param name="score">獲得したスコア</param>
    public void SetNowScore(int score)
    {
        nowScore = score;
    }

    /// <summary>
    /// 現在のスコアをゲット
    /// </summary>
    /// <returns></returns>
    public int GetNowScore()
    {
        return nowScore;
    }

    /// <summary>
    /// 実績の解除状況をセット
    /// </summary>
    /// <param name="achievementType">実績の種類</param>
    public void SetAchievementStatus(AchievementScriptableObject.AchievementType achievementType)
    {
        AchievementStatus[(int)achievementType] = true;
    }

    /// <summary>
    /// データロード
    /// </summary>
    public void LoadData()
    {
        // セーブしたプレイ回数を取得
        PlayCount = PlayerPrefs.GetInt(PlayCountKey, 0);

        // セーブしたハイスコアを取得
        HighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        for (int i = 0; i < AchievementStatus.Length; i++)
        {
            // セーブした実績解除状況を取得
            AchievementStatus[i] = PlayerPrefs.GetInt(AchievementStatusKey + i, 0) == 1 ? true : false;
        }
    }

    /// <summary>
    /// データセーブ
    /// </summary>
    public void SaveData()
    {
        // プレイ回数をセット
        PlayerPrefs.SetInt(PlayCountKey, PlayCount);

        // 現在のスコアがハイスコアを超えていればセット
        if (nowScore > HighScore)
        {
            HighScore = nowScore;
            PlayerPrefs.SetInt(HighScoreKey, HighScore);
        }

        for (int i = 0; i < AchievementStatus.Length; i++)
        {
            // 実績解除状況をセット
            PlayerPrefs.SetInt(AchievementStatusKey + i, AchievementStatus[i] ? 1 : 0);
        }

        // セットしたデータをセーブ
        PlayerPrefs.Save();
    }

    /// <summary>
    /// データ削除
    /// </summary>
    public void DeleteData()
    {
        // プレイ回数を削除
        PlayerPrefs.DeleteKey(PlayCountKey);

        // ハイスコアを削除
        PlayerPrefs.DeleteKey(HighScoreKey);

        for (int i = 0; i < AchievementStatus.Length; i++)
        {
            // 実績解除状況を削除
            PlayerPrefs.DeleteKey(AchievementStatusKey + i);
        }
    }
}

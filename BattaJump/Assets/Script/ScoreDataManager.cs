using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// データ管理クラス
/// </summary>
public class ScoreDataManager : MonoBehaviour
{
    static int nowScore = 0;                //現在のスコア
    static int highScore = 0;               //ハイスコア

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Awake()
    {
        //データロード
        LoadData();
    }

    /// <summary>
    /// スコアセット
    /// </summary>
    /// <param name="score"></param>
    public void SetNowScore(int score)
    {
        nowScore = score;
    }

    /// <summary>
    /// 現在のスコアを渡す
    /// </summary>
    /// <returns></returns>
    public int GetNowScore()
    {
        return nowScore;
    }

    /// <summary>
    /// データロード
    /// </summary>
    public void LoadData()
    {
        //データロード
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    /// <summary>
    /// データセーブ
    /// </summary>
    public void SaveData()
    {
        // 現在のスコアがハイスコアを超えていればハイスコアを更新
        if (nowScore > highScore)
        {
            highScore = nowScore;
        }

        PlayerPrefs.SetFloat("HighScore", highScore);

        //セットしたデータをセーブ
        PlayerPrefs.Save();
    }

    /// <summary>
    /// セーブデータ消去
    /// </summary>
    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}

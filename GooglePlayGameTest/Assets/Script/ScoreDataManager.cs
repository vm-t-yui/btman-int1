using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// データ管理クラス
/// </summary>
public class ScoreDataManager : MonoBehaviour
{
    static int highScore = 0;             //ハイスコア

    static int nowScore = 0;              //現在のスコア

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Awake()
    {
        //データロード
        LoadData();
    }

    public void SetNowScore(int score)
    {
        nowScore = score;
    }

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
        if (nowScore > highScore)
        {
            highScore = nowScore;
        }

        PlayerPrefs.SetInt("HighScore", highScore);

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

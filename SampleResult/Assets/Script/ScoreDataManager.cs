using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// データ管理クラス
/// </summary>
public class ScoreDataManager : MonoBehaviour
{
    [SerializeField]
    ScoreManager scoreManager = default;  //スコアクラス

    [SerializeField]
    float highScore = 0;                  //ハイスコア

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Awake()
    {
        //データロード
        LoadData();
    }

    /// <summary>
    /// データロード
    /// </summary>
    public void LoadData()
    {
        //データロード
        highScore = PlayerPrefs.GetFloat("HighScore", 0);

        //ロードしたデータをセット
        scoreManager.SetHighScore(highScore);
    }

    /// <summary>
    /// データセーブ
    /// </summary>
    public void SaveData()
    {
        //データを取得してセット
        highScore = scoreManager.GetHighScore();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// データ管理クラス
/// </summary>
public class DataManager : MonoBehaviour
{
    [SerializeField]
    float highScore = 0;    //スコア

    [SerializeField]
    int[] isGetItem = new int[ItemManager.count];  //アイテム取得フラグ(PlayerPrefsにboolがないため仕方なくint使用)

    /// <summary>
    /// 起動時処理
    /// </summary>
    private void Awake()
    {
        //データロード
        LoadData();
    }

    /// <summary>
    /// データロード
    /// </summary>
    public void LoadData()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        for (int i = 0; i < ItemManager.count; i++)
        {
            isGetItem[i] = PlayerPrefs.GetInt("isGetItem" + i, 0);
        }
    }

    /// <summary>
    /// データセーブ
    /// </summary>
    public void SaveData()
    {
        PlayerPrefs.SetFloat("HighScore", highScore);
        for (int i = 0; i < ItemManager.count; i++)
        {
            PlayerPrefs.SetInt("isGetItem" + i, isGetItem[i]);
        }

        //取得したデータをセーブ
        PlayerPrefs.Save();
    }

    /// <summary>
    /// セーブデータ消去
    /// </summary>
    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("HighScore");
        for (int i = 0; i < ItemManager.count; i++)
        {
            PlayerPrefs.DeleteKey("isGetItem" + i);
        }
    }

    /// <summary>
    /// アイテム取得フラグをセーブする
    /// </summary>
    /// <param name="ItemNum">アイテム取得時のフラグ</param>
    public void SetIsGetItem(int[] ItemNum)
    {
        for (int i = 0; i < ItemNum.Length; i++)
        {
            if (isGetItem[i] == 0)
            {
                isGetItem[i] = ItemNum[i];
            }
        }
    }

    /// <summary>
    /// ハイスコア更新
    /// </summary>
    /// <param name="score">ゲーム中に稼いだスコア</param>
    public void SetHighScore(float score)
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }

    /// <summary>
    /// アイテム取得フラグのゲット関数
    /// </summary>
    /// <returns>セーブデータから取ってきたアイテム取得フラグ</returns>
    /// <param name="i">アイテムの番号</param>
    public int GetIsGetItem(int i)
    {
        return isGetItem[i];
    }

    /// <summary>
    /// ハイスコアのゲット関数
    /// </summary>
    /// <returns>ハイスコア</returns>
    public float GetHighScore()
    {
        return highScore;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// データ管理クラス
/// </summary>
public class ItemDataManager : MonoBehaviour
{ 
    int[] isHasItem = new int[ItemManager.Num];  　//アイテムゲットフラグ(PlayerPrefsにboolがないため仕方なくint使用)

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
        for (int i = 0; i < isHasItem.Length; i++)
        {
            isHasItem[i] = PlayerPrefs.GetInt(GetKey(i), 0);
        }
    }

    /// <summary>
    /// データセーブ
    /// </summary>
    public void SaveData()
    {
        for (int i = 0; i < isHasItem.Length; i++)
        {
            //ゲットのデータをセット
            PlayerPrefs.SetInt(GetKey(i), isHasItem[i]);
        }

        //セットしたデータをセーブ
        PlayerPrefs.Save();
    }

    /// <summary>
    /// セーブデータ消去
    /// </summary>
    public void DeleteData()
    {
        for (int i = 0; i < isHasItem.Length; i++)
        {
            PlayerPrefs.DeleteKey(GetKey(i));
        }
    }

    /// <summary>
    /// アイテム取得フラグのゲット関数
    /// </summary>
    /// <returns>セーブデータから取ってきたアイテム取得フラグ</returns>
    /// <param name="num">アイテムの番号</param>
    public int GetHaveItemFlag(int num)
    {
        return isHasItem[num];
    }

    /// <summary>
    /// アイテム取得フラグのセット関数
    /// </summary>
    /// <param name="itemNum">アイテム取得時のフラグ</param>
    public void SetHaveItemFlag(int[] itemNum)
    {
        for (int i = 0; i < isHasItem.Length; i++)
        {
            isHasItem[i] = itemNum[i];
        }
    }

    /// <summary>
    /// key取得
    /// </summary>
    /// <returns>The key.</returns>
    /// <param name="count">アイテムの数</param>
    string GetKey(int count)
    {
        return "isGetItem" + count;
    }
}

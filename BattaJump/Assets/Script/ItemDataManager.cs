using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// データ管理クラス
/// </summary>
public class ItemDataManager : MonoBehaviour
{ 
    [SerializeField]
    bool[] isHasItem = new bool[ItemManager.ItemNum];  　//アイテムゲットフラグ

    [SerializeField]
    bool[] isNewText = new bool[ItemManager.ItemNum];   //Newテキストの表示フラグ

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
            isHasItem[i] = PlayerPrefs.GetInt(GetKey(i), 0) == 1 ? true : false;

            isNewText[i] = PlayerPrefs.GetInt(UIGetKey(i), 0) == 1 ? true : false;
        }
    }

    /// <summary>
    /// データセーブ
    /// </summary>
    public void SaveData(int num, bool flag, bool isNew)
    {
        //新しいアイテムかどうか
        if (!isNew)
        {
            //アイテムゲットフラグ保存
            isHasItem[num] = flag;
            PlayerPrefs.SetInt(GetKey(num), isHasItem[num] ? 1 : 0);
        }
        else
        {
            //Newテキストの表示フラグ保存
            isNewText[num] = flag;
            PlayerPrefs.SetInt(UIGetKey(num), isNewText[num] ? 1 : 0);
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
    public bool[] GetIsHasItem()
    {
        return isHasItem;
    }

    /// <summary>
    /// アイテム取得フラグのゲット関数
    /// </summary>
    /// <returns>セーブデータから取ってきたアイテム取得フラグ</returns>
    public bool[] GetIsNewText()
    {
        return isNewText;
    }

    /// <summary>
    /// アイテム取得フラグのセット関数
    /// </summary>
    /// <param name="itemNum">アイテム取得時のフラグ</param>
    public void SetIsHasItem(bool[] itemNum)
    {
        for (int i = 0; i < isHasItem.Length; i++)
        {
            isHasItem[i] = itemNum[i];
        }
    }

    /// <summary>
    /// Newテキストの表示フラグのセット関数
    /// </summary>
    public void SetIsNewText(int num, bool flag)
    {
        isNewText[num] = flag;

        SaveData(num, flag, true);
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

    /// <summary>
    /// 新規アイテムのkey取得
    /// </summary>
    /// <returns>The key.</returns>
    /// <param name="count">アイテムの数</param>
    string UIGetKey(int count)
    {
        return "isNewGetItemUI" + count;
    }
}

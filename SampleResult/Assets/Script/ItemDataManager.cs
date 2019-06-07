﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// データ管理クラス
/// </summary>
public class ItemDataManager : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager = default;                //アイテムクラス

    int[] isHasItem = new int[ItemManager.Num];  　//アイテムゲットフラグ(PlayerPrefsにboolがないため仕方なくint使用)
    string key = "";

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
        for (int i = 0; i < ItemManager.Num; i++)
        {
            isHasItem[i] = PlayerPrefs.GetInt(GetKey(i), 0);
        }

        //ロードしたデータをセット
        itemManager.SetHaveItemFlag(isHasItem);
    }

    /// <summary>
    /// データセーブ
    /// </summary>
    public void SaveData()
    {
        for (int i = 0; i < ItemManager.Num; i++)
        {
            //まだゲットしてしていなかったらゲット
            if (isHasItem[i] == 0)
            {
                isHasItem[i] = itemManager.GetHaveItemFlag(i);
            }

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
        for (int i = 0; i < ItemManager.Num; i++)
        {
            PlayerPrefs.DeleteKey(GetKey(i));
        }
    }

    //keyの取得
    string GetKey(int count)
    {
        return "isGetItem" + count;
    }
}

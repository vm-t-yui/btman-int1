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

    [SerializeField]
    int[] isHaveItem = new int[ItemManager.num];  　//アイテムゲットフラグ(PlayerPrefsにboolがないため仕方なくint使用)

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
        for (int i = 0; i < ItemManager.num; i++)
        {
            isHaveItem[i] = PlayerPrefs.GetInt("isGetItem" + i, 0);
        }

        //ロードしたデータをセット
        itemManager.SetHaveItemFlag(isHaveItem);
    }

    /// <summary>
    /// データセーブ
    /// </summary>
    public void SaveData()
    {
        for (int i = 0; i < ItemManager.num; i++)
        {
            //まだゲットしてしていなかったらゲット
            if (isHaveItem[i] == 0)
            {
                isHaveItem[i] = itemManager.GetHaveItemFlag(i);
            }

            //ゲットのデータをセット
            PlayerPrefs.SetInt("isGetItem" + i, isHaveItem[i]);
        }

        //セットしたデータをセーブ
        PlayerPrefs.Save();
    }

    /// <summary>
    /// セーブデータ消去
    /// </summary>
    public void DeleteData()
    {
        for (int i = 0; i < ItemManager.num; i++)
        {
            PlayerPrefs.DeleteKey("isGetItem" + i);
        }
    }
}

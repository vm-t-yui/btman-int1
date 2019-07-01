using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムクラス
/// </summary>
public class ItemManager : MonoBehaviour
{
    [SerializeField]
    ItemDataManager itemDataManager = default;  //アイテムデータクラス

    public const int ItemNum = 38;               //アイテム数

    public const int ItemAppearanceNum = 5;

    [SerializeField]
    bool[] isHasItem = new bool[ItemNum];       //アイテムゲットフラグ

    [SerializeField]
    public static bool[] isNewHasItem = new bool[ItemNum];   //新しく入手したアイテムのフラグ

    /// <summary>
    /// データからアイテムゲットフラグをロードする
    /// </summary>
    void Start()
    {
        isHasItem = itemDataManager.GetHaveItemFlag();
    }

    /// <summary>
    /// アイテムゲット
    /// </summary>
    /// <param name="num">アイテム番号.</param>
    public void GetItem(int num)
    {
        //まだ手に入れていないアイテムならisNewHasItemもtrueに
        if (!isHasItem[num])
        {
            isNewHasItem[num] = true;
        }

        isHasItem[num] = true;

        //アイテムをセーブ
        itemDataManager.SaveData();
    }

    /// <summary>
    /// 入手したアイテムのフラグのゲット関数
    /// </summary>
    /// <returns>新しく入手したアイテムのフラグ</returns>
    public bool GetIsHasItem(int i)
    {
        return isHasItem[i];
    }

    /// <summary>
    /// 新しく入手したアイテムのフラグのゲット関数(全部)
    /// </summary>
    /// <returns>新しく入手したアイテムのフラグ</returns>
    public bool[] GetIsNewHasItem()
    {
        return isNewHasItem;
    }

    /// <summary>
    /// 新しく入手したアイテムのフラグのゲット関数(1個ずつ)
    /// </summary>
    /// <returns>新しく入手したアイテムのフラグ</returns>
    public bool GetIsNewHasItem(int i)
    {
        return isNewHasItem[i];
    }

    /// <summary>
    /// 新規取得アイテムフラグリセット
    /// </summary>
    public void ResetIsNewHasItem()
    {
        for (int i = 0; i < isNewHasItem.Length; i++) 
        {
            isNewHasItem[i] = false;
        }
    }
}

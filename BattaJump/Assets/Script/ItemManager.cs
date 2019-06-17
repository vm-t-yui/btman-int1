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

    public const int ItemNum = 8;          //アイテム数(10は仮)

    [SerializeField]
    bool[] isHasItem = new bool[ItemNum];    //アイテムゲットフラグ(PlayerPrefsにboolがないため仕方なくint使用)

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
        //NOTE: プレイヤーのOnCollisionEnterにいれて処理をするつもり
        //NOTE: int型で指定しているので1がtrue、0がfalseになる    
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
    /// 新しく入手したアイテムのフラグのゲット関数
    /// </summary>
    /// <returns>新しく入手したアイテムのフラグ</returns>
    public bool[] GetIsNewHasItem()
    {
        return isNewHasItem;
    }
}
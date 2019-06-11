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

    public const int Num = 8;   　             //アイテム数(10は仮)
    static int[] isHasItem = new int[Num];    //アイテムゲットフラグ(PlayerPrefsにboolがないため仕方なくint使用)

    /// <summary>
    /// データからアイテムゲットフラグをロードする
    /// </summary>
    void Start()
    {
        for (int i = 0; i < isHasItem.Length; i++)
        {
            isHasItem[i] = itemDataManager.GetHaveItemFlag(i);
        }
    }

    /// <summary>
    /// アイテムゲット
    /// </summary>
    /// <param name="num">アイテム番号.</param>
    public void GetItem(int num)
    {
        //NOTE: プレイヤーのOnCollisionEnterにいれて処理をするつもり
        //NOTE: int型で指定しているので1がtrue、0がfalseになる    
        isHasItem[num] = 1;

        //アイテムをセーブ
        itemDataManager.SaveData();
    }
}
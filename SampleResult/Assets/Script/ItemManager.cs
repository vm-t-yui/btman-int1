using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムクラス
/// </summary>
public class ItemManager : MonoBehaviour
{
    public const int count = 8;   　             //アイテム数(10は仮)
    static int[] isHaveItem = new int[count];    //アイテムゲットフラグ(PlayerPrefsにboolがないため仕方なくint使用)

    /// <summary>
    /// アイテムゲット
    /// </summary>
    /// <param name="num">アイテム番号.</param>
    public void GetItem(int num)
    {
        //NOTE: プレイヤーのOnCollisionEnterにいれて処理をするつもり
        //NOTE: int型で指定しているので1がtrue、0がfalseになる    
        isHaveItem[num] = 1;
    }

    /// <summary>
    /// アイテム取得フラグのゲット関数
    /// </summary>
    /// <returns>セーブデータから取ってきたアイテム取得フラグ</returns>
    /// <param name="i">アイテムの番号</param>
    public int GetIsHaveItem(int i)
    {
        return isHaveItem[i];
    }

    /// <summary>
    /// アイテム取得フラグのセット関数
    /// </summary>
    /// <param name="ItemNum">アイテム取得時のフラグ</param>
    public void SetIsHaveItem(int[] ItemNum)
    {
        for (int i = 0; i < isHaveItem.Length; i++)
        {
            isHaveItem[i] = ItemNum[i];
        }
    }
}
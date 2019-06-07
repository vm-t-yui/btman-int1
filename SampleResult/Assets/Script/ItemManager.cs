using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムクラス
/// </summary>
public class ItemManager : MonoBehaviour
{
    public const int Num = 8;   　             //アイテム数(10は仮)
    static int[] isHasItem = new int[Num];    //アイテムゲットフラグ(PlayerPrefsにboolがないため仕方なくint使用)

    /// <summary>
    /// アイテムゲット
    /// </summary>
    /// <param name="num">アイテム番号.</param>
    public void GetItem(int num)
    {
        //NOTE: プレイヤーのOnCollisionEnterにいれて処理をするつもり
        //NOTE: int型で指定しているので1がtrue、0がfalseになる    
        isHasItem[num] = 1;
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
}
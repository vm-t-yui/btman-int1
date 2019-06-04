using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムクラス
/// </summary>
public class Item : MonoBehaviour
{
    public static int count = 8;   　//アイテム数(10は仮)

    [SerializeField]
    DataManager dataManager;        //データクラス

    [SerializeField]
    int[] isGetItem = new int[count];           //アイテムゲットフラグ

    public void SendData()
    {
        //アイテムフラグをデータ管理クラスに送る
        dataManager.SetIsGetItem(isGetItem);
    }

    /// <summary>
    /// アイテムゲット
    /// </summary>
    /// <param name="num">アイテム番号.</param>
    public void GetItem(int num)
    {
        //NOTE: プレイヤーのOnCollisionEnterにいれて処理をするつもり
        //NOTE: int型で指定しているので1がtrue、0がfalseになる    
        isGetItem[num] = 1;

        //アイテムフラグをデータ管理クラスに送る
        dataManager.SetIsGetItem(isGetItem);
    }
}
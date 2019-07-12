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

    public static bool[] isNewHasItem = new bool[ItemNum];   //新しく入手したアイテムのフラグ

    [SerializeField]
    bool[] isNewText = new bool[ItemNum]; //Newテキストの表示フラグ

    /// <summary>
    /// データからアイテムゲットフラグをロードする
    /// </summary>
    void Start()
    {
        //データからアイテムゲットフラグをロードする
        isHasItem = itemDataManager.GetIsHasItem();
        isNewText = itemDataManager.GetIsNewText();
    }

    /// <summary>
    /// アイテムゲット
    /// </summary>
    /// <param name="num">アイテム番号.</param>
    public void GetItem(int num)
    {
        //まだ手に入れていないアイテムなら
        if (!isHasItem[num])
        {
            //新規取得アイテムフラグ、Newテキストの表示フラグをtrueにしてセーブ
            isNewHasItem[num] = true;
            isNewText[num] = true;
            itemDataManager.SaveData(num, true, true);
        }

        //アイテムゲットフラグをtrueにしてセーブ
        isHasItem[num] = true;
        itemDataManager.SaveData(num, true, false);
    }

    /// <summary>
    /// 入手したアイテムのフラグのゲット関数(全部)
    /// </summary>
    /// <returns>新しく入手したアイテムのフラグ</returns>
    public bool[] GetIsHasItem()
    {
        return isHasItem;
    }

    /// <summary>
    /// 入手したアイテムのフラグのゲット関数(1個ずつ)
    /// </summary>
    /// <returns>新しく入手したアイテムのフラグ</returns>
    public bool GetIsHasItem(int i)
    {
        return isHasItem[i];
    }

    /// <summary>
    /// NewTextの表示フラグのゲット関数
    /// </summary>
    /// <returns>新しく入手したアイテムのフラグ</returns>
    public bool GetIsNewText(int num)
    {
        return isNewText[num];
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
    public bool GetIsNewHasItem(int num)
    {
        return isNewHasItem[num];
    }

    /// <summary>
    /// UIの新規アイテムフラグを消す
    /// </summary>
    public void ResetIsNewText(int num, bool flag)
    {
        isNewText[num] = flag;

        itemDataManager.SetIsNewText(num, flag);
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

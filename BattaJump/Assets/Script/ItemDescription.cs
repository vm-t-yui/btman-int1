﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテムの説明切り替えクラス
/// </summary>
public class ItemDescription : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager = default;     //アイテムクラス
    [SerializeField] 
    Text displayName = default;         　　//アイテムの名前(表示用)
    [SerializeField] 
    Text displayDescription = default;  　　//アイテムの説明(表示用)
    [SerializeField] 
    Image displayImage = default;          //アイテムの画像(表示用)


    const int DescriptionNum = ItemManager.ItemNum + 1;     //アイテム説明の数(アイテム総数 + 入手してない時の???)
    int selectingNum = 0;     //現在選ばれているアイテムの番号

    [SerializeField]
    string[] itemName = new string[DescriptionNum];         //アイテムの名前(データ用)
    [SerializeField]
    string[] itemDescription = new string[DescriptionNum];  //アイテムの説明(データ用)

    /// <summary>
    /// 押したボタンに応じてアイテム名、説明表示
    /// </summary>
    /// <param name="num">ボタンの番号.</param>
    public void OnClickDescription(int num)
    {
        //入手しているアイテムならそのアイテムの説明表示、していなかったら説明なし
        if (itemManager.GetIsHasItem(num))
        {
            //NOTE: +1 はまだ入手していない時の項目によるずれ
            int itemNum = num + 1;

            selectingNum = itemNum;
            displayImage.sprite = ItemDataObject.Instance.GetSprite(itemNum);
            displayName.text = itemName[itemNum];
            displayDescription.text = itemDescription[itemNum];
        }
        else
        {
            selectingNum = 0;
            displayName.text = itemName[0];
            displayDescription.text = itemDescription[0];
        }
    }

    /// <summary>
    /// アイテムの名前のセット関数
    /// </summary>
    /// <param name="num">アイテムの番号</param>
    /// <param name="name">アイテムの名前</param>
    public void SetItemName(int num, string name)
    {
        itemName[num] = name;
    }

    /// <summary>
    /// アイテム説明のセット関数
    /// </summary>
    /// <param name="num">アイテムの番号</param>
    /// <param name="description">アイテムの説明</param>
    public void SetItemDescription(int num, string description)
    {
        itemDescription[num] = description;
    }

    /// <summary>
    /// 今選択しているアイテム番号のゲット関数
    /// </summary>
    /// <returns>今選択しているアイテム番号.</returns>
    public int GetSelectingNum()
    {
        return selectingNum;
    }
}

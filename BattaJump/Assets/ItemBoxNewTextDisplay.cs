using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムボックスのNewのテキスト表示
/// </summary>
public class ItemBoxNewTextDisplay : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager = default;  //アイテムマネージャー

    [SerializeField]
    GameObject newText = default;       //Newのテキスト

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        //新しいアイテムを全て確認し終えたら
        if(IsAllCheckNewItem())
        {
            //Newを消す
            newText.SetActive(false);
        }
        else
        {
            newText.SetActive(true);
        }
    }

    //新しいアイテムを全て確認し終えたかどうか
    bool IsAllCheckNewItem()
    {
        int count = 0;  //確認カウント数

        for (int i = 0; i < ItemManager.ItemNum; i++)
        {
            if(itemManager.GetIsNewText(i))
			{
                count++;
			}
        }
        
        //カウント数が0なら確認済
        if(count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

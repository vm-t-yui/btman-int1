using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテムの説明切り替えクラス
/// </summary>
public class ItemDescription : MonoBehaviour
{
    [SerializeField] Text displayName = default;         　　//アイテムの名前(表示用)
    [SerializeField] Text displayDescription = default;  　　//アイテムの説明(表示用)

    const int DescriptionNum = ItemManager.Num + 1;         //アイテム説明の数(アイテム総数 + 入手してない時の???)
    int selectingNum = 0;     //現在選ばれているアイテムの番号

    string[] itemName = new string[DescriptionNum];         //アイテムの名前(データ用)
    string[] itemDescription = new string[DescriptionNum];  //アイテムの説明(データ用)

    /// <summary>
    /// 押したボタンに応じてアイテム名、説明表示
    /// </summary>
    /// <param name="num">ボタンの番号.</param>
    public void OnClickDescription(int num)
    {
        selectingNum = num;
        displayName.text = itemName[num]; 
        displayDescription.text = itemDescription[num];
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

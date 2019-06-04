using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテムの説明切り替えクラス
/// </summary>
public class ItemDescription : MonoBehaviour
{
    [SerializeField] Text itemName;         //アイテムの名前
    [SerializeField] Text itemDescription;  //アイテムの説明

    //TODO:アイテムの説明はまだ決まっていないので、仮として〜番目のアイテム、〜番目のアイテムの説明にしています。
    //TODO:アイテムの詳細が決まれば、ちゃんとした説明を入れます。

    /// <summary>
    /// 1番目のアイテムの説明
    /// </summary>
    public void ItemDescription1()
    {
        itemName.text = "1番目のアイテム";
        itemDescription.text = "1番目のアイテムの説明";
    }

    /// <summary>
    /// 2番目のアイテムの説明
    /// </summary>
    public void ItemDescription2()
    {
        itemName.text = "2番目のアイテム";
        itemDescription.text = "2番目のアイテムの説明";
    }

    /// <summary>
    /// 3番目のアイテムの説明
    /// </summary>
    public void ItemDescription3()
    {
        itemName.text = "3番目のアイテム";
        itemDescription.text = "3番目のアイテムの説明";
    }

    /// <summary>
    /// 4番目のアイテムの説明
    /// </summary>
    public void ItemDescription4()
    {
        itemName.text = "4番目のアイテム";
        itemDescription.text = "4番目のアイテムの説明";
    }

    /// <summary>
    /// 5番目のアイテムの説明
    /// </summary>
    public void ItemDescription5()
    {
        itemName.text = "5番目のアイテム";
        itemDescription.text = "5番目のアイテムの説明";
    }

    /// <summary>
    /// 6番目のアイテムの説明
    /// </summary>
    public void ItemDescription6()
    {
        itemName.text = "6番目のアイテム";
        itemDescription.text = "6番目のアイテムの説明";
    }

    /// <summary>
    /// 7番目のアイテムの説明
    /// </summary>
    public void ItemDescription7()
    {
        itemName.text = "7番目のアイテム";
        itemDescription.text = "7番目のアイテムの説明";
    }

    /// <summary>
    /// 8番目のアイテムの説明
    /// </summary>
    public void ItemDescription8()
    {
        itemName.text = "8番目のアイテム";
        itemDescription.text = "8番目のアイテムの説明";
    }

    /// <summary>
    /// そのアイテムを手に入れていない時の説明
    /// </summary>
    public void NotGetItem()
    {
        itemName.text = "???";
        itemDescription.text = "アイテムを手に入れよう！";
    }
}

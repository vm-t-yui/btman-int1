using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムゲットクラス(プレイヤーにアタッチ)
/// </summary>
public class ItemGet : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager = default;

    /// <summary>
    /// アイテムにあたったらゲット
    /// </summary>
    /// <param name="item">アイテム</param>
    void OnTriggerEnter(Collider item)
    {
        //アイテムの表示用オブジェクトは反応させない
        if (LayerMask.LayerToName(item.gameObject.layer) != "ItemDisplayObject")
        {
            itemManager.GetItem(item.GetComponent<ItemController>().GetMyNum());
        }
    }
}

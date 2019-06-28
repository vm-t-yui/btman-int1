using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムゲットクラス(プレイヤーにアタッチ)
/// </summary>
public class PlayerItemGetEvent : MonoBehaviour
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

            // アイテム取得音を鳴らす
            AudioPlayer.instance.PlaySe(AudioPlayer.SeType.ItemGet);
        }
    }
}

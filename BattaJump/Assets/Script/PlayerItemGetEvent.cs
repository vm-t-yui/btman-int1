using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムゲットクラス(プレイヤーにアタッチ)
/// </summary>
public class PlayerItemGetEvent : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager = default;   //アイテムクラス

    [SerializeField]
    GameObject getParticle = default;    //ゲット時のパーティクル

    /// <summary>
    /// アイテムにあたったらゲット
    /// </summary>
    /// <param name="item">アイテム</param>
    void OnTriggerEnter(Collider item)
    {
        //アイテムの表示用オブジェクトは反応させない
        if (LayerMask.LayerToName(item.gameObject.layer) != "ItemDisplayObject")
        {
            //アイテムゲット
            itemManager.GetItem(item.GetComponent<ItemController>().GetMyNum());

            //アイテムゲット用パーティクル再生
            getParticle.SetActive(true);

            //アイテムを非表示に
            item.gameObject.SetActive(false);

            // アイテム取得音を鳴らす
            AudioPlayer.instance.PlaySe(AudioPlayer.SeType.ItemGet);
        }
    }
}

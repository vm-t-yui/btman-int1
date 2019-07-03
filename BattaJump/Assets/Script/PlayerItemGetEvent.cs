using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテムゲットクラス(プレイヤーにアタッチ)
/// </summary>
public class PlayerItemGetEvent : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager = default;   //アイテムクラス

    [SerializeField]
    GameObject getParticle = default;    //ゲット時のパーティクル

    [SerializeField]
    GameObject itemGetImage = default;   //アイテムゲット演出用オブジェクト

    [SerializeField]
    GameObject itemGetBackGround = default;   //アイテムゲット演出背景用オブジェクト

    /// <summary>
    /// アイテムにあたったらゲット
    /// </summary>
    /// <param name="item">アイテム</param>
    void OnTriggerEnter(Collider item)
    {
        //アイテムの表示用オブジェクトは反応させない
        if (LayerMask.LayerToName(item.gameObject.layer) != "ItemDisplayObject")
        {
            int itemNum = item.GetComponent<ItemController>().GetMyNum();

            //アイテムゲット
            itemManager.GetItem(itemNum);

            //アイテムゲット用パーティクル再生
            getParticle.SetActive(true);

            //アイテムを非表示に
            item.gameObject.SetActive(false);

            //アイテムゲット演出開始
            itemGetImage.SetActive(true);
            itemGetBackGround.SetActive(true);
            itemGetImage.GetComponent<Image>().sprite = ItemScriptableObject.Instance.GetSprite(itemNum);

            // アイテム取得音を鳴らす
            AudioPlayer.instance.PlaySe(AudioPlayer.SeType.ItemGet);
        }
    }
}

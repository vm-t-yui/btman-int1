using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// 米のスプライトセット
/// </summary>
public class RicePlantSet : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] riceSprite = default;  //米のオブジェクト

    [SerializeField]
    SpriteAtlas atlas = default;            //スプライトアトラス

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        //米のスプライトセットする
        for(int i = 0; i < riceSprite.Length; i++)
        {
            riceSprite[i].sprite = atlas.GetSprite("rice_plant_sprite");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;
#pragma warning disable RECS0018 // 等値演算子による浮動小数点値の比較

/// <summary>
/// アイテムとの距離メータークラス
/// </summary>
public class ItemDistanceMeter : MonoBehaviour
{
    [SerializeField]
    GameObject player = default;        //プレイヤー

    [SerializeField]
    GameObject meterIcon = default;     //メーターのアイコン

    [SerializeField]
    ItemCreater itemCreater = default;  //アイテム生成クラス

    [SerializeField]
    SpriteAtlas meterAtlas = default;   //メーターアイコンのスプライトアトラス

    [SerializeField]
    string[] atlasName = default;       //スプライトアトラスの検索用の名前

    [SerializeField]
    int maxDistance;              //アイテム表示の最大距離

    float[] posDifference = new float[ItemCreater.appearanceNum];   //プレイヤーの位置とアイテムの位置との距離の差分リスト

    [SerializeField]
    GameObject[] iconList;             //メーターアイコンのリスト

    bool isCreate = false;                                          //メーターが生成されたかどうかのフラグ

    /// <summary>
    /// アイテムアイコン作成
    /// </summary>
    /// <param name="i">アイコン番号</param>
    public void CreateIcon(int i)
    {
        //アイコンを複製
        Image iconImage = iconList[i].GetComponent<Image>();

        //0ならプレイヤー用アイコン作成
        if (i == 0)
        {
            iconImage.sprite = meterAtlas.GetSprite(atlasName[0]);
        }
        //それ以外ならレア度に応じたアイコン作成
        else
        {
            //アイテムのレアリティをセット
            Dictionary<int, float> itemRarity = ItemScriptableObject.Instance.GetItemRarity();

            //このアイテムのレアリティにあわせたアイコンを作成
            foreach(int key in itemRarity.Keys)
            {
                if (itemCreater.GetExistAllItemsRate(i - 1) == itemRarity[key])
                {
                    iconImage.sprite = meterAtlas.GetSprite(atlasName[key]);
                }
            }
        }
    }

    /// <summary>
    /// アイテムメーター作成
    /// </summary>
    public void CreateMeter()
    {
        for (int i = 0; i < ItemCreater.appearanceNum + 1; i++)
        {
            CreateIcon(i);
        }

        //生成完了
        isCreate = true;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        //ちゃんとメーターが生成されたら差分を取り始める
        if (isCreate)
        {
            //NOTE:[0]はプレイヤーのアイコンなので省く
            for (int i = 1; i < iconList.Length; i++)
            {
                //NOTE:i - 1は[0](プレイヤーとプレイヤーの差分は見ないため)のずれ
                //位置の差分をとって座標更新
                posDifference[i - 1] = GetPosDifference(i - 1);

                //メーターの座標が表示の最大距離を上回ったなら最大距離内に収める
                if (posDifference[i - 1] < 0)
                {
                    iconList[i].SetActive(false);
                }
                else
                {
                    iconList[i].transform.position = iconList[0].transform.position + new Vector3(0, (posDifference[i - 1]), 0);

                    //メーターの座標が表示の最大距離を上回ったなら最大距離内に収める
                    if (iconList[i].transform.position.y > iconList[0].transform.position.y + maxDistance)
                    {
                        iconList[i].transform.position = new Vector3(iconList[i].transform.position.x, iconList[0].transform.position.y + maxDistance, 0);
                    }
                }
            }
        }
    }

    /// <summary>
    /// プレイヤーとアイテムの位置の差分のゲット関数
    /// </summary>
    /// <param name="i">アイテムの番号</param>
    /// <returns>位置の差</returns>
    float GetPosDifference(int i)
    {
        return itemCreater.GetExistItemList(i).transform.position.y - player.transform.position.y;
    }
}

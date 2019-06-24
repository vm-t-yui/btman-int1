using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;

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
    GameObject canvas = default;        //カンバスのゲームオブジェクト

    [SerializeField]
    string[] atlasName = default;       //スプライトアトラスの検索用の名前

    [SerializeField]
    int maxDistance = 200;              //アイテム表示の最大距離

    float[] posDifference = new float[ItemCreater.appearanceNum];   //プレイヤーの位置とアイテムの位置との距離の差分リスト

    List<GameObject> iconList = new List<GameObject>();             //メーターアイコンのリスト

    bool isCreate = false;                                          //メーターが生成されたかどうかのフラグ

    /// <summary>
    /// アイテムアイコン作成
    /// </summary>
    /// <param name="i">アイコン番号</param>
    public void CreateIcon(int i)
    {
        //アイコンを複製
        GameObject icon = Instantiate(meterIcon);

        //0ならプレイヤー用アイコン作成
        if (i == 0)
        {
            icon.GetComponent<Image>().sprite = meterAtlas.GetSprite(atlasName[0]);
            icon.transform.position = new Vector3(550, 350, 0);
        }
        //それ以外ならレア度に応じたアイコン作成
        else
        {
            //各アイテムのレア度をゲット
            switch (itemCreater.GetExistAllItemsRate(i))
            {
                case 0.5f: icon.GetComponent<Image>().sprite = meterAtlas.GetSprite(atlasName[5]); break;
                case 2f: icon.GetComponent<Image>().sprite = meterAtlas.GetSprite(atlasName[4]); break;
                case 3f: icon.GetComponent<Image>().sprite = meterAtlas.GetSprite(atlasName[3]); break;
                case 3.5f: icon.GetComponent<Image>().sprite = meterAtlas.GetSprite(atlasName[2]); break;
                case 4f: icon.GetComponent<Image>().sprite = meterAtlas.GetSprite(atlasName[1]); break;
            }
        }

        //カンバスの子にしてアイコンのリストに追加
        icon.transform.parent = canvas.transform;
        iconList.Add(icon);
    }

    /// <summary>
    /// アイテムメーター作成
    /// </summary>
    public void CreateMeter()
    {
        for (int i = 0; i < ItemCreater.appearanceNum; i++)
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
            for (int i = 1; i < iconList.Count; i++)
            {
                //NOTE:i - 1は[0](プレイヤーとプレイヤーの差分は見ないため)のずれ
                //位置の差分をとって座標更新
                posDifference[i - 1] = GetPosDifference(i - 1);
                iconList[i].transform.position = iconList[0].transform.position + new Vector3(0, (posDifference[i - 1] / 2), 0);

                //メーターの座標が表示の最大距離を上回ったなら最大距離内に収める
                if (iconList[i].transform.position.y > iconList[0].transform.position.y + maxDistance)
                {
                    iconList[i].transform.position = new Vector3(iconList[i].transform.position.x, iconList[0].transform.position.y + maxDistance, 0);
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

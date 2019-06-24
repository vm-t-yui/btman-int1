using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム生成クラス
/// </summary>
public class ItemCreater : MonoBehaviour
{
    [SerializeField]
    GameObject parentItemObj = default;                         //空の親オブジェクト

    [SerializeField]
    string[] appearanceRate = new string[ItemManager.ItemNum];  //アイテムの出現確率
    [SerializeField]
    string[] appearancePlace = new string[ItemManager.ItemNum]; //アイテムの出現場所

    List<GameObject> existSkyItems = new List<GameObject>();    //空のオブジェクト
    List<GameObject> existSpaceItems = new List<GameObject>();  //宇宙のオブジェクト

    [SerializeField]
    int appearanceNum = 6;                                      //表示数
    [SerializeField]
    int skyBorder = 300;                                        //空の境目
    [SerializeField]
    int spaceItemInterval = 350;                                //宇宙のアイテムの間隔

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        //スクリプタブルオブジェクトからデータを取得
        appearanceRate = ItemScriptableObject.Instance.GetItemCsv(0);
        appearancePlace = ItemScriptableObject.Instance.GetItemCsv(1);

        //アイテム生成
        for (int i = 0; i < appearanceNum; i++)
        {
            CreateItem();
        }

        //空のアイテムのポジション決め
        for (int i = 0; i < existSkyItems.Count; i++) 
        {
            //NOTO: +1は空の境目と被らないため
            //アイテムの高さ決め
            float itemHeight = skyBorder / (existSkyItems.Count + 1) * (i + 1);

            existSkyItems[i].GetComponent<ItemAppearance>().SetPosition(new Vector3(0, itemHeight, 0));
        }

        //宇宙のアイテムのポジション決め
        for (int i = 0; i < existSpaceItems.Count; i++)
        {
            //アイテムの高さ決め
            float itemHeight = skyBorder + (i * spaceItemInterval + ((i  + 1) * 50));

            existSpaceItems[i].GetComponent<ItemAppearance>().SetPosition(new Vector3(0, itemHeight, 0));
        }
    }

    /// <summary>
    /// 出現するアイテムの番号
    /// </summary>
    /// <returns>アイテムの番号</returns>
    int AppearanceItemNum()
    {
        int index = 0;                              //回った回数
        float randomPoint = Random.value * 100;     //ランダム値

        foreach (var item in appearanceRate)
        {
            //回った回数をカウント
            index++;

            //ランダムで値をとる
            randomPoint = randomPoint - float.Parse(item);

            //番号を返す
            if (randomPoint < 0)
            { 
                return index;
            }
        }
        //万が一値が引きれなかったらうんこの化石
        return 0;
    }

    /// <summary>
    /// アイテム生成
    /// </summary>
    void CreateItem()
    {
        int itemNum = AppearanceItemNum();

        //空の親オブジェクトから親を複製、データオブジェクトからアイテムのモデルのプレハブを持ってきて子にする
        GameObject newParentItem = Instantiate(parentItemObj);
        GameObject newChildItem = Instantiate(ItemScriptableObject.Instance.GetItemPrefabs(itemNum));
        newChildItem.transform.parent = newParentItem.transform;
        newChildItem.transform.position = newParentItem.transform.position;

        //子オブジェクトにトリガーのコライダーとアイテム番号を追加
        newChildItem.AddComponent<SphereCollider>().isTrigger = true;
        newChildItem.GetComponent<SphereCollider>().radius = 8;
        newChildItem.AddComponent<ItemController>().SetMyNum(itemNum);

        //Skyなら空(0,1)、それ以外なら宇宙(2,3,4,5)に場所を指定する
        if (appearancePlace[itemNum] == "Sky")
        {
            existSkyItems.Add(newParentItem);
        } 
        else
        {
            existSpaceItems.Add(newParentItem);
        }
    }
}

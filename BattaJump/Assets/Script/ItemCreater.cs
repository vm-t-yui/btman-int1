using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム生成クラス
/// </summary>
public class ItemCreater : MonoBehaviour
{
    [SerializeField]
    ItemDistanceMeter itemDistanceMeter = default;

    [SerializeField]
    ItemManager itemManager = default;

    string[] appearanceRate = new string[ItemManager.ItemNum];  //アイテムの出現確率
    string[] appearancePlace = new string[ItemManager.ItemNum]; //アイテムの出現場所

    Dictionary<int, GameObject> existSkyItems = new Dictionary<int, GameObject>();  //空のアイテムリスト
    Dictionary<int, GameObject> existSpaceItems = new Dictionary<int, GameObject>();//宇宙のアイテムリスト

    [SerializeField]
    List<GameObject> existAllItems = new List<GameObject>();    //表示されるアイテムのオブジェクト
    [SerializeField]
    List<float> existAllItemsRate = new List<float>();          //表示されるアイテムの出現確立

    public const int appearanceNum = 4;                         //表示数
    [SerializeField]
    float skyBorder = 300;                                      //空の境目
    [SerializeField]
    float spaceItemInterval = 350;                              //宇宙のアイテムの間隔
    [SerializeField]
    float spaceItemPlusInterval = 50;                           //宇宙のアイテムの間隔の増加値

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
            //TODO:あとでちゃんとした広告リワードのフラグを入れるます
            bool isReward = true;      //仮の動画広告フラグ

            //動画広告をみたら一番最初に新規アイテム作成
            if (i == 0 && isReward)
            {
                CreateItem(true);
            }
            else
            {
                CreateItem(false);
            }
        }

        //それぞれのアイテムのポジション決め
        SetSkyItemPositon();
        SetSpaceItemPositon();

        //アイテムメーター作成
        itemDistanceMeter.CreateMeter();
    }

    /// <summary>
    /// アイテム生成
    /// </summary>
    /// <param name="isNewItem">新しいアイテムをつくるかどうか<c>true</c>作る</param>
    void CreateItem(bool isNewItem)
    {
        //生成するアイテムの番号を取得
        int itemNum = AppearanceItemNum(isNewItem);

        //アイテム番号が重複したら
        if (existSkyItems.ContainsKey(itemNum) || existSpaceItems.ContainsKey(itemNum))
        {
            //もう一回やり直す
            CreateItem(isNewItem);
        }
        else
        {
            //空の親オブジェクトから親を複製、データオブジェクトからアイテムのモデルのプレハブを持ってきて子にする
            GameObject newChildItem = Instantiate(ItemScriptableObject.Instance.GetItemPrefabs(itemNum));

            //子オブジェクトにトリガーのコライダーとアイテム番号を追加
            SphereCollider coll = newChildItem.AddComponent<SphereCollider>();
            coll.isTrigger = true;
            coll.radius = 8;

            newChildItem.AddComponent<ItemController>().SetMyNum(itemNum);

            //Skyなら空、それ以外なら宇宙のアイテムリストにいれる
            if (appearancePlace[itemNum] == "Sky")
            {
                existSkyItems.Add(itemNum, newChildItem);
            }
            else
            {
                existSpaceItems.Add(itemNum, newChildItem);
            }

            //それぞれの表示アイテムのリストに追加
            existAllItems.Add(newChildItem);
            existAllItemsRate.Add(float.Parse(appearanceRate[itemNum]));
        }
    }

    /// <summary>
    /// 空のアイテムの高さ決め
    /// </summary>
    void SetSkyItemPositon()
    {
        //回った回数
        int index = 0;

        foreach (int key in existSkyItems.Keys)
        {
            //NOTO: +1は空の境目と被らないため
            //アイテムの高さ
            float itemHeight = skyBorder / (existSkyItems.Count + 1) * (index + 1);
            existSkyItems[key].transform.position = (new Vector3(0, itemHeight, 0));

            index++;
        }
    }

    /// <summary>
    /// 宇宙のアイテムの高さ決め
    /// </summary>
    void SetSpaceItemPositon()
    {
        //回った回数
        int index = 0;

        foreach (int key in existSpaceItems.Keys)
        {
            //アイテムの高さ
            float itemHeight = skyBorder + (index * spaceItemInterval + ((index + 1) * spaceItemPlusInterval));
            existSpaceItems[key].transform.position = (new Vector3(0, itemHeight, 0));

            index++;
        }
    }

    /// <summary>
    /// 出現するアイテムの番号
    /// </summary>
    /// <returns>生成するアイテムの番号</returns>
    /// <param name="isNewItem">新しいアイテムを作るかどうか<c>true</c>作る</param>
    int AppearanceItemNum(bool isNewItem)
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
                //新しいアイテムを作らないならそのまま番号を渡す
                if(!isNewItem)
                {
                    return index;
                }
                //新しいアイテムを作るが、もともと所持しているものならもう一回
                else if (itemManager.GetIsHasItem(index))
                {
                    AppearanceItemNum(isNewItem);
                }
                //所持していないのでそのまま渡す
                else
                {
                    return index;
                }
            }
        }

        //万が一、値が引きれなかったらうんこの化石
        return 0;
    }


    /// <summary>
    /// 全ての表示アイテムのゲット関数
    /// </summary>
    /// <returns>表示アイテムのオブジェクト</returns>
    public GameObject GetExistItemList(int i)
    {
        return existAllItems[i];
    }

    /// <summary>
    /// 全ての表示アイテムのゲット関数
    /// </summary>
    /// <param name="i">アイテムの番号</param>
    /// <returns>表示アイテムの出現確率</returns>
    public float GetExistAllItemsRate(int i)
    {
        return existAllItemsRate[i];
    }
}

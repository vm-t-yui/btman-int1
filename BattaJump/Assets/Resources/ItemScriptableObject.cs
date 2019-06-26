using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

/// <summary>
/// アイテム用のScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "DataObject/Create ItemScriptableObject", fileName = "ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject
{
    static readonly string ResourcePath = "ItemScriptableObject";    //リソースのパス

    static ItemScriptableObject staticInstance = null;         //アイテム用のScriptableObjectクラス

    //リソース内のScriptableObjectロード
    public static ItemScriptableObject LoadResources()
    {
        return Resources.Load(ResourcePath) as ItemScriptableObject;
    }

    //ScriptableObjectインスタンス取得
    public static ItemScriptableObject Instance
    {
        get
        {
            if (staticInstance == null)
            {
                var asset = LoadResources();
                if (asset == null)
                {
                    asset = CreateInstance<ItemScriptableObject>();
                }

                staticInstance = asset;
            }

            return staticInstance;
        }
    }

    //↓こっからアイテム用のScriptableObjectの要素
    [SerializeField]
    SpriteAtlas itemAtlas = default;                                //アイテムボタン用のスプライトアトラス

    [SerializeField]
    int[] rarity;                                                   //アイテムのレアリティ
    [SerializeField]
    float[] rate;                                                   //アイテムの出現確率    

    Dictionary<int, float> itemRarity = new Dictionary<int, float>(); //アイテムの出現確率

    [SerializeField]
    GameObject[] itemPrefabs = new GameObject[ItemManager.ItemNum]; //アイテムのプレハブ

    [SerializeField]
    string[] itemName = new string[ItemManager.ItemNum];            //アイテムの名前

    [SerializeField]
    string[] itemAppearanceRate = new string[ItemManager.ItemNum];  //アイテムの出現確率

    [SerializeField]
    string[] itemAppearancePlace = new string[ItemManager.ItemNum]; //アイテムの出現場所

    /// <summary>
    /// 確率ロード
    /// </summary>
    public void ItemCsvLoad(List<string[]> csvData)
    {
        //csvファイルのデータを各言語配列に入れる
        for (int i = 0; i < ItemManager.ItemNum; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                //各言語の配列に入れる
                switch (j)
                {
                    case 0: itemName[i] = csvData[i][j]; break;
                    case 1: itemAppearanceRate[i] = csvData[i][j]; break;
                    case 2: itemAppearancePlace[i] = csvData[i][j]; break;
                }
            }
        }
    }

    /// <summary>
    /// アイテムのスプライト画像のゲット関数
    /// </summary>
    /// <returns>The sprite.</returns>
    /// <param name="i">アイテム番号</param>
    public Sprite GetSprite(int i)
    {
        return itemAtlas.GetSprite(itemName[i]);
    }

    /// <summary>
    /// アイテムCSVのゲット関数
    /// </summary>
    /// <returns>CSVに入っている名前、出現率、出現場所</returns>
    public string[] GetItemCsv(int num)
    {
        string[] returnText = new string[ItemManager.ItemNum];

        for (int i = 0; i < ItemManager.ItemNum; i++)
        {
            switch (num)
            {
                case 0: returnText = itemAppearanceRate; break;
                case 1: returnText = itemAppearancePlace; break;
            }
        }

        return returnText;
    }

    /// <summary>
    /// アイテムのプレハブのゲット関数
    /// </summary>
    /// <returns>The item prefabs.</returns>
    /// <param name="i">アイテム番号</param>
    public GameObject GetItemPrefabs(int i)
    {
        return itemPrefabs[i];
    }

    /// <summary>
    /// アイテムの出現確率のゲット関数
    /// </summary>
    /// <returns>The appearance rate.</returns>
    public Dictionary<int, float> GetItemRarity()
    {
        for (int i = 0; i < rarity.Length; i++)
        {
            itemRarity.Add(rarity[i], rate[i]);
        }

        return itemRarity;
    }
}
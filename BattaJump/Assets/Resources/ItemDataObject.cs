using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテム用のScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "DataObject/Create ItemDataObject", fileName = "ItemDataObject")]
public class ItemDataObject : ScriptableObject
{
    static readonly string ResourcePath = "ItemDataObject";    //リソースのパス

    static ItemDataObject staticInstance = null;

    //リソース内のアイテム用用ScriptableObject取得
    public static ItemDataObject Instance
    {
        get
        {
            if (staticInstance == null)
            {
                var asset = Resources.Load(ResourcePath) as ItemDataObject;
                if (asset == null)
                {
                    asset = CreateInstance<ItemDataObject>();
                }

                staticInstance = asset;
            }

            return staticInstance;
        }
    }

    //↓こっからアイテム用のScriptableObjectの要素

    [SerializeField] 
    Sprite[] itemSprite = new Sprite[ItemManager.ItemNum];   //アイテムのスプライト画像

    /// <summary>
    /// アイテムのスプライト画像のゲット関数
    /// </summary>
    /// <returns>The sprite.</returns>
    /// <param name="i">アイテム番号</param>
    public Sprite GetSprite(int i)
    {
        return itemSprite[i];
    }
}

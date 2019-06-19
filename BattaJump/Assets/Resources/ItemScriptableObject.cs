using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

/// <summary>
/// アイテム用のScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "DataObject/Create ItemDataObject", fileName = "ItemDataObject")]
public class ItemScriptableObject : ScriptableObject
{
    static readonly string ResourcePath = "ItemDataObject";    //リソースのパス

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
    SpriteAtlas itemButtonAtlas;                           //アイテムボタン用のスプライトアトラス

    [SerializeField]
    string[] itemName = new string[ItemManager.ItemNum];   //アイテムのスプライト画像

    /// <summary>
    /// アイテムのスプライト画像のゲット関数
    /// </summary>
    /// <returns>The sprite.</returns>
    /// <param name="i">アイテム番号</param>
    public Sprite GetSprite(int i)
    {
        return itemButtonAtlas.GetSprite(itemName[i]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

/// <summary>
/// アイテムボタン作成
/// </summary>
public class ItemButtonCreater : MonoBehaviour
{
    [SerializeField]
    GameObject originalButton = default;                //複製用ボタンのオリジナル

    [SerializeField]
    GameObject scrollViewContent = default;             //スクロールビューオブジェクト

    [SerializeField]
    ItemDescription itemDescription = default;          //アイテム説明クラス

    [SerializeField]
    ItemManager itemManager = default;                  //アイテムクラス

    [SerializeField]
    SpriteAtlas existenceSpriteAtlas = default;         //実態のスプライトアトラス

    [SerializeField]
    SpriteAtlas silhouetteSpriteAtlas = default;        //シルエットのスプライトアトラス

    string[] atlasKey = new string[ItemManager.ItemNum * 2];   //スプライトアトラスのKey

    List<GameObject> buttons = new List<GameObject>();  //作ったボタンのリスト

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        //アイテムボタンをを複製して、スクロールビューオブジェクトの子にする。
        for (int i = 0; i < ItemManager.ItemNum; i++)
        {
            //AddListener はアクションを渡す必要があるので、ラムダ式で簡単な無名関数を作って渡すようにする
            int index = i;

            GameObject duplicateButton = Instantiate(originalButton);
            duplicateButton.GetComponent<Button>().onClick.AddListener(() => itemDescription.OnClickDescription(index));
            duplicateButton.transform.parent = scrollViewContent.transform;
            duplicateButton.transform.localScale = new Vector3(1, 1, 1);
            buttons.Add(duplicateButton);
        }

        //スプライトアトラスの名前習得
        //for (int j = 0; j < atlasKey.Length; j++) 
        //{
        //    atlasKey[j] = GetAtlasKey(j);
        //} 
    }

    /// <summary>
    /// 起動時
    /// </summary>
    void OnEnable()
    {
        //ボタンが生成されているなら
        if(buttons != null)
        {
            //ボタンのアイテムゲット
            for (int i = 0; i < ItemManager.ItemNum; i++) 
            {
                //Image buttonImage = buttons[i].GetComponent<Image>();

                ////ゲットしているなら実態、していないならシルエットのみ
                //if (itemManager.GetIsHasItem(i))
                //{
                //    buttonImage.sprite = existenceSpriteAtlas.GetSprite(atlasKey[i]);
                //}
                //else
                //{
                //    buttonImage.sprite = silhouetteSpriteAtlas.GetSprite(atlasKey[i + ItemManager.ItemNum]);
                //}
            }
        }
    }

    /// <summary>
    /// 作ったボタンのリストのゲット関数
    /// </summary>
    /// <returns>作ったボタン</returns>
    public List<GameObject> GetCreateButton()
    {
        return buttons;
    }

    /// <summary>
    /// スプライトアトラスのKey取得
    /// </summary>
    /// <returns>スプライトアトラスのKey</returns>
    /// <param name="i">スプライトアトラスのKeyの番号</param>
    string GetAtlasKey(int i)
    {
        if (i >= ItemManager.ItemNum)
        {
            return "Silhouette" + ItemScriptableObject.Instance.GetName(i);
        }
        else
        {
            return ItemScriptableObject.Instance.GetName(i);
        }
    }
}

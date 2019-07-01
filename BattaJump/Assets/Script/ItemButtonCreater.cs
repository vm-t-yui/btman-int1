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

    string[] atlasKey = new string[ItemManager.ItemNum];   //スプライトアトラスのKey

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
        for (int j = 0; j < atlasKey.Length; j++) 
        {
            atlasKey[j] = GetAtlasKey(j);
        }

        //ボタンが生成されているなら
        if (buttons.Count == ItemManager.ItemNum)
        {
            //ボタンのアイテムゲット
            for (int i = 0; i < ItemManager.ItemNum; i++)
            {
                Image buttonImage = buttons[i].transform.FindChild("ItemImage").GetComponent<Image>();  //各アイテムボタンのイメージ
                GameObject newItemText = buttons[i].transform.FindChild("NewText").gameObject;          //New!!というテキスト

                //ゲットしているなら実態、していないならシルエットのみ
                if (itemManager.GetIsHasItem(i))
                {
                    buttonImage.sprite = existenceSpriteAtlas.GetSprite(atlasKey[i]);

                    //テキスト表示
                    if(itemManager.GetIsNewHasItem(i) == true)
                    {
                        newItemText.SetActive(true);
                    }
                }
                else
                {
                    buttonImage.sprite = existenceSpriteAtlas.GetSprite(atlasKey[i]);
                    buttonImage.color = Color.black;
                }
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
        return ItemScriptableObject.Instance.GetName(i);
    }
}

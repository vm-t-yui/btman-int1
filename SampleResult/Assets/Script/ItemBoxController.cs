using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム説明用ボタンの表示クラス
/// </summary>
public class ItemBoxController : MonoBehaviour
{
    [SerializeField]
    DataManager dataManager;        //データクラス

    [SerializeField]
    GameObject[] materializeButton = new GameObject[Item.count];   //アイテム説明ボタン
    [SerializeField]
    GameObject[] silhouetteButton = new GameObject[Item.count];    //アイテムのシルエットだけのアイテム説明ボタン

    [SerializeField]
    GameObject ItemBox = default;   //アイテムボックス

    [SerializeField]
    int[] isGetItem = new int[Item.count];           //アイテムゲットフラグ

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        //データをもらってくる
        for (int i = 0; i < Item.count; i++)
        {
            isGetItem[i] = dataManager.GetIsGetItem(i);
        }
    }

    /// <summary>
    /// 開始処理
    /// </summary>
    void Update()
    {
        //アイテムボタンの表示
        ButtonActive();
    }

    /// <summary>
    /// アイテムボタンの表示
    /// </summary>
    void ButtonActive()
    {
        for (int i = 0; i < Item.count; i++)
        {
            //アイテムをゲットしたらアイテム説明ボタン表示、していなかったらシルエットだけのボタン表示
            if (isGetItem[i] == 1)
            {
                materializeButton[i].SetActive(true);
                silhouetteButton[i].SetActive(false);
            }
            else
            {
                materializeButton[i].SetActive(false);
                silhouetteButton[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// アイテムボックス表示非表示
    /// </summary>
    public void ItemBoxActive()
    {
        //アイテムボックス表示非表示する
        if (ItemBox.activeInHierarchy)
        {
            ItemBox.SetActive(false);
        }
        else
        {
            ItemBox.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム説明用ボタンの表示クラス
/// </summary>
public class ItemBoxController : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager = default;      //データクラス

    [SerializeField]
    GameObject ItemBox = default;           //アイテムボックス
    [SerializeField]
    GameObject[] materializeButton = new GameObject[ItemManager.count];   //アイテム説明ボタン
    [SerializeField]
    GameObject[] silhouetteButton = new GameObject[ItemManager.count];    //アイテムのシルエットだけのアイテム説明ボタン

    [SerializeField]
    int[] isHaveItem = new int[ItemManager.count];  //アイテムゲットフラグ(PlayerPrefsにboolがないため仕方なくint使用)

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        //データをもらってくる
        for (int i = 0; i < ItemManager.count; i++)
        {
            isHaveItem[i] = itemManager.GetHaveItemFlag(i);
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
        for (int i = 0; i < ItemManager.count; i++)
        {
            //アイテムをゲットしたらアイテム説明ボタン表示、していなかったらシルエットだけのボタン表示
            if (isHaveItem[i] == 1)
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

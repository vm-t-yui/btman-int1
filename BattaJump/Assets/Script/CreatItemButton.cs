using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテムボタン作成
/// </summary>
public class CreatItemButton : MonoBehaviour
{
    [SerializeField]
    GameObject originalButton = default;        //複製用ボタンのオリジナル

    [SerializeField]
    GameObject scrollViewContent = default;     //スクロールビューオブジェクト

    [SerializeField]
    ItemDescription itemDescription = default;  //アイテム説明クラス

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        //アイテムボタンをを複製して、スクロールビューオブジェクトの子にする。
        for (int i = 0; i < ItemManager.ItemNum; i++)
        {
            //AddListener はアクションを渡す必要があるので、ラムダ式で簡単な無名関数を作って渡すようにする
            int index = i + 0;

            GameObject duplicateButton = Instantiate(originalButton);
            duplicateButton.GetComponent<Button>().onClick.AddListener(() => itemDescription.OnClickDescription(index));
            duplicateButton.transform.parent = scrollViewContent.transform;
        }
    }
}

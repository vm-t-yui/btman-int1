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
    GameObject originalButton = default;

    [SerializeField]
    GameObject scrollViewContent = default;

    [SerializeField]
    ItemDescription itemDescription = default;

    void Start()
    {
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

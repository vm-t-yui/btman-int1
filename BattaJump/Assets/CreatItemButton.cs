using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテムボタン作成
/// </summary>
public class CreatItemButton : MonoBehaviour
{
    Button button;
    [SerializeField]
    GameObject originalButton;
    [SerializeField]
    ItemDescription itemDescription = default;
    [SerializeField]
    bool isSilhouette;

    void CreateButton()
    {
        for (int i = 0; i < ItemManager.ItemNum; i++)
        {
            //AddListener はアクションを渡す必要があるので、ラムダ式で簡単な無名関数を作って渡すようにする
            int index = i + 0;

            GameObject duplicateButton = Instantiate(originalButton);
            duplicateButton.AddComponent(typeof(Button)) as Button;

            if (isSilhouette)
            {
                duplicateButton.button.onClick.AddListener(() => itemDescription.OnClickDescription(0));
            }
            else
            {
                duplicateButton.button.onClick.AddListener(() => itemDescription.OnClickDescription(index));
            }
        }
    }
}

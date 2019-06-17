using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewItemDisPlay : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager = default;
    [SerializeField]
    LocalizeController localizeController = default;  //ローカライズクラス
    [SerializeField]
    ItemDescription itemDescription = default;        //アイテム説明クラス
    [SerializeField]
    Animator animator;

    List<int> newHasNum = new List<int>();            //新しく手に入れたアイテムの数
    List<string> names = new List<string>();          //今の言語のアイテム名前
    List<string> descriptions = new List<string>();   //今の言語のアイテム説明
    bool[] isNewHasItem = new bool[ItemManager.ItemNum];  //新しくゲットしたアイテムのフラグ

    int touchCount = 0;     //タッチ数カウント

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        //新しくゲットしたアイテムのフラグを取得
        isNewHasItem = itemManager.GetIsNewHasItem();

        //新しくゲットしたアイテムの名前、説明を入れる
        for (int i = 0; i < isNewHasItem.Length; i++)
        {
            if (isNewHasItem[i])
            {
                newHasNum.Add(i);
                names.Add(LocalizeDataObject.Instance.GetLocalizeText(localizeController.GetLanguageNum(), (int)LocalizeDataObject.LocalizeText.ItemName1 + i));
                descriptions.Add(LocalizeDataObject.Instance.GetLocalizeText(localizeController.GetLanguageNum(), (int)LocalizeDataObject.LocalizeText.ItemDescription1 + i));
            }
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if(Input.touchCount > 0)
        {
            // タッチの情報を取得
            Touch touch = Input.GetTouch(0);
            // タッチされた回数をカウント
            if (touch.phase == TouchPhase.Began)
            {
                DisplayNewItem(touchCount);
                touchCount++;
            }
        }

        // 画面のクリック操作（エディタ用）
        if (Input.GetMouseButtonDown(0))
        {
            DisplayNewItem(touchCount);
            touchCount++;
        }
    }

    /// <summary>
    /// 新規アイテム描画
    /// </summary>
    /// <param name="i">The index.</param>
    void DisplayNewItem(int i)
    {
        animator.SetTrigger("In");

        //タップ数がアイテム数を上回らない限り表示
        if (i < newHasNum.Count)
        {
            //追加されたアイテム数名前、説明表示
            itemDescription.SetItemName(i, names[i]);
            itemDescription.SetItemDescription(i, descriptions[i]);
            itemDescription.OnClickDescription(i);
            animator.SetTrigger("Out");
        }
    }
}

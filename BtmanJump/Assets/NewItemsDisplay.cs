using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
///  新規取得アイテム表示クラス
/// </summary>
public class NewItemsDisplay : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager = default;                //アイテムクラス
    [SerializeField]
    LocalizeController localizeController = default;  //ローカライズクラス
    [SerializeField]
    ItemDescription itemDescription = default;        //アイテム説明クラス

    [SerializeField]
    Animator animator = default;                      //アニメータークラス

    [SerializeField]
    List<int> newHasNum = new List<int>();            //新しく手に入れたアイテムの数
    [SerializeField]
    List<string> names = new List<string>();          //今の言語のアイテム名前
    [SerializeField]
    List<string> descriptions = new List<string>();   //今の言語のアイテム説明

    [SerializeField]
    bool[] isNewHasItem = new bool[ItemManager.ItemNum];  //新しくゲットしたアイテムのフラグ

    [SerializeField]
    GameObject displayImage = default;                //アイテム取得演出表示用イメージ(子に名前と画像)

    [SerializeField]
    AnimationEndChecker animationEndChecker = default;    //画面フェードコントロールクラス

    [SerializeField]
    AdVideoRecommender adVideoRecommender = default;        //動画広告勧誘クラス

    [SerializeField]
    int touchCount = 0;     //タッチ数カウント

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        //新しくゲットしたアイテムのフラグを取得
        isNewHasItem = itemManager.GetIsNewHasItem();

        //そもそも全ての要素数がfalseなら非表示にする
        if (!IsAllItemInactive(isNewHasItem))
        {
            SetNewItem();

            //アイテム取得演出表示
            displayImage.SetActive(true);

            //最初の表示
            itemDescription.NewItemDescription(ItemScriptableObject.Instance.GetSprite(newHasNum[0]), names[0], descriptions[0]);
        }
        else
        {
            //子オブジェクト非表示
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // カンバスのボタンが表示されたら
        if (animationEndChecker.IsEnd)
        {
            // 新規アイテムがないなら動画広告勧誘表示して非表示にする
            if (IsAllItemInactive(isNewHasItem))
            {
                // 動画広告勧誘表示
                adVideoRecommender.Recommend();

                gameObject.SetActive(false);
            }
            else
            {           
                //画面フェードが終わった状態でタッチされたら
                if (Input.touchCount > 0)
                {
                    // タッチの情報を取得
                    Touch touch = Input.GetTouch(0);
                    // タッチされた回数をカウント
                    if (touch.phase == TouchPhase.Began)
                    {
                        touchCount++;
                        DisplayNewItem(touchCount);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 新しくゲットしたアイテムの情報をセット
    /// </summary>
    void SetNewItem()
    {
        for (int i = 0; i < isNewHasItem.Length; i++)
        {
            if (isNewHasItem[i])
            {
                newHasNum.Add(i);
                //NOTE: +1 はまだ入手していない時の項目によるずれ
                names.Add(LocalizeScriptableObject.Instance.GetLocalizeText(localizeController.GetLanguageNum(), (int)LocalizeScriptableObject.LocalizeText.NoItemName + (i + 1)));
                descriptions.Add(LocalizeScriptableObject.Instance.GetLocalizeText(localizeController.GetLanguageNum(), (int)LocalizeScriptableObject.LocalizeText.NoItemDescription + (i + 1)));
            }
        }
    }

    /// <summary>
    /// 新規アイテム描画
    /// </summary>
    /// <param name="i">The index.</param>
    void DisplayNewItem(int i)
    {
        animator.SetTrigger("Out");

        StartCoroutine(WaitAnimationEnd("ResultItemOut", i));
    }

    /// <summary>
    /// アニメーション終了を判定するコルーチン
    /// </summary>
    /// <returns>The animation end.</returns>
    /// <param name="animatorName">アニメーションの名前</param>
    /// <param name="i">画面タッチ数</param>
    private IEnumerator WaitAnimationEnd(string animatorName, int i)
    {
        bool finish = false;
        while (!finish)
        {
            AnimatorStateInfo nowState = animator.GetCurrentAnimatorStateInfo(0);
            if (nowState.IsName(animatorName))
            {
                //タップ数がアイテム数を上回らない限り表示
                if (i < newHasNum.Count)
                {
                    //新しく手に入れたアイテム説明
                    itemDescription.NewItemDescription(ItemScriptableObject.Instance.GetSprite(newHasNum[i]), names[i], descriptions[i]);
                    animator.SetTrigger("In");
                }
                //上回ったら非表示させる
                else
                {
                    // 動画広告勧誘表示
                    adVideoRecommender.Recommend();

                    //新規アイテム関連は非表示
                    displayImage.SetActive(false);
                    gameObject.SetActive(false);
                }

                finish = true;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    /// <summary>
    /// フラグの中の要素が全てfalseの時にTRUEを返す
    /// </summary>
    /// <returns><c>true</c>全てfalse<c>false</c>trueがある</returns>
    /// <param name="flag">フラグ</param>
    bool IsAllItemInactive(bool[] flag)
    {
        int count = 0;
        foreach (var item in isNewHasItem)
        {
            if (item == false)
            {
                count++;
            }
        }

        if (count == isNewHasItem.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

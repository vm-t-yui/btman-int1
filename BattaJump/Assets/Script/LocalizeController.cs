using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ローカライズクラス
/// </summary>
public class LocalizeController : MonoBehaviour
{

    [SerializeField] ItemDescription itemDescription = default;          //アイテム説明クラス

    [SerializeField]int LanguageNum = 0;     //言語番号

    [SerializeField] string[] localizeText;  //ローカライズされたテキスト
    [SerializeField] Text[] displayText;     //表示用テキスト
    [SerializeField] int[] displayTextNum; 　//表示用テキストの番号

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        //ローカライズのデータが保存されていないなら端末の言語設定から、
        //データが保存されているなら、保存されている言語番号からローカライズする
        switch (LanguageNum)
        {
            case (int)LocalizeDataObject.LocalizeLanguage.NoneData: TerminalLocalize(); break;
            default: LanguageNumLocalize(LanguageNum); break;
        }
    }

    /// <summary>
    /// 端末の言語設定に応じたローカライズ
    /// </summary>
    void TerminalLocalize()
    {
        //言語番号を更新 + ローカライズ
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Japanese: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.Japanese); 
                LanguageNum = (int)LocalizeDataObject.LocalizeLanguage.Japanese; break;
                
            case SystemLanguage.English: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.English);
                LanguageNum = (int)LocalizeDataObject.LocalizeLanguage.English; break;

            case SystemLanguage.German: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.German); 
                LanguageNum = (int)LocalizeDataObject.LocalizeLanguage.German; break;

            case SystemLanguage.Italian: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.Italian); 
                LanguageNum = (int)LocalizeDataObject.LocalizeLanguage.Italian; break;

            case SystemLanguage.French: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.French); 
                LanguageNum = (int)LocalizeDataObject.LocalizeLanguage.French; break;

            case SystemLanguage.Chinese: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.Chinese); 
                LanguageNum = (int)LocalizeDataObject.LocalizeLanguage.Chinese; break;

            case SystemLanguage.Spanish: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.Spanish); 
                LanguageNum = (int)LocalizeDataObject.LocalizeLanguage.Spanish; break;

            //7か国以外は英語に統一
            default:
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.English);
                LanguageNum = (int)LocalizeDataObject.LocalizeLanguage.English; break;
        }

        //ローカライズしたアイテム名、説明をセット
        SetLocalizeItem();

        //ローカライズしたテキストをセット
        SetLocalizeText();
    }

    /// <summary>
    /// 言語番号に応じたローカライズ
    /// </summary>
    /// <param name="num">言語番号(ボタンの中に入れた時はボタンの番号)</param>
    public void LanguageNumLocalize(int num)
    {
        //言語番号を更新
        LanguageNum = num;

        switch (num)
        {
            case (int)LocalizeDataObject.LocalizeLanguage.Japanese: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.Japanese); break;

            case (int)LocalizeDataObject.LocalizeLanguage.English: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.English); break;

            case (int)LocalizeDataObject.LocalizeLanguage.German: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.German); break;

            case (int)LocalizeDataObject.LocalizeLanguage.Italian: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.Italian); break;

            case (int)LocalizeDataObject.LocalizeLanguage.French: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.French); break;

            case (int)LocalizeDataObject.LocalizeLanguage.Chinese: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.Chinese); break;

            case (int)LocalizeDataObject.LocalizeLanguage.Spanish: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.Spanish); break;

            //7か国以外は英語に統一
            default: 
                localizeText = LocalizeDataObject.Instance.GetLocalizeText((int)LocalizeDataObject.LocalizeLanguage.English); 
                LanguageNum = (int)LocalizeDataObject.LocalizeLanguage.English; break;
        }

        //ローカライズしたアイテム名、説明をセット
        SetLocalizeItem();

        //ローカライズしたテキストをセット
        SetLocalizeText();
    }

    /// <summary>
    /// ローカライズしたアイテム名、説明をセットする
    /// </summary>
    public void SetLocalizeItem()
    {
        //アイテム説明クラスをゲットしていなかったら処理に入らない
        if (itemDescription != null)
        {
            //NOTE: +1 は入手してない時の???の分の誤差
            //アイテム名セット
            for (int i = (int)LocalizeDataObject.LocalizeText.NoItemName; i < (int)LocalizeDataObject.LocalizeText.NoItemDescription; i++)
            {
                //NOTE:配列が0から始まるのに対し、アイテム名が10から始まるのでその差分を引いた
                int itemNum = i - (int)LocalizeDataObject.LocalizeText.NoItemName;
                itemDescription.SetItemName(itemNum, localizeText[i]);
            }

            //アイテム説明セット
            for (int i = (int)LocalizeDataObject.LocalizeText.NoItemDescription; i < (int)LocalizeDataObject.LocalizeText.EnumLength; i++)
            {
                //NOTE:配列が0から始まるのに対し、アイテム名が10から始まるのでその差分を引いた
                int itemNum = i - (int)LocalizeDataObject.LocalizeText.NoItemDescription;
                itemDescription.SetItemDescription(itemNum, localizeText[i]);
            }

            //ローカライズした瞬間だけアイテムボタンを押さなくてもローカライズさせる
            itemDescription.OnClickDescription(itemDescription.GetSelectingNum());
        }
    }

    /// <summary>
    /// 指定した番号に応じた保存用テキストを表示用テキストに入れる
    /// </summary>
    void SetLocalizeText()
    {
        for (int i = 0; i < displayTextNum.Length; i++)
        {
            displayText[i].text = localizeText[displayTextNum[i]];
        }
    }


    /// <summary>
    /// 言語番号セット関数
    /// </summary>
    /// <param name="num">言語番号</param>
    public void SetLanguageNum(int num)
    {
        LanguageNum = num;
    }

    /// <summary>
    /// 言語番号ゲット関数
    /// </summary>
    /// <returns>言語番号</returns>
    public int GetLanguageNum()
    {
        return LanguageNum;
    }
}

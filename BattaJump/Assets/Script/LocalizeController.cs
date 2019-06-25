using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ローカライズクラス
/// </summary>
public class LocalizeController : MonoBehaviour
{

    [SerializeField]
    ItemDescription itemDescription = default;          //アイテム説明クラス

    int languageNum = -1;     //言語番号

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
        switch (languageNum)
        {
            case (int)LocalizeScriptableObject.LocalizeLanguage.NoneData: TerminalLocalize(); break;
            default: LanguageNumLocalize(languageNum); break;
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
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.Japanese);
                languageNum = (int)LocalizeScriptableObject.LocalizeLanguage.Japanese; break;

            case SystemLanguage.English:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.English);
                languageNum = (int)LocalizeScriptableObject.LocalizeLanguage.English; break;

            case SystemLanguage.German:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.German);
                languageNum = (int)LocalizeScriptableObject.LocalizeLanguage.German; break;

            case SystemLanguage.Italian:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.Italian);
                languageNum = (int)LocalizeScriptableObject.LocalizeLanguage.Italian; break;

            case SystemLanguage.French:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.French);
                languageNum = (int)LocalizeScriptableObject.LocalizeLanguage.French; break;

            case SystemLanguage.Chinese:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.Chinese);
                languageNum = (int)LocalizeScriptableObject.LocalizeLanguage.Chinese; break;

            case SystemLanguage.Spanish:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.Spanish);
                languageNum = (int)LocalizeScriptableObject.LocalizeLanguage.Spanish; break;

            //7か国以外は英語に統一
            default:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.English);
                languageNum = (int)LocalizeScriptableObject.LocalizeLanguage.English; break;
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
        languageNum = num;

        switch (num)
        {
            case (int)LocalizeScriptableObject.LocalizeLanguage.Japanese:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.Japanese); break;

            case (int)LocalizeScriptableObject.LocalizeLanguage.English:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.English); break;

            case (int)LocalizeScriptableObject.LocalizeLanguage.German:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.German); break;

            case (int)LocalizeScriptableObject.LocalizeLanguage.Italian:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.Italian); break;

            case (int)LocalizeScriptableObject.LocalizeLanguage.French:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.French); break;

            case (int)LocalizeScriptableObject.LocalizeLanguage.Chinese:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.Chinese); break;

            case (int)LocalizeScriptableObject.LocalizeLanguage.Spanish:
                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.Spanish); break;

            //7か国以外は英語に統一
            default:

                localizeText = LocalizeScriptableObject.Instance.GetLocalizeText((int)LocalizeScriptableObject.LocalizeLanguage.English);
                languageNum = (int)LocalizeScriptableObject.LocalizeLanguage.English; break;
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
            //アイテム名セット
            SetItemName();

            //アイテム説明セット
            SetItemDescription();
        }
    }

    /// <summary>
    /// アイテム名セット
    /// </summary>
    void SetItemName()
    {
        for (int i = (int)LocalizeScriptableObject.LocalizeText.NoItemName; i < (int)LocalizeScriptableObject.LocalizeText.NoItemDescription; i++)
        {
            //NOTE:配列が0から始まるのに対し、アイテム名前は0から始まらないので、その差分を引いた
            int itemNum = i - (int)LocalizeScriptableObject.LocalizeText.NoItemName;
            itemDescription.SetItemName(itemNum, localizeText[i]);
        }
    }

    /// <summary>
    /// アイテム説明のセット
    /// </summary>
    void SetItemDescription()
    {
        for (int i = (int)LocalizeScriptableObject.LocalizeText.NoItemDescription; i < (int)LocalizeScriptableObject.LocalizeText.EnumLength; i++)
        {
            //NOTE:配列が1から始まる(上記の最初の？？？の分)のに対し、アイテム説明は0から始まらないので、その差分を引いた
            int itemNum = i - (int)LocalizeScriptableObject.LocalizeText.NoItemDescription;
            itemDescription.SetItemDescription(itemNum, localizeText[i]);
        }

        //ローカライズした瞬間だけアイテムボタンを押さなくてもローカライズさせる
        itemDescription.OnClickDescription(itemDescription.GetSelectingNum());
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
        languageNum = num;
    }

    /// <summary>
    /// 言語番号ゲット関数
    /// </summary>
    /// <returns>言語番号</returns>
    public int GetLanguageNum()
    {
        return languageNum;
    }
    
    /// <summary>
    /// ローカライズされたテキストのゲット関数
    /// </summary>
    /// <returns>ローカライズされたテキスト</returns>
    /// <param name="i">要素番号</param>
    public string GetLocalizeText(int i)
    {
        return localizeText[i];
    }
}

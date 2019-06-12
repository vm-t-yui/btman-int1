using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ローカライズクラス
/// </summary>
public class LocalizeController : MonoBehaviour
{
    //言語
    enum LocalizeLanguage
    {
        NoneData = -1,  //データ無し
        Japanese,   //日本語
        English,    //英語
        German,     //ドイツ語
        Italian,    //イタリア語
        French,     //フランス語
        Chinese,    //中国語
        Spanish,    //スペイン語
        EnumLenght, //このenumのサイズ
    }

    //テキスト
    enum LocalizeText
    {
        Japanese,               //日本語
        English,                //英語
        German,                 //ドイツ語
        Italian,                //イタリア語
        French,                 //フランス語
        Chinese,                //中国語
        Spanish,                //スペイン語
        RecommendedApplication, //おすすめアプリ
        Retry,                  //リトライ
        Title,                  //タイトル
        NoItemName,             //アイテムの名前無し
        ItemName1,              //アイテムの名前1
        ItemName2,              //アイテムの名前2
        ItemName3,              //アイテムの名前3
        ItemName4,              //アイテムの名前4
        ItemName5,              //アイテムの名前5
        ItemName6,              //アイテムの名前6
        ItemName7,              //アイテムの名前7
        ItemName8,              //アイテムの名前8
        NoItemDescription,      //アイテムの説明無し
        ItemDescription1,       //アイテムの説明1
        ItemDescription2,       //アイテムの説明2
        ItemDescription3,       //アイテムの説明3
        ItemDescription4,       //アイテムの説明4
        ItemDescription5,       //アイテムの説明5
        ItemDescription6,       //アイテムの説明6
        ItemDescription7,       //アイテムの説明7
        ItemDescription8,       //アイテムの説明8
        AdvertisingPrompt,      //広告促し
        TapToStart,             //タップしてスタート
        EnumLenght,             //このenumのサイズ
    }

    [SerializeField] ItemDescription itemDescription = default;      //アイテム説明クラス

    const int LocalizeTextCount = (int)LocalizeText.EnumLenght;      //ローカライズするテキストの総数
    const int LanguageCount = (int)LocalizeLanguage.EnumLenght;      //ローカライズする言語の総数

    [SerializeField]int LanguageNum = 0;     //言語番号

    [SerializeField] Text[] displayText;                    //表示用テキスト
    [SerializeField] int[] displayTextNum;                  //表示用テキストの番号

    string[] localizeText = new string[LocalizeTextCount];   //保存用テキスト
    string[] japaneseText = new string[LocalizeTextCount];  //日本語テキスト
    string[] englishText = new string[LocalizeTextCount];   //英語テキスト
    string[] germanText = new string[LocalizeTextCount];    //ドイツ語テキスト
    string[] italianText = new string[LocalizeTextCount];   //イタリア語テキスト
    string[] frenchText = new string[LocalizeTextCount];    //フランス語テキスト
    string[] chineseText = new string[LocalizeTextCount];   //中国語テキスト
    string[] spanishText = new string[LocalizeTextCount];   //スペイン語テキスト

    /// <summary>
    /// CSVファイル読み込み
    /// </summary>
    /// <param name="csvData">Csv data.</param>
    public void TextLoad(List<string[]> csvData)
    {
        //csvファイルのデータを各言語配列に入れる
        for (int i = 0; i < LanguageCount; i++)
        {
            for (int j = 0; j < LocalizeTextCount; j++)
            {
                //各言語の配列に入れる
                switch (i)
                {
                    case (int)LocalizeLanguage.Japanese: japaneseText[j] = csvData[i][j]; break;
                    case (int)LocalizeLanguage.English : englishText[j]  = csvData[i][j]; break;
                    case (int)LocalizeLanguage.German  : germanText[j]   = csvData[i][j]; break;
                    case (int)LocalizeLanguage.Italian : italianText[j]  = csvData[i][j]; break;
                    case (int)LocalizeLanguage.French  : frenchText[j]   = csvData[i][j]; break;
                    case (int)LocalizeLanguage.Chinese : chineseText[j]  = csvData[i][j]; break;
                    case (int)LocalizeLanguage.Spanish : spanishText[j]  = csvData[i][j]; break;
                }
            }
        }

        //ローカライズのデータが保存されていないなら端末の言語設定から、
        //データが保存されているなら、保存されている言語番号からローカライズする
        switch (LanguageNum)
        {
            case (int)LocalizeLanguage.NoneData: TerminalLocalize(); break;
            default: LanguageNumLocalize(LanguageNum); break;
        }
    }

    /// <summary>
    /// 端末の言語設定に応じたローカライズ
    /// </summary>
    void TerminalLocalize()
    {
        for (int i = 0; i < LocalizeTextCount; i++)
        {
            //言語番号を更新 + ローカライズ
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Japanese: localizeText[i] = japaneseText[i]; LanguageNum = (int)LocalizeLanguage.Japanese; break;
                case SystemLanguage.English: localizeText[i] = englishText[i]; LanguageNum = (int)LocalizeLanguage.English; break;
                case SystemLanguage.German: localizeText[i] = germanText[i]; LanguageNum = (int)LocalizeLanguage.German; break;
                case SystemLanguage.Italian: localizeText[i] = italianText[i]; LanguageNum = (int)LocalizeLanguage.Italian; break;
                case SystemLanguage.French: localizeText[i] = frenchText[i]; LanguageNum = (int)LocalizeLanguage.French; break;
                case SystemLanguage.Chinese: localizeText[i] = chineseText[i]; LanguageNum = (int)LocalizeLanguage.Chinese; break;
                case SystemLanguage.Spanish: localizeText[i] = spanishText[i]; LanguageNum = (int)LocalizeLanguage.Spanish; break;

                //7か国以外は英語に統一
                default: localizeText[i] = englishText[i]; LanguageNum = (int)LocalizeLanguage.English; break;
            }
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
        Debug.Log(LanguageNum);
        Debug.Log(num);
        //言語番号を更新
        LanguageNum = num;

        for (int i = 0; i < LocalizeTextCount; i++)
        {
            switch (num)
            {
                case (int)LocalizeLanguage.Japanese: localizeText[i] = japaneseText[i]; break;
                case (int)LocalizeLanguage.English : localizeText[i] = englishText[i]; break;
                case (int)LocalizeLanguage.German  : localizeText[i] = germanText[i]; break;
                case (int)LocalizeLanguage.Italian : localizeText[i] = italianText[i]; break;
                case (int)LocalizeLanguage.French  : localizeText[i] = frenchText[i]; break;
                case (int)LocalizeLanguage.Chinese : localizeText[i] = chineseText[i]; break;
                case (int)LocalizeLanguage.Spanish : localizeText[i] = spanishText[i]; break;

                //7か国以外は英語に統一
                default: localizeText[i] = englishText[i]; LanguageNum = (int)LocalizeLanguage.English; break;
            }
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
            for (int i = (int)LocalizeText.NoItemName; i <= (int)LocalizeText.ItemName8; i++)
            {
                //NOTE:配列が0から始まるのに対し、アイテム名が10から始まるのでその差分を引いた
                int itemNum = i - (int)LocalizeText.NoItemName;
                itemDescription.SetItemName(itemNum, localizeText[i]);
            }

            //アイテム説明セット
            for (int i = (int)LocalizeText.NoItemDescription; i <= (int)LocalizeText.ItemDescription8; i++)
            {
                //NOTE:配列が0から始まるのに対し、アイテム名が10から始まるのでその差分を引いた
                int itemNum = i - (int)LocalizeText.NoItemDescription;
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

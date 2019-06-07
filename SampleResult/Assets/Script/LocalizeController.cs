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
    enum language
    {
        Japanese,
        English,
        German,
        Italian,
        French,
        Chinese,
        Spanish,
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
    }

    const int localizeTextCount = 30;      //ローカライズするテキストの総数
    const int languageCount = 7;

    [SerializeField] ItemDescription itemDescription = default;

    [SerializeField] Text[] localizeText = new Text[localizeTextCount];  //ローカライズしたいテキスト

    [SerializeField] string[] japaneseText = new string[localizeTextCount];  //日本語テキスト
    [SerializeField] string[] englishText = new string[localizeTextCount];   //英語テキスト
    [SerializeField] string[] germanText = new string[localizeTextCount];    //ドイツ語テキスト
    [SerializeField] string[] italianText = new string[localizeTextCount];   //イタリア語テキスト
    [SerializeField] string[] frenchText = new string[localizeTextCount];    //フランス語テキスト
    [SerializeField] string[] chineseText = new string[localizeTextCount];   //中国語テキスト
    [SerializeField] string[] spanishText = new string[localizeTextCount];   //スペイン語テキスト

    /// <summary>
    /// CSVファイル読み込み
    /// </summary>
    /// <param name="csvData">Csv data.</param>
    public void TextLoad(List<string[]> csvData)
    {
        //csvファイルのデータを各言語配列に入れる
        for (int i = 0; i < languageCount; i++)
        {
            for (int j = 1; j < localizeTextCount; j++)
            {
                //各言語の配列に入れる
                switch (i)
                {
                    case (int)language.Japanese: japaneseText[j - 1] = csvData[i][j]; break;
                    case (int)language.English : englishText[j - 1]  = csvData[i][j]; break;
                    case (int)language.German  : germanText[j - 1]   = csvData[i][j]; break;
                    case (int)language.Italian : italianText[j - 1]  = csvData[i][j]; break;
                    case (int)language.French  : frenchText[j - 1]   = csvData[i][j]; break;
                    case (int)language.Chinese : chineseText[j - 1]  = csvData[i][j]; break;
                    case (int)language.Spanish : spanishText[j - 1]  = csvData[i][j]; break;
                }
            }
        }

        //端末の言語設定に応じてローカライズ
        for (int i = 0; i < localizeTextCount; i++)
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Japanese: localizeText[i].text = japaneseText[i]; break;
                case SystemLanguage.English : localizeText[i].text = englishText[i]; break;
                case SystemLanguage.German  : localizeText[i].text = germanText[i]; break;
                case SystemLanguage.Italian : localizeText[i].text = italianText[i]; break;
                case SystemLanguage.French  : localizeText[i].text = frenchText[i]; break;
                case SystemLanguage.Chinese : localizeText[i].text = chineseText[i]; break;
                case SystemLanguage.Spanish : localizeText[i].text = spanishText[i]; break;

                default: localizeText[i].text = englishText[i]; break;
            }
        }

        //ローカライズしたアイテム名、説明をセット
        SetLocalizeItem();
    }

    /// <summary>
    /// ローカラズボタン用のローカライズ
    /// </summary>
    /// <param name="num">押したボタンの番号</param>
    public void OnClickLocalize(int num)
    {
        //押したボタンの番号に応じた言語にローカライズ
        for (int i = 0; i < localizeTextCount; i++)
        {
            switch (num)
            {
                case (int)language.Japanese: localizeText[i].text = japaneseText[i]; break;
                case (int)language.English : localizeText[i].text = englishText[i]; break;
                case (int)language.German  : localizeText[i].text = germanText[i]; break;
                case (int)language.Italian : localizeText[i].text = italianText[i]; break;
                case (int)language.French  : localizeText[i].text = frenchText[i]; break;
                case (int)language.Chinese : localizeText[i].text = chineseText[i]; break;
                case (int)language.Spanish : localizeText[i].text = spanishText[i]; break;
            }
        }

        //ローカライズしたアイテム名、説明をセット
        SetLocalizeItem();
    }

    /// <summary>
    /// ローカライズしたアイテム名、説明をセットする
    /// </summary>
    public void SetLocalizeItem()
    {
        //アイテム名
        for(int i = (int)LocalizeText.NoItemName; i <= (int)LocalizeText.ItemName8; i++)
        {
            int itemNum = i - (int)LocalizeText.NoItemName;
            itemDescription.SetItemName(itemNum, localizeText[i].text);
        }

        //アイテム説明
        for (int i = (int)LocalizeText.NoItemDescription; i <= (int)LocalizeText.ItemDescription8; i++)
        {
            int itemNum = i - (int)LocalizeText.NoItemDescription;
            itemDescription.SetItemDescription(itemNum, localizeText[i].text);
        }

        //ローカライズした瞬間だけアイテムボタンを押さなくてもローカライズさせる
        itemDescription.OnClickDescription(itemDescription.GetSelectingNum());
    }
}

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
        EnumLenght,             //このenumのサイズ
    }
    
    const int LocalizeTextCount = (int)LocalizeText.EnumLenght;      //ローカライズするテキストの総数
    const int LanguageCount = (int)LocalizeLanguage.EnumLenght;

    [SerializeField] ItemDescription itemDescription = default;

    [SerializeField] Text[] localizeText = new Text[LocalizeTextCount];  //ローカライズしたいテキスト

    [SerializeField] string[] japaneseText = new string[LocalizeTextCount];  //日本語テキスト
    [SerializeField] string[] englishText = new string[LocalizeTextCount];   //英語テキスト
    [SerializeField] string[] germanText = new string[LocalizeTextCount];    //ドイツ語テキスト
    [SerializeField] string[] italianText = new string[LocalizeTextCount];   //イタリア語テキスト
    [SerializeField] string[] frenchText = new string[LocalizeTextCount];    //フランス語テキスト
    [SerializeField] string[] chineseText = new string[LocalizeTextCount];   //中国語テキスト
    [SerializeField] string[] spanishText = new string[LocalizeTextCount];   //スペイン語テキスト

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

        //端末の言語設定に応じてローカライズ
        for (int i = 0; i < LocalizeTextCount; i++)
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
        for (int i = 0; i < LocalizeTextCount; i++)
        {
            switch (num)
            {
                case (int)LocalizeLanguage.Japanese: localizeText[i].text = japaneseText[i]; break;
                case (int)LocalizeLanguage.English : localizeText[i].text = englishText[i]; break;
                case (int)LocalizeLanguage.German  : localizeText[i].text = germanText[i]; break;
                case (int)LocalizeLanguage.Italian : localizeText[i].text = italianText[i]; break;
                case (int)LocalizeLanguage.French  : localizeText[i].text = frenchText[i]; break;
                case (int)LocalizeLanguage.Chinese : localizeText[i].text = chineseText[i]; break;
                case (int)LocalizeLanguage.Spanish : localizeText[i].text = spanishText[i]; break;
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
        //アイテム名セット
        for(int i = (int)LocalizeText.NoItemName; i <= (int)LocalizeText.ItemName8; i++)
        {
            //NOTE:配列が0から始まるのに対し、アイテム名が10から始まるのでその差分を引いた
            int itemNum = i - (int)LocalizeText.NoItemName;
            itemDescription.SetItemName(itemNum, localizeText[i].text);
        }

        //アイテム説明セット
        for (int i = (int)LocalizeText.NoItemDescription; i <= (int)LocalizeText.ItemDescription8; i++)
        {
            //NOTE:配列が0から始まるのに対し、アイテム名が10から始まるのでその差分を引いた
            int itemNum = i - (int)LocalizeText.NoItemDescription;
            itemDescription.SetItemDescription(itemNum, localizeText[i].text);
        }

        //ローカライズした瞬間だけアイテムボタンを押さなくてもローカライズさせる
        itemDescription.OnClickDescription(itemDescription.GetSelectingNum());
    }
}

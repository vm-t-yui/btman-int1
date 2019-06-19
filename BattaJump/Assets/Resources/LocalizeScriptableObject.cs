using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ローカライズ用のScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "DataObject/Create LocalizeScriptableObject", fileName = "LocalizeScriptableObject")]
public class LocalizeScriptableObject : ScriptableObject
{
    static readonly string ResourcePath = "LocalizeScriptableObject";    //リソースのパス

    static LocalizeScriptableObject staticInstance = null;         //アイテム用のScriptableObjectクラス

    //リソース内のScriptableObjectロード
    public static LocalizeScriptableObject LoadResources()
    {
        return Resources.Load(ResourcePath) as LocalizeScriptableObject;
    }

    //ScriptableObjectインスタンス取得
    public static LocalizeScriptableObject Instance
    {
        get
        {
            if (staticInstance == null)
            {
                var asset = LoadResources();
                if (asset == null)
                {
                    asset = CreateInstance<LocalizeScriptableObject>();
                }

                staticInstance = asset;
            }

            return staticInstance;
        }
    }

    //↓こっからローカライズ用のScriptableObjectの要素

    //言語
    public enum LocalizeLanguage
    {
        NoneData = -1,  //データ無し
        Japanese,   //日本語
        English,    //英語
        German,     //ドイツ語
        Italian,    //イタリア語
        French,     //フランス語
        Chinese,    //中国語
        Spanish,    //スペイン語
        EnumLength, //このenumのサイズ
    }

    //テキスト
    public enum LocalizeText
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
        AdvertisingPrompt,      //広告促し
        TapToStart,             //タップしてスタート
        NoItemName,             //アイテムの名前無し
        NoItemDescription = NoItemName + ItemManager.ItemNum + 1,      //アイテムの説明無し (NOTE: +1 はまだ入手していない時の項目によるずれ)
        EnumLength = NoItemDescription + ItemManager.ItemNum + 1,      //このenumのサイズ  (上記のずれ)
    }

    public const int LocalizeTextCount = (int)LocalizeText.EnumLength;   //ローカライズするテキストの総数
    public const int LanguageCount = (int)LocalizeLanguage.EnumLength;   //ローカライズする言語の総数

    [SerializeField]
    string[] japaneseText = new string[LocalizeTextCount];  //日本語テキスト
    [SerializeField]
    string[] englishText = new string[LocalizeTextCount];   //英語テキスト
    [SerializeField]
    string[] germanText = new string[LocalizeTextCount];    //ドイツ語テキスト
    [SerializeField]
    string[] italianText = new string[LocalizeTextCount];   //イタリア語テキスト
    [SerializeField]
    string[] frenchText = new string[LocalizeTextCount];    //フランス語テキスト
    [SerializeField]
    string[] chineseText = new string[LocalizeTextCount];   //中国語テキスト
    [SerializeField]
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
                    case (int)LocalizeLanguage.English: englishText[j] = csvData[i][j]; break;
                    case (int)LocalizeLanguage.German: germanText[j] = csvData[i][j]; break;
                    case (int)LocalizeLanguage.Italian: italianText[j] = csvData[i][j]; break;
                    case (int)LocalizeLanguage.French: frenchText[j] = csvData[i][j]; break;
                    case (int)LocalizeLanguage.Chinese: chineseText[j] = csvData[i][j]; break;
                    case (int)LocalizeLanguage.Spanish: spanishText[j] = csvData[i][j]; break;
                }
            }
        }
    }

    /// <summary>
    /// テキストのゲット関数(配列ごと)
    /// </summary>
    /// <returns>The localize text.</returns>
    /// <param name="num">言語番号</param>
    public string[] GetLocalizeText(int num)
    {
        string[] returnText = null;

        switch(num)
        {
            case (int)LocalizeLanguage.Japanese: returnText = japaneseText; break;
            case (int)LocalizeLanguage.English: returnText = englishText; break;
            case (int)LocalizeLanguage.German: returnText = germanText; break;
            case (int)LocalizeLanguage.Italian: returnText = italianText; break;
            case (int)LocalizeLanguage.French: returnText = frenchText; break;
            case (int)LocalizeLanguage.Chinese: returnText = chineseText; break;
            case (int)LocalizeLanguage.Spanish: returnText = spanishText; break;

            default: returnText = englishText; break;
        }

        return returnText;
    }

    /// <summary>
    /// テキストのゲット関数(ひとつずつ)
    /// </summary>
    /// <returns>The localize text.</returns>
    /// <param name="lNum">言語番号</param>
    /// <param name="eNum">配列の指定番号</param>
    public string GetLocalizeText(int lNum, int eNum)
    {
        string returnText = null;

        switch (lNum)
        {
            case (int)LocalizeLanguage.Japanese: returnText = japaneseText[eNum]; break;
            case (int)LocalizeLanguage.English: returnText = englishText[eNum]; break;
            case (int)LocalizeLanguage.German: returnText = germanText[eNum]; break;
            case (int)LocalizeLanguage.Italian: returnText = italianText[eNum]; break;
            case (int)LocalizeLanguage.French: returnText = frenchText[eNum]; break;
            case (int)LocalizeLanguage.Chinese: returnText = chineseText[eNum]; break;
            case (int)LocalizeLanguage.Spanish: returnText = spanishText[eNum]; break;

            default: returnText = englishText[eNum]; break;
        }

        return returnText;
    }
}

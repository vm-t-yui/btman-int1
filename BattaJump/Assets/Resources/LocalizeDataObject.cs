using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ローカライズ用のScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "Data/Create LocalizeDataObject", fileName = "LocalizeDataObject")]
public class LocalizeDataObject : ScriptableObject
{
    private static readonly string RESOURCE_PATH = "LocalizeDataObject";    //リソースのパス

    private static LocalizeDataObject s_instance = null;

    //リソース内のローカライズ用ScriptableObject取得
    public static LocalizeDataObject Instance
    {
        get
        {
            if (s_instance == null)
            {
                var asset = Resources.Load(RESOURCE_PATH) as LocalizeDataObject;
                if (asset == null)
                {
                    asset = CreateInstance<LocalizeDataObject>();
                }

                s_instance = asset;
            }

            return s_instance;
        }
    }

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

    [SerializeField]
    string[] localizeText = new string[LocalizeController.LocalizeTextCount];   //保存用テキスト
    [SerializeField]
    string[] japaneseText = new string[LocalizeController.LocalizeTextCount];  //日本語テキスト
    [SerializeField]
    string[] englishText = new string[LocalizeController.LocalizeTextCount];   //英語テキスト
    [SerializeField]
    string[] germanText = new string[LocalizeController.LocalizeTextCount];    //ドイツ語テキスト
    [SerializeField]
    string[] italianText = new string[LocalizeController.LocalizeTextCount];   //イタリア語テキスト
    [SerializeField]
    string[] frenchText = new string[LocalizeController.LocalizeTextCount];    //フランス語テキスト
    [SerializeField]
    string[] chineseText = new string[LocalizeController.LocalizeTextCount];   //中国語テキスト
    [SerializeField]
    string[] spanishText = new string[LocalizeController.LocalizeTextCount];   //スペイン語テキスト

    /// <summary>
    /// CSVファイル読み込み
    /// </summary>
    /// <param name="csvData">Csv data.</param>
    public void TextLoad(List<string[]> csvData)
    {
        //csvファイルのデータを各言語配列に入れる
        for (int i = 0; i < LocalizeController.LanguageCount; i++)
        {
            for (int j = 0; j < LocalizeController.LocalizeTextCount; j++)
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
    /// テキストのゲット関数
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
}

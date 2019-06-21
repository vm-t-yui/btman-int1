using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.U2D;

public class CSVRender : Editor
{
    static List<string[]> localizeCsvDatas = new List<string[]>(); // CSVの中身を入れるリスト;
    static string localizeFilePath = Application.dataPath + "/../Localize.csv";

    static List<string[]> itemCsvDatas = new List<string[]>(); // CSVの中身を入れるリスト;
    static string itemFilePath = Application.dataPath + "/../ItemApper.csv";

    /// <summary>
    /// csv読み込み
    /// </summary>
    [MenuItem("Editor/Load")]
    static void Load()
    {
        //各種ロード
        LocalizeLoad();
        ItemLoad();
    }

    /// <summary>
    /// ローカライズcsvロード
    /// </summary>
    static void LocalizeLoad()
    {
        string[] allText = File.ReadAllLines(localizeFilePath);
        string[] lineText = new string[LocalizeScriptableObject.LanguageCount];

        //NOTE:どう分けてもFile.ReadAllLinesは一気に56個しか読めないみたいで、
        //(1行,2行を50,50にすると、[0]には1行目の50、[1]には2行目の6まで、[2]には2行目の残りの44が入る、みたいなこと)
        //ちゃんと分けても読み込む時に4列になってしまったので下のような形式にした
        for (int j = 0; j < allText.Length; j++)
        {
            Debug.Log(allText[j]);
        }
        for (int i = 0; i < lineText.Length; i++)
        {
            lineText[i] = allText[i * 3] + allText[i * 3 + 1] + allText[i * 3 + 2];
        }

        foreach (var text in lineText)
        {
            localizeCsvDatas.Add(text.Split(',')); // , 区切りでリストに追加
        }

        //読み込んだデータをローカライズのデータオブジェクトへ
        LocalizeScriptableObject.Instance.TextLoad(localizeCsvDatas);
    }

    /// <summary>
    /// アイテムcsvロード
    /// </summary>
    static void ItemLoad()
    {
        string[] allText = File.ReadAllLines(itemFilePath);

        foreach (var text in allText)
        {
            itemCsvDatas.Add(text.Split(',')); // , 区切りでリストに追加
        }

        //読み込んだデータをローカライズのデータオブジェクトへ
        ItemScriptableObject.Instance.ItemCsvLoad(itemCsvDatas);
    }
}

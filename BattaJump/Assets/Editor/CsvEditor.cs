using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CsvEditor : Editor
{
    static List<string[]> localizeCsvDatas = new List<string[]>(); // CSVの中身を入れるリスト;
    static string localizeFilePath = Application.dataPath + "/../ItemCSV.csv";

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

        foreach (var text in allText)
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

using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// CSVファイル読み込み
/// </summary>
public class CSVReader : MonoBehaviour
{
    [SerializeField]
    TextAsset csvFile = default; // CSVファイル

    [SerializeField]
    LocalizeController localizeController = default;    //ローカライズクラス

    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Awake()
    {
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }

        //読み込んだデータをローカライズクラスへ
        localizeController.TextLoad(csvDatas);
    }
}
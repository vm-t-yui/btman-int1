using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ローカライズデータクラス
/// </summary>
public class LocalizeDataManager : MonoBehaviour
{
    //言語
    enum LocalizeLanguage
    {
        None = -1,  //データ無し
        Japanese,   //日本語
        English,    //英語
        German,     //ドイツ語
        Italian,    //イタリア語
        French,     //フランス語
        Chinese,    //中国語
        Spanish,    //スペイン語

    }

    [SerializeField]
    LocalizeController localizeController;  //スコアクラス

    int languageNum = 0;                    //言語番号

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Awake()
    {
        //データロード
        LoadData();
    }

    /// <summary>
    /// データロード
    /// </summary>
    public void LoadData()
    {
        //データロード
        languageNum = PlayerPrefs.GetInt("Language", -1);

        //ロードしたデータをセット
        localizeController.SetLanguageNum(languageNum);
    }

    /// <summary>
    /// データセーブ
    /// </summary>
    public void SaveData()
    {
        //データを取得してセット
        languageNum = localizeController.GetLanguageNum();
        PlayerPrefs.SetInt("Language", languageNum);

        //セットしたデータをセーブ
        PlayerPrefs.Save();
    }

    /// <summary>
    /// セーブデータ消去
    /// </summary>
    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("Language");
    }
}

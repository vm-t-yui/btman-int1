using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン切り替えクラス
/// </summary>
public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// シーンの番号
    /// </summary>
    public enum SceneNum
    {
        Title = 0,
        MainGame,
        Result,
        Num
    }

    bool[] isLoad = new bool[(int)SceneNum.Num];   //各シーンのロード用フラグ

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // フラグリセット
        for (int i = 0; i < (int)SceneNum.Num; i++)
        {
            isLoad[i] = false;
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        //各シーンのロード
        if (isLoad[(int)SceneNum.Title])
        {
            SceneManager.LoadScene("Title");      //タイトルシーン
        }
        if (isLoad[(int)SceneNum.MainGame])
        {
            SceneManager.LoadScene("MainGame");   //メインゲームシーン
        }
        if (isLoad[(int)SceneNum.Result])
        {
            SceneManager.LoadScene("Result");     //リザルトシーン
        }
    }

    /// <summary>
    /// ロード用フラグセット関数
    /// </summary>
    /// <param name="num">ロードしたいシーンの番号</param>
    public void OnLoad(SceneNum num)
    {
        isLoad[(int)num] = true;
    }
}
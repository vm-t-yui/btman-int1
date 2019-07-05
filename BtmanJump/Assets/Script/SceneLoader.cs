using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// シーンロードクラス
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    GameObject loadingUI = default;　// ロード中に表示するUI

    [SerializeField]
    Slider loadingGauge = default;   // ロードゲージ

    /// <summary>
    /// シーンの番号
    /// </summary>
    public enum SceneNum
    {
        Title = 0,    // タイトル
        MainGame,     // メインゲーム
        Result,       // リザルト
        Num           // シーンの数
    }

    Dictionary<SceneNum, string> scenes = new Dictionary<SceneNum, string>();    // シーンマップ

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // シーンマップに各シーンの情報を追加
        scenes.Add(SceneNum.Title, "Title");          // タイトル
        scenes.Add(SceneNum.MainGame, "MainGame");    // メインゲーム
        scenes.Add(SceneNum.Result, "Result");        // リザルト
    }

    /// <summary>
    /// ロード用フラグセット関数
    /// </summary>
    /// <param name="num">ロードしたいシーンの番号</param>
    public void OnLoad(SceneNum num)
    {
        loadingUI.SetActive(true);

        StartCoroutine(LoadScene(num));
    }

    /// <summary>
    /// ロード処理
    /// </summary>
    /// <param name="num">ロードするシーン番号</param>
    /// <returns></returns>
    IEnumerator LoadScene(SceneNum num)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scenes[num]);

        // シーンのロードが完了するまでループ
        while (!async.isDone)
        {
            // ロードゲージに進捗状況を反映
            loadingGauge.value = async.progress;

            yield return null;
        }
    }
}
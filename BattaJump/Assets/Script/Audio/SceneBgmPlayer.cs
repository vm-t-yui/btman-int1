using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンのBGMの再生を行う
/// </summary>
public class SceneBgmPlayer : MonoBehaviour
{
    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        // シーンがロードされた際のコールバック関数として登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// シーンがロードされた際のコールバック
    /// </summary>
    /// <param name="sceneType">ロードされたシーン</param>
    /// <param name="lodeMode">ロードタイプ</param>
    /// memo : 引数のlodeModeは使用しない
    void OnSceneLoaded(Scene sceneType, LoadSceneMode lodeMode)
    {
        // 既に再生されているBGMを停止する
        AudioPlayer.instance.StopBgm();

        // シーンごとに再生するBGMを変更
        switch (sceneType.name)
        {
            // タイトル
            case "Title" :
                // タイトルBGMを再生
                AudioPlayer.instance.PlayBgm(AudioPlayer.BgmType.Title);
                break;

            // リザルト
            case "Result" :
                // リザルトBGMを再生
                AudioPlayer.instance.PlayBgm(AudioPlayer.BgmType.Result);
                break;
        }
    }
}

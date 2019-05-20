using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン管理用ネームスペース

/// <summary>
/// シーンコントロールクラス
/// </summary>
public class SceneControll : MonoBehaviour
{
    // シーンのステータス
    public enum SCENE_STATE
    {
        Title,
        Play,
        Result,
    }

    public static SCENE_STATE NowScene { get; private set; }    // 現在のシーン

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // タイトルシーンでのみシーンの状態を初期化
        if (SceneManager.GetActiveScene().name == "Title")
        {
            NowScene = SCENE_STATE.Title;
        }
    }

    /// <summary>
    /// ゲーム開始更新処理
    /// </summary>
    public void StartGame()
    {
        NowScene = SCENE_STATE.Play;
        SceneManager.LoadScene("MainGame");
    }

    /// <summary>
    /// リトライ時の更新処理
    /// </summary>
    public void RetryGame()
    {
        NowScene = SCENE_STATE.Play;
    }

    /// <summary>
    /// タイトルに戻る時の更新処理
    /// </summary>
    public void ReturnTitle()
    {
        NowScene = SCENE_STATE.Title;
        SceneManager.LoadScene("Title");
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        switch (NowScene)
        {
            case SCENE_STATE.Title:

                break;
            case SCENE_STATE.Play:

                break;
            case SCENE_STATE.Result:

                break;
        }

        Debug.Log(NowScene);
    }
}

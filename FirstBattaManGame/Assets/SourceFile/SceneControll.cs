using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン管理用ネームスペース

/// <summary>
/// シーン管理クラス
/// </summary>
public class SceneControll : MonoBehaviour
{
    // シーンのステータス
    public enum SCENE_STATE
    {
        TITLE,
        PLAY,
        RESULT,
    }

    public static SCENE_STATE NowScene { get; private set; }    // 現在のシーン

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            NowScene = SCENE_STATE.TITLE;
        }
    }

    /// <summary>
    /// ゲーム開始更新処理
    /// </summary>
    public void StartGame()
    {
        NowScene = SCENE_STATE.PLAY;
        SceneManager.LoadScene("MainGame");
    }

    /// <summary>
    /// タイトルに戻る時の更新処理
    /// </summary>
    public void ReturnTitle()
    {
        NowScene = SCENE_STATE.TITLE;
        SceneManager.LoadScene("Title");
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        switch (NowScene)
        {
            case SCENE_STATE.TITLE:

                break;
            case SCENE_STATE.PLAY:

                break;
            case SCENE_STATE.RESULT:

                break;
        }

        Debug.Log(NowScene);
    }
}

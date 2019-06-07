using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーン移行クラス
/// NOTE: m.tanaka ボタンで呼び出す場合もあるので、使い勝手がいいよう細かくpublic関数にしてます
/// </summary>
public class NextSceneChanger : MonoBehaviour
{
    [SerializeField]
    SceneLoader sceneLoader = default;                               // シーンロードクラス

    public SceneLoader.SceneNum NextSceneNum { get; private set; }   // 次のシーン

    /// <summary>
    /// 次のシーンセット関数（メインゲームをセット）
    /// </summary>
    public void SetNextSceneMainGame()
    {
        NextSceneNum = SceneLoader.SceneNum.MainGame;
    }

    /// <summary>
    /// 次のシーンセット関数（リザルトをセット）
    /// </summary>
    public void SetNextSceneResult()
    {
        NextSceneNum = SceneLoader.SceneNum.Result;
    }

    /// <summary>
    /// 次のシーンセット関数（タイトルをセット）
    /// </summary>
    public void SetNextSceneTitle()
    {
        NextSceneNum = SceneLoader.SceneNum.Title;
    }

    /// <summary>
    /// 次のシーンへ切り替え
    /// </summary>
    public void ChangeNextScene()
    {
        sceneLoader.OnLoad(NextSceneNum);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン変更クラス
/// </summary>
public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// メインゲームシーンロード
    /// </summary>
    public void LoadMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    /// <summary>
    /// リザルトシーンロード
    /// </summary>
    public void LoadResult()
    {
        SceneManager.LoadScene("Result");
    }

    /// <summary>
    /// タイトルシーンロード
    /// </summary>
    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    /// <summary>
    /// リトライ(メインゲームシーンロード)
    /// </summary>
    public void Retry()
    {
        SceneManager.LoadScene("MainGame");
    }
}
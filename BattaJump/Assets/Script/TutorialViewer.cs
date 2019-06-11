using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チュートリアル表示クラス
/// </summary>
public class TutorialViewer : MonoBehaviour
{
    [SerializeField]
    Canvas tutorialCanvas = default;                    // チュートリアル表示用カンバス

    public bool IsEnd { get; private set; } = false;    // 処理終了フラグ

    /// <summary>
    /// オブジェクト起動時
    /// </summary>
    void OnEnable()
    {
        // チュートリアル表示
        tutorialCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// チュートリアルを閉じる
    /// NOTE: m.tanaka チュートリアルのパネル自体をボタンにしているので、押されたときにこの関数を呼ぶ仕組みにしてます
    /// </summary>
    public void CloseTutorial()
    {
        tutorialCanvas.gameObject.SetActive(false);
        IsEnd = true;
    }
}

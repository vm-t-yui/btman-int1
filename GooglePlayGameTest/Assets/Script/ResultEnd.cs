using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルト終了検知クラス
/// </summary>
public class ResultEnd : MonoBehaviour
{
    public bool IsEnd { get; private set; } = false;    // 処理終了フラグ

    /// <summary>
    /// 終了処理
    /// NOTE: m.tanaka リトライかタイトルのどちらかのボタンが押されたら呼ぶ
    /// </summary>
    public void End()
    {
        IsEnd = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// メインゲーム終了検知クラス
/// FIXME: m.tanaka ループを通すため仮で実装、ゲーム部分とくっつけたら削除します。
/// </summary>
public class MainGameEnd : MonoBehaviour
{
    public bool IsEnd { get; private set; } = false;    // 処理終了フラグ

    /// <summary>
    /// 終了処理
    /// </summary>
    public void End()
    {
        IsEnd = true;
    }
}

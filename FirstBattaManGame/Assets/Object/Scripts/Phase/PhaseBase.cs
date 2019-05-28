using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  フェーズの基底クラス
/// </summary>
public class PhaseBase : MonoBehaviour
{
    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // MonoBehaviour内の関数の自動コールを無効化
        enabled = false;
    }

    /// <summary>
    /// フェーズの初期化
    /// </summary>
    public virtual void Initialize()
    {
        // 仮想関数
    }

    /// <summary>
    /// フェーズの更新
    /// </summary>
    public virtual void Update()
    {
        // 仮想関数
    }

    /// <summary>
    /// フェーズの終了
    /// （関数名がFinalizeだと「デストラクタに影響がでる」と警告がでる）
    /// </summary>
    public virtual void Cleanup()
    {
        // 仮想関数
    }
}

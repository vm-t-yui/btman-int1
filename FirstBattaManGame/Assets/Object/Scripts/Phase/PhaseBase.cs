using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  フェーズの基底クラス
/// </summary>
public class PhaseBase : MonoBehaviour
{
    /// <summary>
    /// フェーズの初期化
    /// </summary>
    public virtual void Initializer()
    {
        // 仮想関数
    }

    /// <summary>
    /// フェーズの更新
    /// </summary>
    public virtual void Updater()
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

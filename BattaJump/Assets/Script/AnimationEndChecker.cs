using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アニメーション終了検知用クラス
/// </summary>
public class AnimationEndChecker : MonoBehaviour
{
    public bool IsEnd { get; private set; } = false;

    /// <summary>
    /// アニメーション終了時にイベントで呼ぶ
    /// </summary>
    public void End()
    {
        IsEnd = true;
    }
}

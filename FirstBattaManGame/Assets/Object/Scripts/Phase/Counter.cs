using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カウント関連のクラス
/// </summary>
public class Counter
{
    // カウントの値
    static public float CurrentCountNum { get; private set; } = 0;

    /// <summary>
    /// カウントをセットする
    /// </summary>
    /// <param name="count">セットする値</param>
    static public void SetCount(float count)
    {
        CurrentCountNum = count;
    }

    /// <summary>
    /// カウントダウンを行う
    /// </summary>
    static public void CountDown()
    {
        CurrentCountNum -= Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの入力を制御するクラス
/// </summary>
public class InputController
{
    // タッチの回数
    public int TouchCount { get; private set; } = 0;

    /// <summary>
    /// タッチされた回数を記録
    /// </summary>
    public void TouchCounter()
    {
        // 画面のタッチ入力が行われていたら
        if (Input.touchCount > 0)
        {
            // タッチの情報を取得
            Touch touch = Input.GetTouch(0);
            // タッチされた回数をカウント
            if (touch.phase == TouchPhase.Began)
            {
                TouchCount++;
            }
        }
    }
}

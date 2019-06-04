using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの入力を制御するクラス
/// </summary>
public class InputController : MonoBehaviour
{
    // タッチの回数
    static public int  TouchCountNum { get; private set; } = 0;
    // 初めてタッチが行われたかどうか
    static public bool IsFirstTouch  { get; private set; } = false;

    /// <summary>
    /// タッチされた回数を記録
    /// </summary>
    static public void CountTouch()
    {
        // 画面のタッチ入力が行われていたら
        if (Input.touchCount > 0)
        {
            // 初めてタッチが行われたフラグを起こす
            IsFirstTouch = true;

            // タッチの情報を取得
            Touch touch = Input.GetTouch(0);
            // タッチされた回数をカウント
            if (touch.phase == TouchPhase.Began)
            {
                TouchCountNum++;
            }
        }
    }
}
　
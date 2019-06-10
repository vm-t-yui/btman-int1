using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの入力を制御するクラス
/// </summary>
public class InputController
{
    // タッチの回数
    static public int  TouchCountNum { get; private set; } = 0;
    // 初めてタッチが行われたかどうか
    static public bool IsFirstTouch  { get; private set; } = false;

    /// <summary>
    /// 初期化処理
    /// </summary>
    static public void Init()
    {
        IsFirstTouch = false;

        TouchCountNum = 0;
    }

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

        // 画面のクリック操作（エディタ用）
        if (Input.GetMouseButtonDown(0))
        {
            // 初めてタッチが行われたフラグを起こす
            IsFirstTouch = true;
            // タッチされた回数をカウント
            TouchCountNum++;
        }
    }
}
　
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
    // クラス起動フラグ
    static bool isAble = false;

    /// <summary>
    /// 起動時
    /// </summary>
    void OnEnable()
    {
        // クラスを起動させる
        isAble = true;
    }

    /// <summary>
    /// 終了時
    /// </summary>
    void OnDisable()
    {
        // クラスを停止する
        isAble = false;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    static public void Init()
    {
        TouchCountNum = 0;

        IsFirstTouch = false;
    }

    /// <summary>
    /// タッチされた回数を記録
    /// </summary>
    static public void CountTouch()
    {
        // クラス起動フラグがtrueでなければ処理を抜ける
        if (!isAble) { return; }

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
　
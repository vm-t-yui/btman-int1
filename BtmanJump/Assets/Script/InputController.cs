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
    /// 更新
    /// </summary>
    void Update()
    {
        // タッチの情報を取得
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // 最初の入力が行われたらフラグを起こす
            if (touch.phase == TouchPhase.Began)
            {
                IsFirstTouch = true;
            }
        }

        // 最初の入力が行われたらフラグを起こす
        if (Input.GetMouseButtonDown(0))
        {
            IsFirstTouch = true;
        }
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

        // タッチの情報を取得
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                TouchCountNum++;

                // チャージ中のタップ音を再生
                // memo : カウント数を２で割った余りを利用して、２種類のタップ音を交互に再生させる
                if (TouchCountNum % 2 == 0)
                {
                    AudioPlayer.instance.PlaySe(AudioPlayer.SeType.ChargeingTap1);
                }
                else
                {
                    AudioPlayer.instance.PlaySe(AudioPlayer.SeType.ChargeingTap2);
                }
            }
        }

#if UNITY_EDITOR
        // マウスクリック
        // NOTE : "Input.GetMouseButtonDown(0)"は実機だとタップとして扱われるので、プリプロセッサディレクティブで判別
        if (Input.GetMouseButtonDown(0))
        {
            // タッチされた回数をカウント
            TouchCountNum++;
        
            // チャージ中のタップ音を再生
            // memo : カウント数を２で割った余りを利用して、２種類のタップ音を交互に再生させる
            if (TouchCountNum % 2 == 0)
            {
                AudioPlayer.instance.PlaySe(AudioPlayer.SeType.ChargeingTap1);
            }
            else
            {
                AudioPlayer.instance.PlaySe(AudioPlayer.SeType.ChargeingTap2);
            }
        }
#endif
    }
}
　
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの動きを制御するクラス
/// </summary>
public class CameraMoveController : MonoBehaviour
{
    [SerializeField] Transform         playerTransform   = default;       // プレイヤーのトランスフォーム
    [SerializeField] JumpHeightCounter jumpHeightCounter = default;       // プレイヤーのジャンプ高さを計測するクラス

    // 現在のズームカウント数
    int currentZoomTimeCount = 0;

    readonly Vector3 jumpCameraPos      = new Vector3(-2,-0.8f,5);    // ジャンプ時のカメラの位置
    readonly Vector3 LookAtPosOffset    = new Vector3(0, 1, 0);       // 注視点のオフセット
    const    int     CameraMoveWaitTime = 5;                         // カメラの移動が開始するまでの待機時間
    const    int     ZoomTime           = 600;                        // ズーム時間
    const    float   ZoomSpeed          = 0.003f;                     // ズームスピード
    const    float   ZoomLerpRate       = 0.05f;                      // ズームのLerp率

    /// <summary>
    /// 更新
    /// FixedUpdate：モニターのレートによって処理速度が左右されないように
    /// </summary>
    void FixedUpdate()
    {
        // プレイヤーにカメラを向ける
        transform.LookAt(playerTransform.position + LookAtPosOffset);

        // 最初のタップ操作が行われるまで更新処理をスキップする
        if (!InputController.IsFirstTouch) { return; }
        currentZoomTimeCount++;

        // 指定の時間だけ、プレイヤーにズームし続ける
        if (currentZoomTimeCount < ZoomTime)
        {
            transform.position += new Vector3(0, 0, ZoomSpeed);
        }
        // プレイヤーに向いてる状態でしばらく待機する
        // 待機したあとはLerpでプレイヤーに近づいていく
        else if (currentZoomTimeCount > ZoomTime + CameraMoveWaitTime)
        {
            // カメラをプレイヤーの子オブジェクトにする
            transform.parent = playerTransform;
            // Lerpを利用して移動する
            transform.localPosition = Vector3.Lerp(transform.localPosition, jumpCameraPos, ZoomLerpRate);

            // ジャンプ高さの結果がでたら
            if (jumpHeightCounter.IsJumpHeightResult)
            {
                // カメラとプレイヤーの親子関係を解除する
                transform.parent = null;
                // スクリプトをオフにする
                enabled = false;
            }


        }
    }
}

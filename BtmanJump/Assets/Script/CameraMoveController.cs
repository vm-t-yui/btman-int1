using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの動きを制御するクラス
/// </summary>
public class CameraMoveController : MonoBehaviour
{
    [SerializeField] Transform         playerTransform   = default;       // プレイヤーのトランスフォーム
    [SerializeField] JumpHeightCounter jumpHeightCounter = default;       // プレイヤーのジャンプ高さを計測するクラス
    [SerializeField] CloudCreater cloudCreater = default;
    [SerializeField] ViewOutObjectHider mapObjectHider = default;

    // 現在のズームカウント数
    int currentZoomTimeCount = 0;

    readonly Vector3 jumpCameraPos         = new Vector3(-2.5f,2.5f,7);   // ジャンプ時のカメラの位置
    readonly Vector3 LookAtPosGroundOffset = new Vector3(0, 1, 0);        // 注視点のオフセット
    readonly Vector3 LookAtPosJumpOffset   = new Vector3(0, 3, 0);        // 注視点のオフセット
    const    int     CameraMoveWaitTime    = 50;                          // カメラの移動が開始するまでの待機時間
    const    int     ZoomTime              = 500;                         // ズーム時間
    const    float   ZoomSpeed             = 0.05f;                   　  // ズームスピード
    const    float   ZoomLerpRate          = 0.05f;                       // ズームのLerp率
    const    float   DefaultFieldOfView    = 60f;                         // デフォルトの視野

    bool isChace = false;                                                 // 追跡フラグ

    /// <summary>
    /// 更新
    /// FixedUpdate：モニターのレートによって処理速度が左右されないように
    /// </summary>
    void FixedUpdate()
    {
        // ズーム時のカウントをインクリメント
        currentZoomTimeCount++;

        // 指定の時間だけ、プレイヤーにズームし続ける
        if (currentZoomTimeCount < ZoomTime)
        {
            //transform.position += new Vector3(0, 0, ZoomSpeed);
            Camera.main.fieldOfView -= ZoomSpeed;
        }
        // プレイヤーに向いてる状態でしばらく待機する
        // 待機したあとはLerpでプレイヤーに近づいていく
        else if (currentZoomTimeCount > ZoomTime + CameraMoveWaitTime)
        {
            // カメラをプレイヤーの子オブジェクトにする
            transform.parent = playerTransform;

            // カメラの位置を更新して追跡開始
            if (!isChace)
            {
                transform.localPosition = jumpCameraPos;

                // 視野を初期化
                Camera.main.fieldOfView = DefaultFieldOfView;

                // 雲の生成を開始
                cloudCreater.StartCreate();

                mapObjectHider.Hide();

                isChace = true;

                // 風切り音を再生する
                AudioPlayer.instance.PlaySe(AudioPlayer.SeType.WindNoise);
            }

            // Lerpを利用して移動する
            transform.localPosition = Vector3.Lerp(transform.localPosition, jumpCameraPos, ZoomLerpRate);

            // ジャンプ高さの結果がでたら
            if (jumpHeightCounter.IsJumpHeightResult)
            {
                // カメラとプレイヤーの親子関係を解除する
                transform.parent = null;
            }
        }

        // プレイヤーにカメラを向ける
        transform.LookAt(playerTransform.position + GetPosOffset());
    }

    /// <summary>
    /// 注視点のオフセットのゲット関数
    /// </summary>
    /// <returns>注視点のオフセット</returns>
    Vector3 GetPosOffset()
    {
        // 飛ん出る時のオフセット
        if (currentZoomTimeCount > ZoomTime + CameraMoveWaitTime)
        {
            return LookAtPosJumpOffset;
        }
        // 地面にいる時のオフセット
        else
        {
            return LookAtPosGroundOffset;
        }
    }

    /// <summary>
    /// カメラの追跡フラグゲット関数
    /// </summary>
    /// <returns>カメラの追跡フラグ</returns>
    public bool GetIsChace()
    {
        return isChace;
    }
}

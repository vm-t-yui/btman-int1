using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 対象モデルの回転クラス
/// </summary>
public class ModelRotation : MonoBehaviour
{
    Vector2 currentTouchPos = Vector2.zero;     // 現在のタッチ位置の座標
    Vector2 prevTouchPos    = Vector2.zero;     // 前フレームのタッチ位置の座標]

    const float RotationSensitivity = 0.5f;     // モデルの回転の感度

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // タッチの入力状態を取得
        Touch touchInfo = Input.GetTouch(0);

        // スワイプ中の処理
        if (touchInfo.phase == TouchPhase.Moved)
        {
            // スワイプの方向を算出
            Vector2 swipeDir = touchInfo.deltaPosition.normalized;
            // 前フレームからのスワイプの移動量を算出
            float swipeMoveAmount = touchInfo.deltaPosition.magnitude;

            // スワイプの向きと移動量をもとにモデルを回転させる
            gameObject.transform.Rotate(new Vector3(swipeDir.y,-swipeDir.x, 0) * (swipeMoveAmount * RotationSensitivity), Space.World);
        }
    }
}

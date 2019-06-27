using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クレーター破片のコントロールクラス
/// </summary>
public class DebrisController : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidbody = default;    // Rigidbody

    [SerializeField]
    Vector3 Deceleration;             // 減速度

    /// <summary>
    /// 更新
    /// </summary>
    void FixedUpdate()
    {
        // 移動ベクトルを取得
        Vector3 velocity = rigidbody.velocity;

        // 減速
        velocity -= Deceleration;

        // 計算した移動ベクトルでvelocityを更新
        rigidbody.velocity = velocity;
    }
}

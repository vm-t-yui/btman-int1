using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クレーター破片のコントロールクラス
/// </summary>
public class DebrisController : MonoBehaviour
{
    [SerializeField]
    Rigidbody myRigid = default;            // Rigidbody

    [SerializeField]
    Vector3 Deceleration = Vector3.zero;    // 減速度

    /// <summary>
    /// 更新
    /// </summary>
    void FixedUpdate()
    {
        // 移動ベクトルを取得
        Vector3 velocity = myRigid.velocity;

        // 減速
        velocity -= Deceleration;

        // 計算した移動ベクトルでvelocityを更新
        myRigid.velocity = velocity;
    }
}

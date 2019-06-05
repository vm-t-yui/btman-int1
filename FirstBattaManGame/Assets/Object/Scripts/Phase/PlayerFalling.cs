using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジャンプ後の落下処理
/// </summary>
public class PlayerFalling : MonoBehaviour
{
    // プレイヤーのリジッドボディ
    [SerializeField] Rigidbody playerRigidbody = default;

    // 現在の待機時間
    int currentFallWaitTimeCount = 0;
    // 落下するまでの待機時間
    const int FallWaitTime = 60;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void OnEnable()
    {
        
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        currentFallWaitTimeCount++;
        // 指定の時間まで待機したら、プレイヤーの物理演算をオンにして落下させる
        if (currentFallWaitTimeCount > FallWaitTime)
        {
            // 物理演算をオンにする
            playerRigidbody.isKinematic = false;
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        
    }
}

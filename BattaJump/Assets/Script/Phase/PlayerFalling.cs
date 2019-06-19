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
    const int FallWaitTime = 20;
    // 落下フラグ
    bool isFalling = false;
    // 落下中のカウント
    float fallingTime = 0;
    // 落下させる秒数
    const float FallingMaxTime = 2;
    // 処理終了フラグ
    public bool IsEnd { get; private set; } = false;

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

            // 落下フラグを起こす
            isFalling = true;
        }

        // 落下が始まったらカウントを回す
        if (isFalling)
        {
            fallingTime += Time.deltaTime;

            // 指定した秒数経ったら処理終了
            if (fallingTime >= FallingMaxTime)
            {
                IsEnd = true;
            }
        }
    }
}

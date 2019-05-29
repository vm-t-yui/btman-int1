using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのジャンプを制御するクラス
/// </summary>
public class JumpController : MonoBehaviour
{
    // プレイヤーのリジッドボディ
    [SerializeField] Rigidbody playerRigidbody;
    // ジャンプ中どうか
    bool isJumping = false;

    /// <summary>
    /// プレイヤーをジャンプさせる
    /// </summary>
    /// <param name="power">ジャンプ力</param>
    public void PlayerJump(float power)
    {
        // プレイヤーに上方向に力を加える
        playerRigidbody.AddForce(Vector3.up * power, ForceMode.VelocityChange);
        // ジャンプフラグを立てる
        isJumping = true;
    }

    /// <summary>
    /// ジャンプの状態を監視する
    /// </summary>
    /// <returns>ジャンプ中かどうかを返す</returns>
    public bool ObserverJumpState()
    {
        // ジャンプ中の処理
        if (isJumping)
        {
            // ベロシティが下向きになったら
            if (playerRigidbody.velocity.y <= 0)
            {
                // ジャンプフラグを倒す
                isJumping = false;
                // プレイヤーの重力を停止する
                playerRigidbody.useGravity = false;
            }
        }

        return isJumping;
    }
}

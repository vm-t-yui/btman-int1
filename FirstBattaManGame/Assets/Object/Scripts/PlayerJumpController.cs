using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのジャンプ制御クラス
/// </summary>
public sealed class PlayerJumpController : MonoBehaviour
{
    [field:SerializeField]
    public Rigidbody PlayerRigidbody { get; private set; }            // プレイヤーのリジッドボディ
    public bool      IsJump          { get; private set; } = false;   // ジャンプしたかどうかのフラグ

    /// <summary>
    /// ジャンプを開始する
    /// </summary>
    public void StartJump(float jumpPower)
    {
        // プレイヤーに蓄積されたジャンプ力を与える
        PlayerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
        // ジャンプフラグをtrueに変更する
        IsJump = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのジャンプを制御するクラス
/// </summary>
public class JumpController : MonoBehaviour
{
    // プレイヤーのリジッドボディ
    [SerializeField] Rigidbody playerRigidbody = default;
    // ジャンプ中どうか
    public bool IsJumping { get; private set; } = false;

    // ワンタップあたりのジャンプ力
    const float OneTouchJumpPower = 5;
    // 初期ジャンプ力（タップ回数に関係なく必ず与えられるジャンプ力）
    const float InitJumpPower = 20;

    /// <summary>
    /// プレイヤーをジャンプさせる
    /// </summary>
    /// <param name="touchCount">タッチのカウント数</param>
    public void PlayerJump(int touchCount)
    {
        // ジャンプ力を算出 (初期ジャンプ力 + (ワンタップのジャンプ力 * タップ回数))
        float jumpPower = InitJumpPower + (OneTouchJumpPower * touchCount);

        // プレイヤーに上方向に力を加える（ジャンプ力＝ワンタップあたりのジャンプ力＊タッチされた数）
        playerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
        // ジャンプフラグを立てる
        IsJumping = true;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // ジャンプ中の処理
        if (IsJumping)
        {
            // ベロシティが下向きになったら
            if (playerRigidbody.velocity.y < 0)
            {
                // コールバック関数を呼ぶ
                OnJumpStop();
            }
        }
    }

    /// <summary>
    /// ジャンプが停止した場合のコールバック
    /// </summary>
    public void OnJumpStop()
    {
        // ジャンプフラグを倒す
        IsJumping = false;
        // プレイヤーの物理演算を停止する
        playerRigidbody.isKinematic = true;
    }
}

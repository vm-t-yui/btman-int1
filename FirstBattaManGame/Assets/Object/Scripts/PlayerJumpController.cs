using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのジャンプ制御クラス
/// </summary>
public sealed class PlayerJumpController : MonoBehaviour
{
    public new Rigidbody rigidbody { get; private set; }      // プレイヤーのリジッドボディ
    public new Transform transform { get; private set; }      // プレイヤーのトランスフォーム

    public float currentJumpPower             { get; set; }         = 0;       // 現在蓄積されているジャンプ力
    public float currentJumpHeight            { get; set; }         = 0;       // 現在のジャンプ高さ
    public float currentJumpHeightToKilometer { get; private set; } = 0;       // 現在のジャンプ高さ（キロメートル）
    public bool  isJump                       { get; private set; } = false;   // ジャンプしたかどうかのフラグ

    public readonly float OneTouchJumpPower = 5;                               // ワンタッチで蓄積されるジャンプ力

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // プレイヤーのリジッドボディコンポーネントを取得
        rigidbody = GetComponent<Rigidbody>();
        // プレイヤーのトランスフォームを取得
        transform = gameObject.transform;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void FixedUpdate()
    {
        if (currentJumpHeight != 0)
        {
            // 現在のジャンプ力をキロメートルに変換
            currentJumpHeightToKilometer = currentJumpHeight / GameCommonParameter.OneKiloMetreDistance;
        }
    }

    /// <summary>
    /// ジャンプを開始する
    /// </summary>
    public void StartJump()
    {
        // プレイヤーに蓄積されたジャンプ力を与える
        rigidbody.AddForce(Vector3.up * currentJumpPower, ForceMode.VelocityChange);
        // ジャンプフラグをtrueに変更する
        isJump = true;
    }
}

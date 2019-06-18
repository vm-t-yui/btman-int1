﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フェーズの状態を管理するクラス
/// </summary>
public class PhaseState : MonoBehaviour
{
    /// <summary>
    /// フェーズの種類
    /// </summary>
    public enum PhaseType
    {
        ChargeCountDown,       // ジャンプ力を溜めている際のカウントダウン
        JumpHeightCounter,     // プレイヤーのジャンプ高さを計測するクラス
        PlayerFalling,         // プレイヤーの落下演出
    }

    // ステートマシン
    StateMachine<PhaseType> stateMachine = new StateMachine<PhaseType>();

    [SerializeField] ChargeCountDown   chargeCountDown   = default;         // ジャンプ力の溜めている際のカウントダウン
    [SerializeField] JumpHeightCounter jumpHeightCounter = default;         // プレイヤーのジャンプ高さを計測するクラス
    [SerializeField] PlayerFalling     playerFalling     = default;         // プレイヤーの落下演出

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // "ChargeCountDown"の関数をステートマシンに追加
        stateMachine.Add(PhaseType.ChargeCountDown, EnterChargeCountDown, UpdateChargeCountDown, ExitChargeCountDown);
        // "JumpHeightCounter"の関数をステートマシンに追加
        stateMachine.Add(PhaseType.JumpHeightCounter, EnterJumpHeightCounter, UpdateJumpHeightCounter, ExitJumpHeightCounter);
        // "PlayerFalling"の関数をステートマシンに追加
        stateMachine.Add(PhaseType.PlayerFalling, EnterPlayerFalling, UpdatePlayerFalling, ExitPlayerFalling);

        // 最初のステートを"ChargeCountDown"にセット
        stateMachine.SetState(PhaseType.ChargeCountDown);
    }

    /// <summary>
    /// 更新
    /// FixedUpdate：モニターのレートによって処理速度が左右されないように
    /// </summary>
    void FixedUpdate()
    {
        // 各フェーズの更新を行う
        stateMachine.Update();
    }

    /// <summary>
    /// "ChargeCountDown"の初期化処理
    /// </summary>
    void EnterChargeCountDown()
    {
        // "ChargeCountDown"をtrueに設定
        chargeCountDown.enabled = true;
    }

    /// <summary>
    /// "ChargeCountDown"の更新処理
    /// </summary>
    void UpdateChargeCountDown()
    {
        // カウントダウンの値が０になったら
        if (chargeCountDown.CurrentCountNum <= 0.1f)
        {
            // ステートを"JumpHeightCounter"に変更する
            stateMachine.SetState(PhaseType.JumpHeightCounter);
        }
    }

    /// <summary>
    /// "ChargeCountDown"の終了処理
    /// </summary>
    void ExitChargeCountDown()
    {
        // "ChargeCountDown"をfalseに設定
        chargeCountDown.enabled = false;
    }

    /// <summary>
    /// "JumpHeightCounter"の初期化処理
    /// </summary>
    void EnterJumpHeightCounter()
    {
        // "JumpHeightCounter"をtrueに設定
        jumpHeightCounter.enabled = true;
    }

    /// <summary>
    /// "JumpHeightCounter"の更新処理
    /// </summary>
    void UpdateJumpHeightCounter()
    {
        // ジャンプ高さの結果が出たら
        if (jumpHeightCounter.IsJumpHeightResult)
        {
            // ステートを"PlayerFalling"に変更する
            stateMachine.SetState(PhaseType.PlayerFalling);
        }
    }

    /// <summary>
    /// "JumpHeightCounter"の終了処理
    /// </summary>
    void ExitJumpHeightCounter()
    {
        // "JumpHeightCounter"をfalseに設定
        jumpHeightCounter.enabled = false;
    }

    /// <summary>
    /// "EnterPlayerFalling"の初期化処理
    /// </summary>
    void EnterPlayerFalling()
    {
        // "PlayerFalling"をtrueに設定
        playerFalling.enabled = true;
    }

    /// <summary>
    /// "EnterPlayerFalling"の更新処理
    /// </summary>
    /// TODO：t.mitsumaru　未実装（今後処理を追加する可能性があるため関数のみを追加）
    void UpdatePlayerFalling()
    {

    }

    /// <summary>
    /// "EnterPlayerFalling"の終了処理
    /// </summary>
    /// TODO：t.mitsumaru　未実装（今後処理を追加する可能性があるため関数のみを追加）
    void ExitPlayerFalling()
    {

    }
}
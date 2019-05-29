using System.Collections;
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
        CountDown,          // カウントダウン
        ChargeJumpPower,    // ジャンプ力を溜める
        PlayerJump,         // ジャンプ
    }

    [SerializeField] PhaseCountDown       phaseCountDown       = default;       // フェーズ：カウントダウン
    [SerializeField] PhaseChargeJumpPower phaseChargeJumpPower = default;       // フェーズ：ジャンプ力を溜める


    // ステートマシン
    static public StateMachine<PhaseType> StateMachine { get; private set; } = new StateMachine<PhaseType>();

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // フェーズのそれぞれの関数をステートマシンに追加
        StateMachine.Add(PhaseType.CountDown, phaseCountDown.Initializer, phaseCountDown.Updater, phaseCountDown.Cleanup);
        StateMachine.Add(PhaseType.ChargeJumpPower, phaseChargeJumpPower.Initializer, phaseChargeJumpPower.Updater, phaseChargeJumpPower.Cleanup);

        // 最初のフェーズをセット
        StateMachine.SetState(PhaseType.CountDown);
    }

    /// <summary>
    /// 更新
    /// FixedUpdate：モニターのレートによって処理速度が左右されないように
    /// </summary>
    void FixedUpdate()
    {
        // 各フェーズの更新を行う
        StateMachine.Update();
    }
}

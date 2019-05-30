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
        ChargeCountDown,    // ジャンプ力を溜めている際のカウントダウン
        PlayerJump,         // ジャンプ
    }

    [SerializeField] PhaseChargeCountDown phaseChargeJumpPower = default;       // フェーズ：ジャンプ力を溜める
    // ステートマシン
    static public StateMachine<PhaseType> StateMachine { get; private set; } = new StateMachine<PhaseType>();

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // フェーズのそれぞれの関数をステートマシンに追加
        StateMachine.Add(PhaseType.ChargeCountDown, phaseChargeJumpPower.Initializer, phaseChargeJumpPower.Updater, phaseChargeJumpPower.Cleanup);
        // 最初のフェーズをセット
        StateMachine.SetState(PhaseType.ChargeCountDown);
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

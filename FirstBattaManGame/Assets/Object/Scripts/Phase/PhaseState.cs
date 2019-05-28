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
        Result,             // 結果
    }

    [SerializeField] PhaseCountDown phaseCountDown = default;       // フェーズ：カウントダウン


    // ステートマシン
    static public StateMachine<PhaseType> StateMachine { get; private set; } = new StateMachine<PhaseType>();

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        StateMachine.Add(PhaseType.CountDown, phaseCountDown.Initialize, phaseCountDown.Update, phaseCountDown.Cleanup);
        StateMachine.SetState(PhaseType.CountDown);
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        StateMachine.Update();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェーズ：カウントダウン
/// </summary>
public class PhaseCountDown : PhaseBase
{
    [SerializeField] GameObject countDownTimerUi   = default;    // カウントダウンUI
    [SerializeField] Text       countDownTimerText = default;    // カウントダウンUIのテキスト

    const float CountDownNum           = 3.6f;     // カウントダウンの初期値
    const int   PhaseChangeDelayToMsec = 1000;     // フェーズ変更時の遅延時間（ミリ秒）

    /// <summary>
    /// フェーズの初期化
    /// </summary>
    public override void Initializer()
    {
        // カウントの値をセットする
        Counter.SetCount(CountDownNum);
    }

    /// <summary>
    /// フェーズの更新
    /// </summary>
    public override void Updater()
    {
        // 開始カウント数が０以外なら、そのままカウント数を表示
        // （UIに表示されるまでにラグがあるため、0.５以下は０とみなす）
        if (Counter.currentCountNum > 0.5f)
        {
            // カウントダウンの値を表示
            countDownTimerText.text = Counter.currentCountNum.ToString("F0");
            // カウントダウンを行う
            Counter.CountDown();
        }
        // カウント数が０であれば、代わりに"スタート"を表示
        else
        {
            // "スタート"が表示されたら、指定時間分待機してフェーズを変更する
            if (countDownTimerText.text == "スタート")
            {
                PhaseState.StateMachine.SetState(PhaseState.PhaseType.ChargeJumpPower, PhaseChangeDelayToMsec);
            }

            // UIに"スタート"を表示
            countDownTimerText.text = "スタート";
        }
    }

    /// <summary>
    /// フェーズの終了
    /// </summary>
    public override void Cleanup()
    {
        // 開始カウントダウンのUIを非表示にする
        countDownTimerUi.SetActive(false);
    }
}

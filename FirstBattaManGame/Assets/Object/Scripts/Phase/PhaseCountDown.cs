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

          int countDownInterval   = 0;                           // カウントダウンのインターバル
          int currentCountDownNum = CountDownNum;                // 現在のカウント数
    const int CountDownNum        = 3;                           // カウントダウンの数

    /// <summary>
    /// フェーズの初期化
    /// </summary>
    public override void Initialize()
    {
        // 処理なし
    }

    /// <summary>
    /// フェーズの更新
    /// </summary>
    public override void Update()
    {
        // インターバルカウントを回しながら、カウントダウンを行う
        countDownInterval++;
        if (countDownInterval % 60 == 59)
        {
            // カウント数が０になったら、それぞれのUIを入れ替えてフェーズを変更する
            if (currentCountDownNum == 0)
            {
                // フェーズをジャンプ力チャージに変更
                PhaseState.StateMachine.SetState(PhaseState.PhaseType.ChargeJumpPower);
            }
            currentCountDownNum--;
        }

        // 開始カウント数が０以外なら、そのままカウント数を表示
        if (currentCountDownNum != 0)
        {
            countDownTimerText.text = currentCountDownNum.ToString();
        }
        // カウント数が０であれば、代わりに"START"を表示
        else
        {
            countDownTimerText.text = "START";
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェーズ：ジャンプ力を溜める
/// </summary>
public class PhaseChargeJumpPower : PhaseBase
{
    [SerializeField] GameObject countDownUi;        // カウントダウンUI
    [SerializeField] Text       countDownText;      // カウントダウンUIのテキスト

    // 入力を制御するクラス
    InputController inputController = new InputController();

    // カウントダウンの初期値
    const float CountDownNum = 10;

    /// <summary>
    /// フェーズの初期化
    /// </summary>
    public override void Initializer()
    {
        // カウントの値をセットする
        Counter.SetCount(CountDownNum);
        // カウントダウンUIのアクティブフラグを起こす
        countDownUi.SetActive(true);
    }

    /// <summary>
    /// フェーズの更新
    /// </summary>
    public override void Updater()
    {
        // 開始カウント数が０以外なら、そのままカウント数を表示
        if (Counter.CurrentCountNum > 0)
        {
            // タップの回数を記録する
            inputController.TouchCounter();

            // カウントダウンの値を表示
            countDownText.text = Counter.CurrentCountNum.ToString("F1");
            // カウントダウンを行う
            Counter.CountDown();
        }
        // カウント数が０であれば、代わりに"スタート"を表示
        else
        {
            // フェードをプレイヤーのジャンプに変更する
            // （現在は"キー：PlayerJump"の関数は設定していないのでコメントアウトしておく）
            //PhaseState.StateMachine.SetState(PhaseState.PhaseType.PlayerJump);
        }
    }

    /// <summary>
    /// フェーズの終了
    /// </summary>
    public override void Cleanup()
    {
        // カウントダウンのUIを非表示にする
        countDownUi.SetActive(false);
    }
}

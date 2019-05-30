using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

/// <summary>
/// フェーズ：ジャンプ力を溜める
/// </summary>
public class PhaseChargeCountDown : MonoBehaviour
{
    [SerializeField] GameObject countDownUi   = default;      // カウントダウンUI
    [SerializeField] Text       countDownText = default;     // カウントダウンUIのテキスト

    float currentCountNum = CountDownNum;
    const float CountDownNum = 10;       // カウントダウンの初期値

    /// <summary>
    /// フェーズの初期化
    /// </summary>
    public void Initializer()
    {
        // カウントダウンUIのアクティブフラグを起こす
        countDownUi.SetActive(true);
    }

    /// <summary>
    /// フェーズの更新
    /// </summary>
    public void Updater()
    {
        // 開始カウント数が０以外なら、そのままカウント数を表示
        if (currentCountNum > 0)
        {
            // タップの回数を記録する
            InputController.CountTouch();

            // 最初のタッチが行われたらカウントダウンを開始する
            if (InputController.IsFirstTouch) 
            {
                // カウントダウンを行う
                currentCountNum -= Time.deltaTime;
                // カウントダウンの値を表示
                countDownText.text = currentCountNum.ToString("F1");
            }
        }
    }

    /// <summary>
    /// フェーズの終了
    /// </summary>
    public void Cleanup()
    {
        // カウントダウンのUIを非表示にする
        countDownUi.SetActive(false);
    }
}

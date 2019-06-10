using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

/// <summary>
/// ジャンプ力の溜めている際のカウントダウン
/// </summary>
public class ChargeCountDown : MonoBehaviour
{
    // カウントダウンUIのテキスト
    [SerializeField] Text countDownText = default;

    // 現在のカウント数
    public float CurrentCountNum { get; private set; } = CountDownNum;
    // カウントダウンの初期値
    const float CountDownNum = 10;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void OnEnable()
    {
        // カウントダウンのUIを表示する
        GeneralFuncion.SetActiveFromAllChild(transform, true);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 開始カウント数が０以外なら、そのままカウント数を表示
        if (CurrentCountNum > 0)
        {
            // タップの回数を記録する
            InputController.CountTouch();

            // 最初のタッチが行われたらカウントダウンを開始する
            if (InputController.IsFirstTouch) 
            {
                // カウントダウンを行う
                CurrentCountNum -= Time.deltaTime;
                // カウントダウンの値を表示
                countDownText.text = CurrentCountNum.ToString("F1");
            }
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        // カウントダウンのUIを非表示にする
        GeneralFuncion.SetActiveFromAllChild(transform, false);
    }
}

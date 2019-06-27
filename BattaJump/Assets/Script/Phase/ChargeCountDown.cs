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

    // ジャンプボイスを再生させる残りカウント数
    const float JumpVoiceRemainCountNum = 1;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void OnEnable()
    {
        // カウントダウンのUIを表示する
        GeneralFuncion.SetActiveFromAllChild(transform, true);

        // ジャンプ力のチャージ音を再生
        AudioPlayer.instance.PlaySe(AudioPlayer.SeType.JumpChargeing);

        // 指定の残りカウント数まで待機したあとにジャンプボイスを再生させる
        // NOTE : 待機時間 = 最大カウント数 - 指定の残りカウント数
        Invoke("PlayJumpVoice", CountDownNum - JumpVoiceRemainCountNum);
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

            // カウントダウンを行う
            CurrentCountNum -= Time.deltaTime;
            // カウントダウンの値を表示
            countDownText.text = CurrentCountNum.ToString("F1");

        }
    }

    /// <summary>
    /// ジャンプボイスを再生させる
    /// </summary>
    void PlayJumpVoice()
    {
        AudioPlayer.instance.PlaySe(AudioPlayer.SeType.JumpVoice);
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        // カウントダウンのUIを非表示にする
        GeneralFuncion.SetActiveFromAllChild(transform, false);

        // チャージ音を停止する
        AudioPlayer.instance.StopSe(AudioPlayer.SeType.JumpChargeing);
        // 鳥のさえずりを停止する
        AudioPlayer.instance.StopSe(AudioPlayer.SeType.BirdTwitter);
    }
}

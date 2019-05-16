using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// シーンのフェーズを管理するクラス
/// </summary>
public class ScenePhase : MonoBehaviour
{
    /// <summary>
    /// シーンの状態
    /// </summary>
    enum Phase
    {
        CountDown,      // カウントダウン
        Playerjump,     // プレイヤー（バッタマン）のジャンプ
        Result,         // リザルト
    }

    Phase currentPhase;                          // シーンの現在のフェーズ

    [SerializeField] GameObject player;          // プレイヤー
    [SerializeField] GameObject countTimerUi;    // カウントタイマーのUI
    [SerializeField] GameObject jumpHeightUi;    // ジャンプの高さのUI

    Text countTimerText;                         // カウントタイマーUIのテキスト
    Text jumpHeightText;                         // ジャンプの高さのUIのテキスト
    
    float currentTime;                          // 現在のタイマーの時間
    float LimitTime = 10.0f;                    // 制限時間

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        currentPhase = Phase.CountDown;                         // シーンのフェーズを"カウントダウン"に初期化

        countTimerText = countTimerUi.GetComponent<Text>();     // カウントタイマーのテキストのコンポーネントを取得
        jumpHeightText = jumpHeightUi.GetComponent<Text>();     // ジャンプの高さのテキストのコンポーネントを取得

        currentTime = LimitTime;                                 // タイマーの制限時間をセット

        // それぞれのアクティブフラグを初期化
        player.SetActive(true);                 // プレイヤー
        countTimerUi.SetActive(true);           // カウントタイマーのUI
        jumpHeightUi.SetActive(false);          // ジャンプの高さのUI
    }

    /// <summary>
    /// 更新
    /// </summary>
    void FixedUpdate()
    {
        // それぞれのフェーズごとに関数を分けて処理を行う
        switch (currentPhase)
        {
            // カウントダウン
            case Phase.CountDown:
                PhaseCountDown();
                break;

            // プレイヤー（バッタマン）のジャンプ
            case Phase.Playerjump:
                PhasePlayerJump();
                break;

            // リザルト
            case Phase.Result:
                PhaseResult();
                break;

            // デフォルト
            default:
                break;
        }
    }

    /// <summary>
    /// フェーズ内の処理：カウントダウン
    /// </summary>
    void PhaseCountDown()
    {
        if (currentTime > 0)
        {
            // タイマーを減らしていく
            currentTime -= Time.deltaTime;
        }
        else
        {
            // アルファを変更して、タイマーのテキストをフェードアウトさせる
            Color timerTextColor = countTimerText.color;
            timerTextColor.a -= 0.1f;
            countTimerText.color = timerTextColor;

            // フェードアウトが終了したら、アクティブフラグをfalseにする
            if (timerTextColor.a <= 0)
            {
                countTimerUi.SetActive(false);
            }
        }

        // タイマーの値をテキストにセット
        countTimerText.text = currentTime.ToString("f1");
    }

    /// <summary>
    /// フェーズ内の処理：プレイヤー（バッタマン）のジャンプ
    /// </summary>
    void PhasePlayerJump()
    {

    }

    /// <summary>
    /// フェーズ内の処理：リザルト
    /// </summary>
    void PhaseResult()
    {

    }
}

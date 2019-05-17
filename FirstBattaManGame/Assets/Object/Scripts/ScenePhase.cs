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

    Phase currentPhase;                                // シーンの現在のフェーズ

    [SerializeField] GameObject player;                // プレイヤー
    [SerializeField] GameObject ground;                // 地面
    [SerializeField] GameObject countTimerUi;          // カウントタイマーのUI
    [SerializeField] GameObject jumpHeightUi;          // ジャンプの高さのUI

    Rigidbody playerRigidbody;                         // プレイヤーのリジッドボディ
    Text      countTimerText;                          // カウントタイマーUIのテキスト
    Text      jumpHeightText;                          // ジャンプの高さのUIのテキスト
    
    float currentTime;                                 // 現在のタイマー時間
    float currentJumpPower;                            // 現在蓄積されているジャンプ力
    float currentJumpHeightToMetre;                    // ジャンプの高さ（メートル）
    bool  isJump;                                      // ジャンプしたかどうか

    [SerializeField] float LimitTime;                  // 制限時間
    [SerializeField] float OneTouchJumpPower;          // ワンタッチで蓄積されるジャンプ力
    [SerializeField] float OneMetreDistance;           // 1メール分の距離
    [SerializeField] uint  TimeScaleToPlayerJump;      // プレイヤーがジャンプしている際のタイムスケール
    [SerializeField] float uiFadeAttenuation;          // UIのフェード時の減衰値

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        currentPhase = Phase.CountDown;                         // シーンのフェーズを"カウントダウン"に初期化

        countTimerText  = countTimerUi.GetComponent<Text>();     // カウントタイマーのテキストのコンポーネントを取得
        jumpHeightText  = jumpHeightUi.GetComponent<Text>();     // ジャンプの高さのテキストのコンポーネントを取得
        playerRigidbody = player.GetComponent<Rigidbody>();      // プレイヤーのリジッドボディのコンポーネントを取得

        currentTime              = LimitTime;                    // タイマーの制限時間をセット
        currentJumpPower         = 0;                            // 現在蓄積されているジャンプ力
        currentJumpHeightToMetre = 0;                            // ジャンプの高さ（メートル）
        isJump                   = false;                        // ジャンプしたかどうか

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
        // タイマーが０になるまでの処理
        if (currentTime > 0)
        {
            // 
            if (Input.touchCount > 0)
            {
                // タッチの状態を取得
                Touch touchInfo = Input.GetTouch(0);
                // タッチされたら、ジャンプ力を溜める
                if (touchInfo.phase == TouchPhase.Began)
                {
                    currentJumpPower += OneTouchJumpPower;
                }
            }

            // タイマーを減らしていく
            currentTime -= Time.deltaTime;
        }
        // タイマーが０になった場合の処理
        else
        {
            // アルファを変更して、タイマーのテキストをフェードアウトさせる
            Color timerTextColor = countTimerText.color;
            timerTextColor.a -= uiFadeAttenuation;
            countTimerText.color = timerTextColor;

            // フェードアウトが終了
            if (timerTextColor.a <= 0)
            {
                // タイマーのUIのアクティブフラグをfalseに変更する
                countTimerUi.SetActive(false);
                // フェーズを"PlayerJump"に変更する
                currentPhase = Phase.Playerjump;

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
        // プレイヤー（バッタマン）をジャンプさせる
        if (!isJump)
        {
            // プレイヤーに蓄積されたジャンプ力を与える
            playerRigidbody.AddForce(Vector3.up * currentJumpPower, ForceMode.VelocityChange);
            // ジャンプフラグをtrueに変更する
            isJump = true;

            // ジャンプの高さのUIのアクティブフラグをtrueに変更する
            jumpHeightUi.SetActive(true);

            // タイムスケールを変更する
            Time.timeScale = TimeScaleToPlayerJump;
        }

        // 地面からのプレイヤーの高さを算出
        float currentJumpHeight = (player.transform.position - ground.transform.position).magnitude;
        // 現在のジャンプ力をメートルに変換する
        currentJumpHeightToMetre = currentJumpHeight / OneMetreDistance;

        // ジャンプの高さを表すUIに反映させる
        jumpHeightText.text = currentJumpHeightToMetre.ToString("f1") + "m";
    }

    /// <summary>
    /// フェーズ内の処理：リザルト
    /// </summary>
    void PhaseResult()
    {

    }
}

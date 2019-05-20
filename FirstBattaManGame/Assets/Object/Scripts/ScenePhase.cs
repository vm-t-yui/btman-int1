using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [SerializeField] GameObject player;                // プレイヤー
    [SerializeField] GameObject ground;                // 地面
    [SerializeField] GameObject countTimerUi;          // カウントタイマーのUI
    [SerializeField] GameObject jumpHeightUi;          // ジャンプの高さのUI
    [SerializeField] GameObject backToTitleButton;     // タイトルへ戻るボタン

    Phase     currentPhase             = Phase.CountDown;     // シーンの現在のフェーズ
    float     currentTime              = LimitTime;           // 現在のタイマー時間
    float     currentJumpPower         = 0;                   // 現在蓄積されているジャンプ力
    float     currentJumpHeightToMetre = 0;                   // ジャンプの高さ（メートル）
    bool      isJump                   = false;               // ジャンプしたかどうか

    Rigidbody playerRigidbody;                                // プレイヤーのリジッドボディ
    Text      countTimerText;                                 // カウントタイマーUIのテキスト
    Text      jumpHeightText;                                 // ジャンプの高さのUIのテキスト

    const float LimitTime             = 10;            // 制限時間
    const float OneTouchJumpPower     = 5;             // ワンタッチで蓄積されるジャンプ力
    const float OneMetreDistance      = 100;           // 1メール分の距離
    const uint  TimeScaleToPlayerJump = 5;             // プレイヤーがジャンプしている際のタイムスケール
    const float UiFadeAttenuation     = 0.1f;          // UIのフェード時の減衰値
    const float ResultTextScale       = 1.5f;          // リザルトでのテキストのスケール
    const float ResultLerpRate        = 0.02f;         // リザルトでのラープの割合

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        countTimerText  = countTimerUi.GetComponent<Text>();     // カウントタイマーのテキストのコンポーネントを取得
        jumpHeightText  = jumpHeightUi.GetComponent<Text>();     // ジャンプの高さのテキストのコンポーネントを取得
        playerRigidbody = player.GetComponent<Rigidbody>();      // プレイヤーのリジッドボディのコンポーネントを取得

        // それぞれのアクティブフラグを初期化
        player.SetActive(true);                 // プレイヤー
        countTimerUi.SetActive(true);           // カウントタイマーのUI
        jumpHeightUi.SetActive(false);          // ジャンプの高さのUI
        backToTitleButton.SetActive(false);     // タイトルへ戻るボタン
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
            timerTextColor.a -= UiFadeAttenuation;
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
    /// TODO: t.mitsumaru 未実装
    void PhaseResult()
    {
      
    }

    /// <summary>
    /// 指定の時間だけ待機した後に、フェーズを変更するコルーチン
    /// </summary>
    /// <param name="phaseWaitSeconds">フェーズを変更するまでの待機時間</param>
    /// <param name="changePhase">変更するフェーズ</param>
    /// <returns>IEnumeratorを返す</returns>
    IEnumerator ChangePhase(float phaseWaitSeconds,Phase changePhase)
    {
        // 指定の時間だけ待機する
        yield return new WaitForSeconds(phaseWaitSeconds);
        // フェーズを変更する
        currentPhase = changePhase;
    }

}

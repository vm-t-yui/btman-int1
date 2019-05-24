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
        StartCountDown,     // 開始時のカウントダウン
        CountDown,          // カウントダウン
        Playerjump,         // プレイヤー（バッタマン）のジャンプ
        Result,             // リザルト
    }

    [SerializeField] PlayerJumpController playerJumpController;  // プレイヤーのジャンプ制御クラス
    [SerializeField] GameObject           ground;                // 地面
    [SerializeField] GameObject           startCountDownUi;      // 開始時のカウントダウン
    [SerializeField] GameObject           countTimerUi;          // カウントタイマーのUI
    [SerializeField] GameObject           jumpHeightUi;          // ジャンプの高さのUI
    [SerializeField] GameObject           backToTitleButton;     // タイトルへ戻るボタン

    [SerializeField] Rigidbody            playerRigidbody;       // プレイヤーのリジッドボディ
    [SerializeField] Text                 startCountDownText;    // 開始時のカウントダウンのテキスト
    [SerializeField] Text                 countTimerText;        // カウントタイマーUIのテキスト
    [SerializeField] Text                 jumpHeightText;        // ジャンプの高さのUIのテキスト

    public float CurrentJumpPower             { get; private set; } = 0;    // 現在蓄積されているジャンプ力
    public float CurrentJumpHeight            { get; private set; } = 0;    // 現在のジャンプ高さ
    public float CurrentJumpHeightToKilometer { get; private set; } = 0;    // 現在のジャンプ高さ（キロメートル）

    Phase     currentPhase       = Phase.StartCountDown;                    // シーンの現在のフェーズ
    int       currentStartCount  = StartCountNum;                           // 現在の開始カウント数
    int       startCountInterval = 0;                                       // 開始カウント時のインターバル 
    float     currentTime        = LimitTime;                               // 現在のタイマー時間

    const float OneTouchJumpPower     = 5;             // ワンタッチで蓄積されるジャンプ力
    const int   StartCountNum         = 3;             // 開始時のカウント数
    const float LimitTime             = 10;            // 制限時間
    const float CameraZoomInSpeed     = 0.005f;        // カメラのズームインのスピード
    const float CameraZoomOutLerpRate = 0.05f;         // Lerpのズームアウト時のLerpの割合
    const float CameraLerpMoveAmount  = 15.0f;         // Lerpでのカメラの移動量
    const uint  TimeScaleToPlayerJump = 5;             // プレイヤーがジャンプしている際のタイムスケール
    const float UiFadeAttenuation     = 0.1f;          // UIのフェード時の減衰値
    const float ResultChangeWait      = 1.0f;          // フェーズをリザルトに変更する際の待機時間
    const float ResultTextScale       = 1.5f;          // リザルトでのテキストのスケール
    const float ResultLerpRate        = 0.05f;         // リザルトでのLerpの割合

    /// <summary>
    /// 更新
    /// （Updateだとモニターのリフレッシュレートによって処理速度が変わるため）
    /// </summary>
    void FixedUpdate()
    {
        // それぞれのフェーズごとに関数を分けて処理を行う
        switch (currentPhase)
        {
            // 開始時のカウントダウン
            case Phase.StartCountDown:
                PhaseStartCountDown();
                break;

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
    /// ゲーム開始時のカウントダウン
    /// </summary>
    void PhaseStartCountDown()
    {
        // インターバルカウントを回しながら、カウントダウンを行う
        // カウントダウンの間隔は１秒(60フレーム)
        startCountInterval++;
        if (startCountInterval % 60 == 59)
        {
            // カウント数が０になったら、それぞれのUIを入れ替えてフェーズを変更する
            if (currentStartCount == 0)
            {
                currentPhase = Phase.CountDown;     // フェーズをカウントダウンに変更
                countTimerUi.SetActive(true);       // カウントダウンのUIを表示する
                startCountDownUi.SetActive(false);  // 開始カウントダウンのUIを非表示にする
            }
            currentStartCount--;
        }

        // 開始カウント数が０以外なら、そのままカウント数を表示
        if (currentStartCount != 0)
        {
            startCountDownText.text = currentStartCount.ToString();
        }
        // カウント数が０であれば、代わりに"START"を表示
        else
        {
            startCountDownText.text = "START";
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
            if (Input.touchCount > 0)
            {
                // タッチの状態を取得
                Touch touchInfo = Input.GetTouch(0);
                // タッチされたら、ジャンプ力を溜める
                if (touchInfo.phase == TouchPhase.Began)
                {
                    CurrentJumpPower += OneTouchJumpPower;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CurrentJumpPower += OneTouchJumpPower;
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

        // カメラのトランスフォームを取得
        Transform cameraTrans = Camera.main.transform;
        // カメラのズームさせる
        cameraTrans.Translate(new Vector3(0, 0, CameraZoomInSpeed));
    }

    /// <summary>
    /// フェーズ内の処理：プレイヤー（バッタマン）のジャンプ
    /// </summary>
    void PhasePlayerJump()
    {
        // プレイヤー（バッタマン）をジャンプさせる
        if (!playerJumpController.IsJump)
        {
            // プレイヤーをジャンプさせる
            playerJumpController.StartJump(CurrentJumpPower);

            // ジャンプの高さのUIのアクティブフラグをtrueに変更する
            jumpHeightUi.SetActive(true);

            // タイムスケールを変更する
            Time.timeScale = TimeScaleToPlayerJump;
        }
        // ジャンプした際の処理
        else
        {
            // 地面からのプレイヤーの高さを算出
            CurrentJumpHeight = (playerJumpController.transform.position - ground.transform.position).magnitude;
            // ジャンプ高さをキロメートルに変換
            JumpHeightToKiloMetre();

            // ベロシティが下向きになったら
            if (playerJumpController.PlayerRigidbody.velocity.y <= 0)
            {
                // タイムスケールをもとに戻す
                Time.timeScale = 1;

                // プレイヤーの重力を停止する
                playerJumpController.PlayerRigidbody.useGravity = false;
                // 指定の時間だけ待機して、フェーズをリザルトに変更する
                StartCoroutine(ChangePhase(ResultChangeWait, Phase.Result));
            }
        }

        // ジャンプの高さを表すUIに反映させる
        jumpHeightText.text = CurrentJumpHeightToKilometer.ToString("f1") + "km" ;

        // カメラのトランスフォームを取得
        Transform cameraTrans = Camera.main.transform;
        // Lerpを使用して、ジャンプの瞬間にカメラを引く
        cameraTrans.position = Vector3.Lerp(cameraTrans.position, new Vector3(cameraTrans.position.x, cameraTrans.position.y, -CameraLerpMoveAmount), CameraZoomOutLerpRate);
    }

    /// <summary>
    /// フェーズ内の処理：リザルト
    /// </summary>
    void PhaseResult()
    {
        // テキストをLerpで拡大させる
        jumpHeightUi.transform.localScale = Vector3.Lerp(jumpHeightUi.transform.localScale, new Vector3(ResultTextScale, ResultTextScale, ResultTextScale), ResultLerpRate);
        // テキストをLerpで画面中央に移動させる
        jumpHeightUi.transform.localPosition = Vector3.Lerp(jumpHeightUi.transform.localPosition, Vector3.zero, ResultLerpRate);

        // テキストが画面中央に移動したら
        // （0.1未満になった時点で中心に移動したとみなす）
        if (jumpHeightUi.transform.localPosition.magnitude < 0.1f)
        {
            // タイトルへ戻るボタンを表示
            backToTitleButton.SetActive(true);
        }
    }

    /// <summary>
    /// ジャンプ高さをキロメートルに変換する
    /// </summary>
    void JumpHeightToKiloMetre()
    {
        if (CurrentJumpHeight != 0)
        {
            // 現在のジャンプ力をキロメートルに変換
            CurrentJumpHeightToKilometer = CurrentJumpHeight / GameCommonParameter.OneKiloMetreDistance;
        }
    }

    /// <summary>
    /// タイトルへ戻るボタンを押した際のコールバック
    /// </summary>
    public void OnTouchBackToTitleButton()
    {
        // シーンをタイトルへ変更する
        SceneManager.LoadScene("Title");
        // 終了処理
        Finalize();
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

    /// <summary>
    /// 終了処理
    /// </summary>
    void Finalize()
    {
        // 各GameObjectを削除
        Destroy(startCountDownUi);
        Destroy(ground);
        Destroy(countTimerUi);
        Destroy(jumpHeightUi);
        Destroy(backToTitleButton);

        // 各コンポーネントを削除
        Destroy(startCountDownText);
        Destroy(playerRigidbody);
        Destroy(countTimerText);
        Destroy(jumpHeightText);
    }
}

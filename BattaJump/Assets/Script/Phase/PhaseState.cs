using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フェーズの状態を管理するクラス
/// </summary>
public class PhaseState : MonoBehaviour
{
    /// <summary>
    /// フェーズの種類
    /// </summary>
    public enum PhaseType
    {
        WaitFadeOut,           // フェードアウト待機
        ChargeCountDown,       // ジャンプ力を溜めている際のカウントダウン
        JumpHeightCounter,     // プレイヤーのジャンプ高さを計測するクラス
        PlayerFalling,         // プレイヤーの落下演出
        NextScene,             // 次のシーンへ
        SceneEnd,              // シーンの終了
    }

    // ステートマシン
    StateMachine<PhaseType> stateMachine = new StateMachine<PhaseType>();

    [SerializeField] DisplayFadeContoller fadeContoller  = default;         // フェード管理クラス
    [SerializeField] ChargeCountDown   chargeCountDown   = default;         // ジャンプ力の溜めている際のカウントダウン
    [SerializeField] JumpHeightCounter jumpHeightCounter = default;         // プレイヤーのジャンプ高さを計測するクラス
    [SerializeField] PlayerFalling     playerFalling     = default;         // プレイヤーの落下演出
    [SerializeField] NextSceneChanger  nextScene         = default;         // シーン移行クラス

    [SerializeField] PlayDataManager   playData          = default;         // プレイデータクラス
    [SerializeField] InputController   inputController   = default;         // 入力制御クラス
    [SerializeField] CraterCreater     craterCreater     = default;         // ジャンプ時クレーター生成クラス

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // "WaitFadeOut"の関数をステートマシンに追加
        stateMachine.Add(PhaseType.WaitFadeOut, EnterWaitFadeOut, UpdateWaitFadeOut);
        // "ChargeCountDown"の関数をステートマシンに追加
        stateMachine.Add(PhaseType.ChargeCountDown, EnterChargeCountDown, UpdateChargeCountDown, ExitChargeCountDown);
        // "JumpHeightCounter"の関数をステートマシンに追加
        stateMachine.Add(PhaseType.JumpHeightCounter, EnterJumpHeightCounter, UpdateJumpHeightCounter, ExitJumpHeightCounter);
        // "PlayerFalling"の関数をステートマシンに追加
        stateMachine.Add(PhaseType.PlayerFalling, EnterPlayerFalling, UpdatePlayerFalling, ExitPlayerFalling);
        // "NextScene"の関数をステートマシンに追加
        stateMachine.Add(PhaseType.NextScene, EnterNextScene, UpdateNextScene);
        // "SceneEnd"をステートマシンに追加
        stateMachine.Add(PhaseType.SceneEnd);
        // 最初のステートを"WaitFadeOut"にセット
        stateMachine.SetState(PhaseType.WaitFadeOut);
    }

    /// <summary>
    /// 更新
    /// FixedUpdate：モニターのレートによって処理速度が左右されないように
    /// </summary>
    void FixedUpdate()
    {
        // 各フェーズの更新を行う
        stateMachine.Update();
    }

    /// <summary>
    /// "WaitFadeOut"の初期化処理
    /// </summary>
    void EnterWaitFadeOut()
    {
        // フェードアウト開始
        fadeContoller.OnFade(DisplayFadeContoller.FadeType.FadeOut, DisplayFadeContoller.PanelType.AdView);
    }

    /// <summary>
    /// "WaitFadeOut"の更新処理
    /// </summary>
    void UpdateWaitFadeOut()
    {
        // フェードアウトが終わったら
        if (fadeContoller.IsFadeEnd)
        {
            // ステートを"ChargeCountDown"に変更する
            stateMachine.SetState(PhaseType.ChargeCountDown);
        }
    }

    /// <summary>
    /// "ChargeCountDown"の初期化処理
    /// </summary>
    void EnterChargeCountDown()
    {
        // "ChargeCountDown"をtrueに設定
        chargeCountDown.enabled = true;

        // 入力制御クラスをオン
        inputController.enabled = true;
    }

    /// <summary>
    /// "ChargeCountDown"の更新処理
    /// </summary>
    void UpdateChargeCountDown()
    {
        // カウントダウンの値が０になったら
        if (chargeCountDown.CurrentCountNum <= 0.1f)
        {
            // ステートを"JumpHeightCounter"に変更する
            stateMachine.SetState(PhaseType.JumpHeightCounter);
        }
    }

    /// <summary>
    /// "ChargeCountDown"の終了処理
    /// </summary>
    void ExitChargeCountDown()
    {
        // "ChargeCountDown"をfalseに設定
        chargeCountDown.enabled = false;

        // 入力制御クラスをオフ
        inputController.enabled = false;
    }

    /// <summary>
    /// "JumpHeightCounter"の初期化処理
    /// </summary>
    void EnterJumpHeightCounter()
    {
        // "JumpHeightCounter"をtrueに設定
        jumpHeightCounter.enabled = true;

        // ジャンプした瞬間にクレーターを生成
        craterCreater.Create();
    }

    /// <summary>
    /// "JumpHeightCounter"の更新処理
    /// </summary>
    void UpdateJumpHeightCounter()
    {
        // ジャンプ高さの結果が出たら
        if (jumpHeightCounter.IsJumpHeightResult)
        {
            // スコアをデータにセット
            playData.SetNowScore(jumpHeightCounter.JumpHeightToKiloMetre);

            // ステートを"PlayerFalling"に変更する
            stateMachine.SetState(PhaseType.PlayerFalling);
        }
    }

    /// <summary>
    /// "JumpHeightCounter"の終了処理
    /// </summary>
    void ExitJumpHeightCounter()
    {
        // "JumpHeightCounter"をfalseに設定
        jumpHeightCounter.enabled = false;
    }

    /// <summary>
    /// "EnterPlayerFalling"の初期化処理
    /// </summary>
    void EnterPlayerFalling()
    {
        // "PlayerFalling"をtrueに設定
        playerFalling.enabled = true;

        //重力を落下用重力に設定
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    /// <summary>
    /// "EnterPlayerFalling"の更新処理
    /// </summary>
    void UpdatePlayerFalling()
    {
        // 落下処理が終わったら
        if (playerFalling.IsEnd)
        {
            // ステートを"NextScene"に変更する
            stateMachine.SetState(PhaseType.NextScene);
        }
    }

    /// <summary>
    /// "EnterPlayerFalling"の終了処理
    /// </summary>
    /// TODO：t.mitsumaru　未実装（今後処理を追加する可能性があるため関数のみを追加）
    void ExitPlayerFalling()
    {

    }

    /// <summary>
    /// "NextScene"の初期化処理
    /// </summary>
    void EnterNextScene()
    {
        // フェードイン開始
        fadeContoller.OnFade(DisplayFadeContoller.FadeType.FadeIn, DisplayFadeContoller.PanelType.White);
    }

    /// <summary>
    /// "NextScene"の更新処理
    /// </summary>
    void UpdateNextScene()
    {
        // フェードアウトが終わったら
        if (fadeContoller.IsFadeEnd)
        {
            //重力をデフォルトに設定
            Physics.gravity = new Vector3(0, -50f, 0);

            // 次に読むシーンをリザルトに設定
            nextScene.SetNextSceneResult();

            // シーンを移行
            nextScene.ChangeNextScene();

            // ステートを"SceneEnd"に変更する
            stateMachine.SetState(PhaseType.SceneEnd);
        }
    }
}

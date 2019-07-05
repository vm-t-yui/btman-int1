using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 状態に応じてBGMの変更を行う
/// </summary>
public class BgmPlayState : MonoBehaviour
{
    /// <summary>
    /// BGMを再生する状態
    /// </summary>
    enum PlayState
    {
        ActiveSceneToTitle,     // シーン：タイトル
        JumpChargeStay,         // ジャンプ溜め待ち
        JumpChargeing,          // ジャンプ溜め
        PlayerJumping,          // プレイヤーがジャンプ中
        ActiveSceneToResult,    // シーン：リザルト
    }

    // ジャンプのカウントダウンクラス
    public ChargeCountDown chargeCountDown { get; set; } = default;

    // シングルトンインスタンス
    static public BgmPlayState instance = null;

    // ステートマシン
    StateMachine<PlayState> stateMachine = new StateMachine<PlayState>();

    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        if (null == instance)
        {
            // 自分自身をインスタンスとして渡す
            instance = this;
            // シーンが切り替わってもインスタンスが破棄されないように設定
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 各ステートの関数をセット
        stateMachine.Add(PlayState.ActiveSceneToTitle, EnterActiveSceneToTitle,UpdateActiveSceneToTitle);
        stateMachine.Add(PlayState.JumpChargeStay, null, UpdateJumpChargeStay);
        stateMachine.Add(PlayState.JumpChargeing, null, UpdateJumpChargeing);
        stateMachine.Add(PlayState.PlayerJumping, EnterPlayerJumping, UpdatePlayerJumping);
        stateMachine.Add(PlayState.ActiveSceneToResult, EnterActiveSceneToResult,UpdateActiveSceneToResult);

        // 最初のステートを設定
        stateMachine.SetState(PlayState.ActiveSceneToTitle);
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
    /// タイトルシーンの開始
    /// </summary>
    void EnterActiveSceneToTitle()
    {
        // タイトルBGMを再生
        AudioPlayer.instance.PlayBgm(AudioPlayer.BgmType.Title);
    }

    /// <summary>
    /// タイトルシーンの更新
    /// </summary>
    void UpdateActiveSceneToTitle()
    {
        // シーンがメインに切り替わったら
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            // BGMを止める
            AudioPlayer.instance.StopBgm();

            // ステートをジャンプ溜め待ちに変更する
            stateMachine.SetState(PlayState.JumpChargeStay);
        }
    }

    /// <summary>
    /// ジャンプ溜め待ちの更新
    /// </summary>
    void UpdateJumpChargeStay()
    {
        // ジャンプの溜めを開始する入力があったら
        if (InputController.IsFirstTouch)
        {
            // ステートをジャンプ溜めに変更する
            stateMachine.SetState(PlayState.JumpChargeing);
        }
    }

    /// <summary>
    /// ジャンプ溜めの更新
    /// </summary>
    void UpdateJumpChargeing()
    {
        // ジャンプ溜めが終了して、ジャンプを開始したら
        if (chargeCountDown.CurrentCountNum <= 0.1f)
        {
            // ステートをジャンプに切り替える
            stateMachine.SetState(PlayState.PlayerJumping);
        }
    }

    /// <summary>
    /// プレイヤージャンプの開始
    /// </summary>
    void EnterPlayerJumping()
    {
        // プレイヤージャンプ中のBGMを再生
        AudioPlayer.instance.PlayBgm(AudioPlayer.BgmType.Jumping);
    }

    /// <summary>
    /// プレイヤージャンプ中の更新
    /// </summary>
    void UpdatePlayerJumping()
    {
        // シーンがリザルトに変わったら
        if (SceneManager.GetActiveScene().name == "Result")
        {
            // ステートをリザルトに変更する
            stateMachine.SetState(PlayState.ActiveSceneToResult);
        }
    }

    /// <summary>
    /// リザルトシーン開始
    /// </summary>
    void EnterActiveSceneToResult()
    {
        // リザルトのBGMを再生
        AudioPlayer.instance.PlayBgm(AudioPlayer.BgmType.Result);
    }

    /// <summary>
    /// リザルトシーン更新
    /// </summary>
    void UpdateActiveSceneToResult()
    {
        // リザルトからの移動先がメインであれば
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            // BGMを停止する
            AudioPlayer.instance.StopBgm();
            // ステートをジャンプ溜め待ちに変更する
            stateMachine.SetState(PlayState.JumpChargeStay);
        }
        else if (SceneManager.GetActiveScene().name == "Title")
        {
            // BGMを停止する
            AudioPlayer.instance.StopBgm();
            // ステートをタイトルシーンに変更する
            stateMachine.SetState(PlayState.ActiveSceneToTitle);
        }
    }
}

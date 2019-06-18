using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインゲーム時プレイヤーアニメーター管理クラス
/// </summary>
public class MainPlayerAnimator : MonoBehaviour
{
    /// <summary>
    /// アニメーションの種類
    /// </summary>
    enum AnimationType
    {
        Charge,
        Jump,
        Fall
    }

    [SerializeField]
    Animator animator;                                // アニメーター
    [SerializeField]
    ChargeCountDown chargeCountDown = default;        // チャージ中カウントダウン
    [SerializeField]
    JumpHeightCounter jumpHeightCounter = default;    // ジャンプの高さ計測

    [SerializeField]
    PlayerFalling playerFalling = default;            // プレイヤー落下
    [SerializeField]
    GameObject chargeParticlre = default;             // チャージのパーティクル
    [SerializeField]
    GameObject JumpImpactParticle = default;          // ジャンプの瞬間のパーティクル
    [SerializeField]
    GameObject JumpNowParticle = default;             // ジャンプ中のパーティクル

    AnimationType nextAnim = AnimationType.Charge;    // 次に再生するアニメーション

    bool isEnd = false;                               // 処理終了フラグ

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 処理終了フラグがtrueなら処理を抜ける
        if (isEnd) { return; }

        switch (nextAnim)
        {
            case AnimationType.Charge:
                // 初めてタップされたら
                if (InputController.IsFirstTouch)
                {
                    // チャージアニメーション再生
                    animator.SetTrigger("Charge");

                    // チャージのパーティクルをオン
                    chargeParticlre.SetActive(true);

                    nextAnim = AnimationType.Jump;
                }
                break;

            case AnimationType.Jump:
                // カウントダウンの値が０になったら
                if (chargeCountDown.CurrentCountNum <= 0.1f)
                {
                    // ジャンプアニメーション再生
                    animator.SetTrigger("Jump");

                    // チャージのパーティクルをオフ
                    chargeParticlre.SetActive(false);

                    // ジャンプの瞬間のパーティクルをオン
                    JumpImpactParticle.SetActive(true);

                    // ジャンプ中のパーティクルをオン
                    JumpNowParticle.SetActive(true);

                    nextAnim = AnimationType.Fall;
                }
                break;

            case AnimationType.Fall:
                // ジャンプ高さの結果が出たら
                if (jumpHeightCounter.IsJumpHeightResult)
                {
                    // 落下アニメーション再生
                    animator.SetTrigger("Fall");

                    // ジャンプ中のパーティクルをオフ
                    JumpNowParticle.SetActive(false);

                    isEnd = true;
                }
                break;
        }
    }
}

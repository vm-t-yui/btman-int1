using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;                                // アニメーター

    [SerializeField]
    ChargeCountDown chargeCountDown = default;        // チャージ中カウントダウン

    [SerializeField]
    JumpHeightCounter jumpHeightCounter = default;    // ジャンプの高さ計測

    [SerializeField]
    PlayerFalling playerFalling = default;            // プレイヤー落下

    [SerializeField]
    GameObject chargeParticlre = default;             // チャージ中のパーティクル

    [SerializeField]
    GameObject JumpImpactParticle = default;          // ジャンプの瞬間のパーティクル

    [SerializeField]
    GameObject JumpNowParticle = default;             // ジャンプ中のパーティクル

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 初めてタップされたら
        if (InputController.IsFirstTouch)
        {
            // チャージアニメーション再生
            animator.SetTrigger("Charge");

            chargeParticlre.SetActive(true);
        }

        // カウントダウンの値が０になったら
        if (chargeCountDown.CurrentCountNum <= 0.1f)
        {
            // ジャンプアニメーション再生
            animator.SetTrigger("Jump");

            chargeParticlre.SetActive(false);

            JumpImpactParticle.SetActive(true);

            JumpNowParticle.SetActive(true);
        }

        // ジャンプ高さの結果が出たら
        if (jumpHeightCounter.IsJumpHeightResult)
        {
            // 落下アニメーション再生
            animator.SetTrigger("Fall");

            JumpNowParticle.SetActive(false);
        }
    }
}

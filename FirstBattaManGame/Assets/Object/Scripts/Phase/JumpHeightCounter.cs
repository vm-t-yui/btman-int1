using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのジャンプ高さを計測するクラス
/// </summary>
public class JumpHeightCounter : MonoBehaviour
{
    [SerializeField] JumpController jumpController  = default;       // ジャンプ制御クラス
    [SerializeField] Transform      playerTransform = default;       // プレイヤーのトランスフォーム
    [SerializeField] Text           jumpHeightText  = default;       // ジャンプ高さのUIテキスト

    // １キロあたりの距離
    const float OneKiloMetreDistance = 10;
    // 地面のY軸の位置
    const float GroundPosY = -0.5f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void OnEnable()
    {
        // プレイヤーの上方向に力を与えて、ジャンプさせる
        jumpController.PlayerJump(InputController.TouchCountNum);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // ジャンプ高さを算出
        float jumpHeight = playerTransform.position.y - GroundPosY;
        if (jumpHeight != 0)
        {
            // ジャンプ高さをキロメートルに変換してUIに表示
            jumpHeightText.text = (jumpHeight / OneKiloMetreDistance).ToString("F1");
        }
    }
}

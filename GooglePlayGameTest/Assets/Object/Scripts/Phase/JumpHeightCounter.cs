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
    [SerializeField] ScoreDataManager scoreDate     = default;

    // ジャンプ高さ（キロメートル単位）
    public int JumpHeightToKiloMetre { get; private set; } = 0;
    // ジャンプ高さの結果が出たかどうか
    public bool IsJumpHeightResult { get; private set; } = false;

    const    float  OneKiloMetreDistance  = 1;           // １キロあたりの距離
    const    float  GroundPosY            = -0.5f;       // 地面のY軸の位置
    const    float  HeightUiMagnification = 10;          // UIに表示する高さの倍率
    readonly string DistanceUnit          = " km";       // 距離の単位

    /// <summary>
    /// 初期化処理
    /// </summary>
    void OnEnable()
    {
        // プレイヤーの上方向に力を与えて、ジャンプさせる
        jumpController.PlayerJump(InputController.TouchCountNum);
        // ジャンプ高さのUIを表示する
        GeneralFuncion.SetActiveFromAllChild(transform, true);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // ジャンプ高さを算出
        float jumpHeight = playerTransform.position.y - GroundPosY;
        // 高さをキロメートルに変換
        JumpHeightToKiloMetre = (int)((jumpHeight / OneKiloMetreDistance) * HeightUiMagnification);

        // ジャンプ中の処理
        if (jumpController.IsJumping)
        {
            if (jumpHeight != 0)
            {
                // ジャンプ高さをキロメートルに変換してUIに表示
                jumpHeightText.text = JumpHeightToKiloMetre.ToString() + DistanceUnit;
            }

        }
        // プレイヤーのジャンプが停止したら
        else
        {
            // ジャンプ高さの結果が出たフラグをtrueに変更
            IsJumpHeightResult = true;

            scoreDate.SetNowScore(JumpHeightToKiloMetre);
            scoreDate.SaveData();
        }
    }
}

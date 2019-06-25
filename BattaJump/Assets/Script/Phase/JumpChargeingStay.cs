using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジャンプ力チャージ前の待機処理
/// </summary>
public class JumpChargeingStay : MonoBehaviour
{
    [SerializeField]
    AchievementController achievementController = default;    // 実績コントロールクラス

    /// <summary>
    /// 開始
    /// </summary>
    void OnEnable()
    {
        // 鳥のさえずりを再生
        AudioPlayer.instance.PlaySe(AudioPlayer.SeType.BirdTwitter);
    }

    /// <summary>
    /// 終了
    /// </summary>
    void OnDisable()
    {
        // 鳥のさえずりを停止する
        AudioPlayer.instance.StopSe(AudioPlayer.SeType.BirdTwitter);
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 放置時間の実績解除チェック
        achievementController.CheckPutTime();
    }
}

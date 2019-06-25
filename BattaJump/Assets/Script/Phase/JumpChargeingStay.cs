﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジャンプ力チャージ前の待機処理
/// </summary>
public class JumpChargeingStay : MonoBehaviour
{
    /// <summary>
    /// 開始
    /// </summary>
    void OnEnable()
    {
        // 鳥のさえずりを再生
        AudioPlayer.instance.PlaySe(AudioPlayer.SeType.BirdTwitter);
    }
}

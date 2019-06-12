﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMのオーディオソース
/// </summary>
public class BgmAudioSource: MonoBehaviour
{
    // AudioSourceコンポーネント
    [SerializeField] AudioSource audioSource = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 再生が終了したBGMはオブジェクトを切る
        if (!audioSource.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}
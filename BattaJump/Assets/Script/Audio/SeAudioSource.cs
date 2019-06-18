using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SEのオーディオソース
/// </summary>
public class SeAudioSource : MonoBehaviour
{
    // AudioSourceコンポーネント
    [SerializeField] AudioSource audioSource = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 再生が終了したSEはオブジェクトを切る
        if (!audioSource.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}

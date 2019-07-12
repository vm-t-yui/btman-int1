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
    [SerializeField] float randomPitchBand = default;

    /// <summary>
    /// Awake
    /// </summary>
    void Awake()
    {
        // set random pitch if status is not zero
        if (randomPitchBand > 0)
        {
            audioSource.pitch += Random.Range(-randomPitchBand * 0.5f, randomPitchBand * 0.5f);
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 音量をセット
        audioSource.volume = AudioParameter.seVolume;
        // ミュートフラグをセット
        audioSource.mute = AudioParameter.seMute;

        // 再生が終了したSEはオブジェクトを切る
        if (!audioSource.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 終了
    /// </summary>
    void OnDisable()
    {
        // オーディオを停止する
        audioSource.Stop();
    }
}

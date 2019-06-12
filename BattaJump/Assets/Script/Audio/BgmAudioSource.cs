using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMのオーディオソース
/// </summary>
public class BgmAudioSource: MonoBehaviour
{
    // AudioSourceコンポーネント
    [SerializeField] AudioSource audioSource = default;
    // BGMのパラメータ
    [SerializeField] BgmParameter bgmParameter = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // BGMのパラメータをセットする
        audioSource.volume = bgmParameter.GetPlayVolume();
        audioSource.mute   = bgmParameter.GetMuteFlag();

        // 再生が終了したBGMはオブジェクトを切る
        if (!audioSource.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMのパラメータクラス
/// </summary>
public class BgmParameter : MonoBehaviour
{
    // AudioSourceコンポーネント
    [SerializeField] AudioSource bgmAudioSource = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 再生が終了したBGMはオブジェクトを切る
        if (!bgmAudioSource.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}

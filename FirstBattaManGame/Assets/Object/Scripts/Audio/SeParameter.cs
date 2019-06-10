using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SEのパラメータクラス
/// </summary>
public class SeParameter : MonoBehaviour
{
    // AudioSourceコンポーネント
    [SerializeField] AudioSource seAudioSource = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 再生が終了したSEはオブジェクトを切る
        if (!seAudioSource.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}

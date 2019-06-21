using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// セーブされているオーディオをロードする
/// </summary>
public class AudioParameterLoader : MonoBehaviour
{
    // オーディオパラメータクラス
    [SerializeField] AudioParameter audioParameter = default;

    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        // ロードする
        audioParameter.Load();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// セーブされているオーディオをロードする
/// </summary>
public class AudioParameterLoader : MonoBehaviour
{
    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        // ロードする
        AudioParameter.Load();
    }
}

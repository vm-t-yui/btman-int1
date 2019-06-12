using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGM共通のパラメータ
/// </summary>
[CreateAssetMenu]
public class BgmParameter : ScriptableObject
{
    // BGMの再生ボリューム
    [SerializeField][Range(0,1)]
    float playVolumeBgm = 1;

    // BGMがミュートかどうか
    [SerializeField]
    bool isMuteBgm = false;

    /// <summary>
    /// 再生ボリュームを取得
    /// </summary>
    /// <returns>現在のボリュームを返す</returns>
    public float GetPlayVolume()
    {
        return playVolumeBgm;
    }

    /// <summary>
    /// 再生ボリュームをセット
    /// </summary>
    /// <param name="set">セットする値</param>
    public void SetPlayVolume(float set)
    {
        playVolumeBgm = set;
    }

    /// <summary>
    /// 再生を行うかどうかのフラグを取得
    /// </summary>
    public bool GetMuteFlag()
    {
        return isMuteBgm;
    }

    /// <summary>
    /// 再生を行うかどうかのフラグをセット
    /// </summary>
    /// <param name="set">現在のフラグを返す</param>
    public void SetMuteFlag(bool set)
    {
        isMuteBgm = set;
    }
}

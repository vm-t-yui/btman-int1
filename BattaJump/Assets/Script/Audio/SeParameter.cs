using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SE共通のパラメータ
/// </summary>
[CreateAssetMenu]
public class SeParameter : ScriptableObject
{
    // SEの再生ボリューム
    [SerializeField][Range(0,1)]
    float playVolumeSe = 1;

    // SEがミュートかどうか
    [SerializeField]
    bool isMuteSe = false;

    /// <summary>
    /// 再生ボリュームを取得
    /// </summary>
    /// <returns>現在のボリュームを返す</returns>
    public float GetPlayVolume()
    {
        return playVolumeSe;
    }

    /// <summary>
    /// 再生ボリュームをセット
    /// </summary>
    /// <param name="set">セットする値</param>
    public void SetPlayVolume(float set)
    {
        playVolumeSe = set;
    }

    /// <summary>
    /// 再生を行うかどうかのフラグを取得
    /// </summary>
    public bool GetMuteFlag()
    {
        return isMuteSe;
    }

    /// <summary>
    /// 再生を行うかどうかのフラグをセット
    /// </summary>
    /// <param name="set">現在のフラグを返す</param>
    public void SetMuteFlag(bool set)
    {
        isMuteSe = set;
    }
}

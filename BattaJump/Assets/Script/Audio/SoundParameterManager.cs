using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンドのパラメータを管理
/// </summary>
public class SoundParameterManager : MonoBehaviour
{
    static public float bgmVolume { get; private set; } = 1;      // BGMの音量
    static public float seVolume  { get; private set; } = 1;      // SEの音量

    static public bool bgmMute { get; private set; } = false;     // BGMのミュート
    static public bool seMute  { get; private set; } = false;     // SEのミュート

    // 各パラメータのキー
    static string bgmVolumeDataKey;
    static string seVolumeDataKey;
    static string bgmMuteDataKey;
    static string seMuteDataKey;

    /// <summary>
    /// 各音量のセーブを行う
    /// </summary>
    /// <param name="bgm">BGMの音量</param>
    /// <param name="se">SEの音量</param>
    static public void SaveVolume(float bgm,float se)
    {
        // BGNとSEの音量
        PlayerPrefs.SetFloat(bgmVolumeDataKey, bgm);
        PlayerPrefs.SetFloat(seVolumeDataKey, se);
    }

    /// <summary>
    /// 各ミュートフラグのセーブを行う
    /// </summary>
    /// <param name="bgm">BGMのミュートフラグ</param>
    /// <param name="se">SEのミュートフラグ</param>
    static public void SaveMuteFlag(bool bgm,bool se)
    {
        // BGMとSEのミュート
        PlayerPrefs.SetInt(bgmMuteDataKey, bgm ? 1 : 0);
        PlayerPrefs.SetInt(seMuteDataKey, se ? 1 : 0);
    }

    /// <summary>
    /// 各パラメータのロードを行う
    /// </summary>
    static public void Load()
    {
        // BGMとSEの音量
        bgmVolume = PlayerPrefs.GetFloat(bgmVolumeDataKey);
        seVolume  = PlayerPrefs.GetFloat(seVolumeDataKey);

        // BGMのミュート（データが存在しない場合は、falseを表す０を返す）
        var bgmMuteSaveData = PlayerPrefs.GetInt(bgmMuteDataKey,0);
        bgmMute = (bgmMuteSaveData == 1) ? true : false;

        // SEのミュート（データが存在しない場合は、falseを表す０を返す）
        var seMuteSaveData = PlayerPrefs.GetInt(seMuteDataKey,0);
        seMute = (seMuteSaveData == 1) ? true : false;
    }
}

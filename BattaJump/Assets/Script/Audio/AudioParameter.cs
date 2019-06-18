using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オーディオのパラメータを管理
/// </summary>
public class AudioParameter : MonoBehaviour
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
    /// 開始
    /// </summary>
    void Start()
    {
        // データをロードする
        Load();
    }

    /// <summary>
    /// 各音量のセーブを行う
    /// </summary>
    static public void Save()
    {
        // BGNとSEの音量
        PlayerPrefs.SetFloat(bgmVolumeDataKey, bgmVolume);
        PlayerPrefs.SetFloat(seVolumeDataKey, seVolume);

        // BGMとSEのミュート
        PlayerPrefs.SetInt(bgmMuteDataKey, bgmMute ? 1 : 0);
        PlayerPrefs.SetInt(seMuteDataKey, seMute ? 1 : 0);
    }

    /// <summary>
    /// 各パラメータのロードを行う
    /// </summary>
    static public void Load()
    {
        // BGMとSEの音量（データが存在しない場合は、最大音量の１を返す）
        bgmVolume = PlayerPrefs.GetFloat(bgmVolumeDataKey,1);
        seVolume = PlayerPrefs.GetFloat(seVolumeDataKey,1);

        // BGMのミュート（データが存在しない場合は、falseを表す０を返す）
        var bgmMuteSaveData = PlayerPrefs.GetInt(bgmMuteDataKey, 0);
        bgmMute = (bgmMuteSaveData == 1) ? true : false;

        // SEのミュート（データが存在しない場合は、falseを表す０を返す）
        var seMuteSaveData = PlayerPrefs.GetInt(seMuteDataKey, 0);
        seMute = (seMuteSaveData == 1) ? true : false;
    }

    /// <summary>
    /// オーディオのBGMパラメータをセット
    /// </summary>
    /// <param name="bgmVolume">BGMのボリューム</param>
    /// <param name="bgmMute">BGMのミュートフラグ</param>
    static public void SetBgmParameter(float bgmVolume,bool bgmMute)
    {
        AudioParameter.bgmVolume = bgmVolume;
        AudioParameter.bgmMute   = bgmMute;
    }

    /// <summary>
    /// オーディオのSEパラメータをセット
    /// </summary>
    /// <param name="seVolume">SEのボリューム</param>
    /// <param name="seMute">SEのミュートフラグ</param>
    static public void SetSeParameter(float seVolume,bool seMute)
    {
        AudioParameter.seVolume = seVolume;
        AudioParameter.seMute   = seMute;
    }

    /// <summary>
    /// オプション画面が閉じられた際のコールバック
    /// </summary>
    public void OnOptionClose()
    {
        // パラメータをセーブする
        Save();
    }
}

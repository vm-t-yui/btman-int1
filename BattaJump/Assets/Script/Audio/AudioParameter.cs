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
    [SerializeField] string bgmVolumeDataKey;
    [SerializeField] string seVolumeDataKey;
    [SerializeField] string bgmMuteDataKey;
    [SerializeField] string seMuteDataKey;

    /// <summary>
    /// 各音量のセーブを行う
    /// </summary>
    public void Save()
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
    public void Load()
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
    /// BGMボリュームをセット
    /// </summary>
    /// <param name="bgmVolume">BGMのボリューム</param>
    public void SetBgmVolume(float bgmVolume)
    {
        AudioParameter.bgmVolume = bgmVolume;
    }

    /// <summary>
    /// BGMのミュートフラグをセット
    /// </summary>
    /// <param name="bgmMute">セットするフラグ</param>
    public void SetBgmMuteFlag(bool bgmMute)
    {
        AudioParameter.bgmMute = bgmMute;
    }

    /// <summary>
    /// SEボリュームをセット
    /// </summary>
    /// <param name="seVolume">SEのボリューム</param>
    public void SetSeVolume(float seVolume)
    {
        AudioParameter.seVolume = seVolume;
    }

    /// <summary>
    /// SEのミュートフラグをセット
    /// </summary>
    /// <param name="seMute">セットするフラグ</param>
    public void SetSeMuteFlag(bool seMute)
    {
        AudioParameter.seMute = seMute;
    }

    /// <summary>
    /// オプション画面が閉じられた際のコールバック
    /// </summary>
    void OnOptionClose()
    {
        // パラメータをセーブする
        Save();
    }
}

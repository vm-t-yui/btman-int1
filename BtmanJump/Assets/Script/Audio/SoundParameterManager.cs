using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンドのパラメータを管理
/// </summary>
/// NOTE : スクリプト名とクラス名を「SoundParameterManager」に変更する予定だが
///        ここで変更してしまうと前のプルリクとの差分が見れなくなるのでのちのプルリクで変更する。
public class SoundParameterManager : MonoBehaviour
{
 static public float bgmVolume { get; private set; } = 1;      // BGMの音量
    static public float seVolume  { get; private set; } = 1;      // SEの音量

    static public bool bgmMute { get; private set; } = false;     // BGMのミュート
    static public bool seMute  { get; private set; } = false;     // SEのミュート

    // 各パラメータのキー
    static string bgmVolumeDataKey = null;
    static string seVolumeDataKey = null;
    static string bgmMuteDataKey = null;
    static string seMuteDataKey = null;

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
        SoundParameterManager.bgmVolume = bgmVolume;
        SoundParameterManager.bgmMute   = bgmMute;
    }

    /// <summary>
    /// オーディオのSEパラメータをセット
    /// </summary>
    /// <param name="seVolume">SEのボリューム</param>
    /// <param name="seMute">SEのミュートフラグ</param>
    static public void SetSeParameter(float seVolume,bool seMute)
    {
        SoundParameterManager.seVolume = seVolume;
        SoundParameterManager.seMute   = seMute;
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

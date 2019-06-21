using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オーディオの設定UIの制御
/// </summary>
public class AudioSettingUiController : MonoBehaviour
{
    // オーディオパラメータクラス
    [SerializeField] AudioParameter audioParameter = default;

    [SerializeField] Slider bgmVolumeSlider = default;
    [SerializeField] Slider seVolumeSlider = default;

    [SerializeField] Toggle bgmMuteToggle = default;
    [SerializeField] Toggle seMuteToggle  = default;

    /// <summary>
    /// 開始
    /// </summary>
    void OnEnable()
    {
        // ロードしたパラメータをUIに反映
        bgmVolumeSlider.value = AudioParameter.bgmVolume;   // BGMボリューム
        seVolumeSlider.value = AudioParameter.seVolume;     // SEボリューム
        bgmMuteToggle.isOn = AudioParameter.bgmMute;        // BGMミュートフラグ
        seMuteToggle.isOn = AudioParameter.seMute;          // SEミュートフラグ
    }

    /// <summary>
    /// BGMのスライダーが変更された際のコールバック
    /// </summary>
    public void OnBgmVolumeSliderChange()
    {
        // スライダーの値をBGMのボリュームとしてセットする
        audioParameter.SetBgmVolume(bgmVolumeSlider.value);
    }

    /// <summary>
    /// BGMのミュートトグルが変更された際のコールバック
    /// </summary>
    public void OnBgmMuteToggleChange()
    {
        // BGMのトグルのフラグをミュートフラグとしてセット
        audioParameter.SetBgmMuteFlag(bgmMuteToggle.isOn);
    }

    /// <summary>
    /// SEのスライダーが変更された際のコールバック
    /// </summary>
    public void OnSeVolumeSliderChange()
    {
        // スライダーの値をSEのボリュームとしてセットする
        audioParameter.SetSeVolume(seVolumeSlider.value);
    }

    /// <summary>
    /// SEのミュートトグルが変更された際のコールバック
    /// </summary>
    public void OnSeMuteToggleChange()
    {
        // SEのトグルのフラグをミュートフラグとしてセット
        audioParameter.SetSeMuteFlag(seMuteToggle.isOn);
    }
}

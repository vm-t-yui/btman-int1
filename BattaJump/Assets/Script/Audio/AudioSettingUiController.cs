using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オーディオの設定UIの制御
/// </summary>
public class AudioSettingUiController : MonoBehaviour
{
    [SerializeField] Slider bgmVolumeSlider = default;
    [SerializeField] Slider seVolumeSlider = default;

    [SerializeField] Toggle bgmMuteToggle = default;
    [SerializeField] Toggle seMuteToggle  = default;

    /// <summary>
    /// BGMのパラメータが変更された際のコールバック
    /// </summary>
    public void OnBgmParameterChange()
    {
        // スライダーの値をBGMのパラメータとしてセットする
        AudioParameter.SetBgmParameter(bgmVolumeSlider.value, bgmMuteToggle.isOn);
    }

    /// <summary>
    /// SEのパラメータが変更された際のコールバック
    /// </summary>
    public void OnSeParameterChange()
    {
        // スライダーの値をSEのパラメータとしてセットする
        AudioParameter.SetSeParameter(seVolumeSlider.value, seMuteToggle.isOn);
    }
}

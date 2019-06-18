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

    /// <summary>
    /// BGMのボリュームスライダーが変更された際のコールバック
    /// </summary>
    /// TODO : 未実装 （SetBgmParameterの第二引数はミュートかどうかのフラグ変数をセットするが、オプションのミュートを設定するUIが完成していないので仮でfalseをセット）
    public void OnBgmVolumeSliderChange()
    {
        // スライダーの値をBGMのパラメータとしてセットする
        SoundParameterManager.SetBgmParameter(bgmVolumeSlider.value, false);
    }

    /// <summary>
    /// SEのボリュームスライダーが変更された際のコールバック
    /// </summary>
    /// TODO : 未実装 （SetSeParameterの第二引数はミュートかどうかのフラグ変数をセットするが、オプションのミュートを設定するUIが完成していないので仮でfalseをセット）
    public void OnSeVolumeSliderChange()
    {
        // スライダーの値をSEのパラメータとしてセットする
        SoundParameterManager.SetSeParameter(seVolumeSlider.value, false);
    }
}

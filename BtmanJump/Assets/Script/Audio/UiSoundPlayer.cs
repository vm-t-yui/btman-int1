using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI関連のサウンドの再生を行う
/// </summary>
public class UiSoundPlayer : MonoBehaviour
{
    /// <summary>
    /// セレクト音を再生
    /// </summary>
    public void PlaySelectButtonSound()
    {
        AudioPlayer.instance.PlaySe(AudioPlayer.SeType.SelectButton);
    }

    /// <summary>
    /// キャンセル音を再生
    /// </summary>
    public void PlayCancelButtonSound()
    {
        AudioPlayer.instance.PlaySe(AudioPlayer.SeType.CancelButton);
    }

    /// <summary>
    /// パネルを開く音を再生
    /// </summary>
    public void PlayPanelOpenSound()
    {
        AudioPlayer.instance.PlaySe(AudioPlayer.SeType.PanelOpen);
    }
}

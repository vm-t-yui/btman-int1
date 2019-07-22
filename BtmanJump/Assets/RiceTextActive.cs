using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 米の文字の表示
/// </summary>
public class RiceTextActive : MonoBehaviour
{
    [SerializeField]
    GameObject riceText = default;           // 米の文字

    /// <summary>
    /// 米の文字を表示する
    /// </summary>
    public void Active()
    {
        riceText.SetActive(true);

        // ドラ音再生
        AudioPlayer.instance.PlaySe(AudioPlayer.SeType.Dora);
    }
}

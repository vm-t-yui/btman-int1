using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンドの再生テスト用クラス
/// NOTE : ゲームの内容には無関係
/// </summary>
public class SoundPlayTest : MonoBehaviour
{
    // オーディオプレイヤー
    [SerializeField] AudioPlayer audioPlayer = default;

    // 更新
    void Update()
    {
        // BGMの再生
        if (Input.GetKeyDown(KeyCode.Return))
        {
            audioPlayer.PlayBgm(AudioPlayer.BgmType.SampleBgm1);
        }

        // BGMの停止
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            audioPlayer.StopBgm();
        }

        // SEの再生
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioPlayer.PlaySe(AudioPlayer.SeType.SampleSe1);
        }
    }
}

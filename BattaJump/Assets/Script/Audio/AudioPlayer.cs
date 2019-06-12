using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オーディオ再生クラス
/// </summary>
public class AudioPlayer : MonoBehaviour
{
    /// <summary>
    /// BGMの種類
    /// </summary>
    public enum BgmType
    {
        SampleBgm1,
        SampleBgm2,
        SampleBgm3,
    }

    /// <summary>
    /// SEの種類
    /// </summary>
    public enum SeType
    {
        SampleSe1,
        SampleSe2,
        SampleSe3,
    }

    [SerializeField] Transform parentBgm            = default;     // BGMの親オブジェクトのトランスフォーム
    [SerializeField] Transform parentSe             = default;     // SEの親オブジェクトのトランスフォーム
    [SerializeField] Transform parentPlayingSe      = default;     // 再生中のSEの親オブジェクトのトランスフォーム

    // BGMが再生中かどうか
    bool isPlayingBgm = false;

    /// <summary>
    /// BGMの再生
    /// </summary>
    /// <param name="type">再生するBGMの種類</param>
    public void PlayBgm(BgmType type)
    {
        // BGMが既に再生中（フラグが立っていれば）であれば停止する
        if (isPlayingBgm)
        {
            // 全てBGMに対してSetActiveを行いBGMを停止させる
            GeneralFuncion.SetActiveFromAllChild(parentBgm, false);
            // フラグを倒す
            isPlayingBgm = false;
        }

        // 指定のBGMを再生する
        parentBgm.GetChild((int)type).gameObject.SetActive(true);
        // フラグを起こす
        isPlayingBgm = true;
    }

    /// <summary>
    /// BGMを停止
    /// </summary>
    public void StopBgm()
    {
        if (isPlayingBgm)
        {
            // 全てBGMに対してSetActiveを行いBGMを停止させる
            GeneralFuncion.SetActiveFromAllChild(parentBgm, false);
            // フラグを倒す
            isPlayingBgm = false;
        }
    }

    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="type">再生するSEの種類</param>
    public void PlaySe(SeType type)
    {
        // 再生するSEのオブジェクトを複製する（再生中のSEを持つ親オブジェクトにセット）
        GameObject playingSe = Instantiate(parentSe.GetChild((int)type).gameObject, parentPlayingSe);
        // 再生するSEのオブジェクトをオンにする
        playingSe.SetActive(true);
    }

    /// <summary>
    /// 指定のSEを停止する
    /// </summary>
    public void StopAllSe()
    {
        // 全ての再生中のSEに対してSetActiveを行う
        GeneralFuncion.SetActiveFromAllChild(parentSe, false);
    }
}

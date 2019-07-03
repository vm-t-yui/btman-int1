using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

/// <summary>
/// メインのスプライトクラス
/// </summary>
public class MainSpriteSetter : MonoBehaviour
{
    [SerializeField]
    Image gage = default;               //カウントダウンのゲージ

    [SerializeField]
    Image gageZero = default;           //ゼロの状態のカウントダウンのゲージ

    [SerializeField]
    SpriteAtlas canvasAtlas = default;  //カンバス用スプライトアトラス

    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        //ゲージのスプライト設定
        gage.sprite = canvasAtlas.GetSprite("timer");

        gageZero.sprite = canvasAtlas.GetSprite("timerZero");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面フェードコントロールクラス
/// </summary>
public class DisplayFadeContoller : MonoBehaviour
{
    /// <summary>
    /// フェードのタイプ
    /// </summary>
    public enum FadeType
    {
        FadeIn,       // フェードイン
        FadeOut       // フェードアウト
    }

    /// <summary>
    /// 使用するパネルのタイプ（色）
    /// </summary>
    public enum PanelType
    {
        Black,        // 黒
        White,        // 白
        AdView        // 黒＋広告
    }

    [SerializeField]
    CanvasGroup fadeCanvas = default;                       // フェードさせるカンバス

    [SerializeField]
    GameObject blackPanel = default;                        // 黒いパネル
    [SerializeField]
    GameObject whitePanel = default;                        // 白いパネル
    [SerializeField]
    GameObject adPanel = default;                           // 広告パネル

    [SerializeField][Range(0.0f, 0.1f)]
    float fadeSpeed = 0.02f;                                // フェードするスピード

    FadeType fadeType;                                      // フェードのタイプ
    PanelType panelType;                                    // フェードさせるパネルのタイプ

    public bool IsFade { get; private set; } = false;       // フェード中
    public bool IsFadeEnd { get; private set; } = false;    // フェード終了

    float adViewTime = 0f;                                  // 広告表示時間計測
    const float adViewMaxTime = 2.5f;                       // 広告表示時間

    /// <summary>
    /// 更新
    /// </summary>
    void FixedUpdate()
    {
        // フェード中のフラグがtrueならフェード処理
        if (IsFade && !IsFadeEnd)
        {
            Fade();
        }
    }

    /// <summary>
    /// パネルの状態設定
    /// シーンの最初にフェードアウトさせたい時はStart()などでこれを呼ぶ
    /// </summary>
    /// <param name="panel">設定する色（これによってどのパネルをいじるか判断）</param>
    /// <param name="isView">trueなら不透明、falseなら透明で表示</param>
    public void OnPanel(PanelType panel, bool isView)
    {
        fadeCanvas.gameObject.SetActive(true);

        // 不透明で表示するならalphaを1、そうでなければ0に設定
        if (isView)
        {
            fadeCanvas.alpha = 1;
        }
        else
        {
            fadeCanvas.alpha = 0;
        }

        // 指定されたパネルを表示、他は非表示
        switch (panel)
        {
            case PanelType.Black:    // 黒
                blackPanel.SetActive(true);
                whitePanel.SetActive(false);
                adPanel.SetActive(false);
                break;

            case PanelType.White:    // 白
                whitePanel.SetActive(true);
                blackPanel.SetActive(false);
                adPanel.SetActive(false);
                break;

            case PanelType.AdView:  // 広告
                adPanel.SetActive(true);
                blackPanel.SetActive(false);
                whitePanel.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// フェード開始処理（これをフェード処理したいタイミングで呼ぶ）
    /// </summary>
    /// <param name="type">フェードのタイプ</param>
    /// <param name="panel">パネルタイプ</param>
    public void OnFade(FadeType type, PanelType panel)
    {
        fadeType = type;
        panelType = panel;
        IsFade = true;
        IsFadeEnd = false;

        // フェードインなら透明の状態でカンバスを出す
        if (fadeType == (int)FadeType.FadeIn)
        {
            OnPanel(panelType, false);
        }
        // フェードアウトなら不透明でパネルを出す
        else
        {
            OnPanel(panelType, true);
        }
    }

    /// <summary>
    /// フェード処理
    /// </summary>
    void Fade()
    {
        // フェードイン
        if (fadeType == FadeType.FadeIn)
        {
            // alphaが1以上になるまで増加
            fadeCanvas.alpha += fadeSpeed;

            // 1以上になったらフェード処理終了
            if (fadeCanvas.alpha >= 1)
            {
                fadeCanvas.alpha = 1;
                IsFade = false;
                IsFadeEnd = true;
            }
        }
        // フェードアウト
        else
        {
            // パネルタイプが広告なら、指定した時間までフェードアウトを待つ
            if (panelType == PanelType.AdView && adViewTime < adViewMaxTime)
            {
                // 表示時間を計測
                adViewTime += Time.deltaTime;
            }
            else
            {
                // alphaが0以下になるまで減少
                fadeCanvas.alpha -= fadeSpeed;
            }

            // 0以下になったらフェード処理終了
            if (fadeCanvas.alpha <= 0)
            {
                adViewTime = 0;
                fadeCanvas.alpha = 0;

                fadeCanvas.gameObject.SetActive(false);

                IsFade = false;
                IsFadeEnd = true;
            }
        }
    }
}

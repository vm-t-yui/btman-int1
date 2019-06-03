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
    /// フェード色
    /// </summary>
    public enum FadeColor
    {
        Black,        // 黒
        White,        // 白
        Logo          // ロゴが入った白
    }

    [SerializeField]
    CanvasGroup fadeCanvas = default;                       // フェードさせるカンバス

    [SerializeField]
    GameObject blackPanel = default;                        // 黒いパネル
    [SerializeField]
    GameObject whitePanel = default;                        // 白いパネル
    [SerializeField]
    GameObject logoPanel = default;                         // ロゴが入ったパネル

    [SerializeField][Range(0.0f, 0.1f)]
    float fadeSpeed = 0.02f;                                // フェードするスピード

    FadeType fadeType;                                      // フェードのタイプ
    FadeColor fadeColor;                                    // フェードさせるパネルの色

    public bool IsFade { get; private set; } = false;       // フェード中
    public bool IsFadeEnd { get; private set; } = false;    // フェード終了

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // フェード中のフラグがtrueならフェード処理
        if (IsFade)
        {
            Fade();
        }
    }

    /// <summary>
    /// カンバスの状態設定（シーンの初めに画面を隠しておきたいときにも呼ぶ）
    /// </summary>
    /// <param name="color">設定する色（これによってどのカンバスをいじるか判断）</param>
    /// <param name="isView">透明かどうか</param>
    public void OnPanel(FadeColor color, bool isView)
    {
        // 透明ならalphaを1、そうでなければ0に設定
        if (isView)
        {
            fadeCanvas.alpha = 1;
        }
        else
        {
            fadeCanvas.alpha = 0;
        }

        // 指定された色のパネルを表示、他２つは非表示
        switch (color)
        {
            case FadeColor.Black:    // 黒
                blackPanel.SetActive(isView);
                whitePanel.SetActive(false);
                logoPanel.SetActive(false);
                break;

            case FadeColor.White:    // 白
                whitePanel.SetActive(isView);
                blackPanel.SetActive(false);
                logoPanel.SetActive(false);
                break;

            case FadeColor.Logo:  // ロゴ入り 
                logoPanel.SetActive(isView);
                blackPanel.SetActive(false);
                whitePanel.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// フェード開始処理（これをフェード処理したいタイミングで呼ぶ）
    /// </summary>
    /// <param name="type">フェードのタイプ</param>
    /// <param name="color">フェード色</param>
    public void OnFade(FadeType type, FadeColor color)
    {
        fadeType = type;
        fadeColor = color;
        IsFade = true;
        IsFadeEnd = false;

        // フェードインなら透明の状態でカンバスを出す
        if (fadeType == (int)FadeType.FadeIn)
        {
            OnPanel(fadeColor, false);
        }
        // フェードアウトなら不透明でカンバスを出す
        else
        {
            OnPanel(fadeColor, true);
        }
    }

    /// <summary>
    /// フェード処理
    /// </summary>
    void Fade()
    {
        // フェードイン
        if (fadeType == (int)FadeType.FadeIn)
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
            // alphaが0以下になるまで減少
            fadeCanvas.alpha -= fadeSpeed;

            // 0以下になったらフェード処理終了
            if (fadeCanvas.alpha <= 0)
            {
                fadeCanvas.alpha = 0;
                IsFade = false;
                IsFadeEnd = true;
            }
        }
    }
}

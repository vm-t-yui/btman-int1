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
        FadeIn = 0,   // フェードイン
        FadeOut       // フェードアウト
    }

    /// <summary>
    /// フェード色
    /// </summary>
    public enum FadeColor
    {
        Black,    // 黒
        White,    // 白
        Loading   // ロードの文字が入った白
    }

    [SerializeField]
    CanvasGroup blackCanvas;                                // 黒いパネル
    [SerializeField]
    CanvasGroup whiteCanvas;                                // 白いパネル
    [SerializeField]
    CanvasGroup loadingCanvas;                              // ロードの文字が入ったパネル

    CanvasGroup fadeCanvas;                                 // 実際にフェードさせるカンバス

    [SerializeField][Range(0.0f, 0.1f)]
    float fadeSpeed = 0.02f;                                // フェードするスピード

    int fadeType;                                           // フェードのタイプ
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
            Fade(fadeCanvas);
        }
    }

    /// <summary>
    /// カンバスの状態設定（シーンの初めに画面を隠しておきたいときにも呼ぶ）
    /// </summary>
    /// <param name="color">設定する色（これによってどのカンバスをいじるか判断）</param>
    /// <param name="isView">透明かどうか</param>
    public void OnCanvas(FadeColor color, bool isView)
    {
        float alpha;

        // 透明ならalphaを1、そうでなければ0に設定
        if (isView)
        {
            alpha = 1;
        }
        else
        {
            alpha = 0;
        }

        // 指定された色のカンバスを更新、フェード処理に使用するカンバスを設定
        switch (color)
        {
            case FadeColor.Black:    // 黒
                blackCanvas.gameObject.SetActive(isView);
                blackCanvas.alpha = alpha;
                fadeCanvas = blackCanvas;
                break;

            case FadeColor.White:    // 白
                whiteCanvas.gameObject.SetActive(isView);
                whiteCanvas.alpha = alpha;
                fadeCanvas = whiteCanvas;
                break;

            case FadeColor.Loading:  // ロード文字入り 
                loadingCanvas.gameObject.SetActive(isView);
                loadingCanvas.alpha = alpha;
                fadeCanvas = loadingCanvas;
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
        fadeType = (int)type;
        fadeColor = color;
        IsFade = true;
        IsFadeEnd = false;

        // フェードインなら透明の状態でカンバスを出す
        if (fadeType == (int)FadeType.FadeIn)
        {
            OnCanvas(fadeColor, false);
        }
        // フェードアウトなら不透明でカンバスを出す
        else
        {
            OnCanvas(fadeColor, true);
        }
    }

    /// <summary>
    /// フェード処理
    /// </summary>
    /// <param name="canvas">フェード処理に使用するカンバス</param>
    void Fade(CanvasGroup canvas)
    {
        // フェードイン
        if (fadeType == (int)FadeType.FadeIn)
        {
            // alphaが1以上になるまで増加
            canvas.alpha += fadeSpeed;

            // 1以上になったらフェード処理終了
            if (canvas.alpha >= 1)
            {
                IsFade = false;
                IsFadeEnd = true;
            }
        }
        // フェードアウト
        else
        {
            // alphaが0以下になるまで減少
            canvas.alpha -= fadeSpeed;

            // 0以下になったらフェード処理終了
            if (canvas.alpha <= 0)
            {
                canvas.gameObject.SetActive(false);
                IsFade = false;
                IsFadeEnd = true;
            }
        }
    }
}

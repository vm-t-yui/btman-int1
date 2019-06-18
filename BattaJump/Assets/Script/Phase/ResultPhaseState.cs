using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルトフェーズ管理クラス
/// </summary>
public class ResultPhaseState : MonoBehaviour
{
    /// <summary>
    /// フェーズの状態
    /// </summary>
    enum PhaseType
    {
        WaitAdLoad,                                  // ロード完了待機
        WaitFadeOut,                                 // フェードアウト待機
        CountScore,                                  // スコアカウント中
        ViewResult,                                  // リザルト表示中
        NextScene                                    // 次のシーンへ
    }

    PhaseType nowPhase;                             // フェーズの状態

    [SerializeField]
    DisplayFadeContoller fadeContoller = default;    // フェード管理クラス

    [SerializeField]
    AdManager adManager = default;                   // 広告管理クラス

    [SerializeField]
    ScoreCountUp scoreCountUp = default;             // スコアカウントアップクラス

    [SerializeField]
    ResultEnd resultEnd = default;                   // リザルト終了検知クラス

    [SerializeField]
    NextSceneChanger nextScene = default;            // シーン移行クラス

    [SerializeField]
    GameObject resultCanvas = default;               // ボタン等のカンバス

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 広告管理クラス起動
        adManager.gameObject.SetActive(true);

        // 画面を白いパネルで隠す
        fadeContoller.OnPanel(DisplayFadeContoller.PanelType.White, true);

        // 現在のフェーズをロード完了待機に設定
        nowPhase = PhaseType.WaitAdLoad;

        // FPSを15に固定
        Application.targetFrameRate = 15;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 現在のフェーズの状態により処理を分岐
        switch (nowPhase)
        {
            case PhaseType.WaitAdLoad:        // 広告ロード完了待機

                // 広告のロードが完了したら次の処理へ
                if (adManager.IsLoaded())
                {
                    // フェードアウト開始
                    fadeContoller.OnFade(DisplayFadeContoller.FadeType.FadeOut, DisplayFadeContoller.PanelType.White);

                    // FPSをもとに戻す
                    Application.targetFrameRate = -1;

                    nowPhase = PhaseType.WaitFadeOut;
                }
                break;

            case PhaseType.WaitFadeOut:       // フェードアウト待機

                // フェードアウトが終わったら次の処理へ
                if (fadeContoller.IsFadeEnd)
                {
                    // バナー広告を表示
                    adManager.ShowBanner();

                    // スコアカウントアップ処理開始
                    scoreCountUp.enabled = true;

                    nowPhase = PhaseType.CountScore;
                }
                break;

            case PhaseType.CountScore:        // スコアカウント中

                // カウントが終わったら次の処理へ
                if (scoreCountUp.IsEnd)
                {
                    // スコアカウントアップ処理終了
                    scoreCountUp.enabled = false;

                    resultCanvas.SetActive(true);

                    // インタースティシャル広告を表示
                    adManager.ShowInterstitial();

                    nowPhase = PhaseType.ViewResult;
                }
                break;

            case PhaseType.ViewResult:        // リザルト表示中

                // リザルトが終了したら次の処理へ
                if (resultEnd.IsEnd)
                {
                    // 次に読むシーンがメインゲームなら黒いパネルでフェードイン開始
                    if (nextScene.NextSceneNum == SceneLoader.SceneNum.MainGame)
                    {
                        fadeContoller.OnFade(DisplayFadeContoller.FadeType.FadeIn, DisplayFadeContoller.PanelType.Black);
                    }
                    // そうでなければ白いパネルでフェードイン開始
                    else
                    {
                        fadeContoller.OnFade(DisplayFadeContoller.FadeType.FadeIn, DisplayFadeContoller.PanelType.White);
                    }

                    // バナー広告を非表示
                    adManager.HideBanner();

                    nowPhase = PhaseType.NextScene;
                }
                break;

            case PhaseType.NextScene:         // 次のシーンへ

                // フェードインが終わったら次のシーンへ移行
                if (fadeContoller.IsFadeEnd)
                {
                    // シーンをロード
                    nextScene.ChangeNextScene();
                }
                break;
        }
    }
}

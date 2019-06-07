using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルフェーズ管理クラス
/// </summary>
public class TitlePhaseState : MonoBehaviour
{
    /// <summary>
    /// フェーズの状態
    /// </summary>
    enum PhaseState
    {
        WaitAdLoad,                                  // ロード完了待機
        WaitFadeOut,                                 // フェードアウト待機
        ViewTitle,                                   // タイトル表示中
        NextScene                                    // 次のシーンへ
    }

    PhaseState nowPhase;                             // フェーズの状態

    [SerializeField]
    DisplayFadeContoller fadeContoller = default;    // フェード管理クラス

    [SerializeField]
    AdManager adManager = default;                   // 広告管理クラス

    [SerializeField]
    TutorialViewer tutorialViewer = default;         // チュートリアル表示クラス

    [SerializeField]
    NextSceneChanger nextScene = default;            // シーン移行クラス

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
        nowPhase = PhaseState.WaitAdLoad;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 現在のフェーズの状態により処理を分岐
        switch (nowPhase)
        {
            case PhaseState.WaitAdLoad:        // 広告ロード完了待機

                // 広告のロードが完了したら次の処理へ
                if (adManager.IsLoaded())
                {
                    // フェードアウト開始
                    fadeContoller.OnFade(DisplayFadeContoller.FadeType.FadeOut, DisplayFadeContoller.PanelType.White);

                    nowPhase = PhaseState.WaitFadeOut;
                }
                break;

            case PhaseState.WaitFadeOut:       // フェードアウト待機

                // フェードアウトが終わったら次の処理へ
                if (fadeContoller.IsFadeEnd)
                {
                    // バナー広告を表示
                    adManager.ShowBanner();

                    nowPhase = PhaseState.ViewTitle;
                }
                break;

            case PhaseState.ViewTitle:          // タイトル表示中

                // チュートリアルを閉じたら次の処理へ
                if (tutorialViewer.IsEnd)
                {
                    // フェードイン開始
                    fadeContoller.OnFade(DisplayFadeContoller.FadeType.FadeIn, DisplayFadeContoller.PanelType.Black);

                    // チュートリアル表示クラスをオフにする
                    tutorialViewer.gameObject.SetActive(false);

                    // バナー広告を非表示
                    adManager.HideBanner();

                    nowPhase = PhaseState.NextScene;
                }
                break;

            case PhaseState.NextScene:         // 次のシーンへ

                // フェードインが終わったら次のシーンへ移行
                if (fadeContoller.IsFadeEnd)
                {
                    // 次に読むシーンをメインゲームに設定
                    nextScene.SetNextSceneMainGame();

                    // シーンをロード
                    nextScene.ChangeNextScene();
                }
                break;
        }
    }
}

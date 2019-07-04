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
    enum PhaseType
    {
        WaitAdLoad,                                  // ロード完了待機
        WaitFadeOut,                                 // フェードアウト待機
        ViewTitle,                                   // タイトル表示中
        NextScene,                                   // 次のシーンへ
        SceneEnd                                     // シーンの終了
    }

    PhaseType nowPhase;                             // フェーズの状態

    [SerializeField]
    DisplayFadeContoller fadeContoller = default;    // フェード管理クラス

    [SerializeField]
    AdManager adManager = default;                   // 広告管理クラス

    [SerializeField]
    TutorialViewer tutorialViewer = default;         // チュートリアル表示クラス

    [SerializeField]
    NextSceneChanger nextScene = default;            // シーン移行クラス

    float loadTime = 0f;                             // ロード待機時間
    const float LoadMaxTime = 3f;                    // ロード待機最大時間

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

                loadTime += Time.deltaTime;

                // 広告のロードが完了したら次の処理へ
                if (adManager.IsLoaded() || loadTime >= LoadMaxTime)
                {
                    // フェードアウト開始
                    fadeContoller.OnFade(DisplayFadeContoller.FadeType.FadeOut, DisplayFadeContoller.PanelType.White);

                    // タイトルBGMを再生
                    AudioPlayer.instance.PlayBgm(AudioPlayer.BgmType.Title);

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

                    nowPhase = PhaseType.ViewTitle;
                }
                break;

            case PhaseType.ViewTitle:          // タイトル表示中

                // チュートリアルを閉じたら次の処理へ
                if (tutorialViewer.IsEnd)
                {
                    // フェードイン開始
                    fadeContoller.OnFade(DisplayFadeContoller.FadeType.FadeIn, DisplayFadeContoller.PanelType.Black);

                    // チュートリアル表示クラスをオフにする
                    tutorialViewer.gameObject.SetActive(false);

                    // バナー広告を非表示
                    adManager.HideBanner();

                    nowPhase = PhaseType.NextScene;
                }
                break;

            case PhaseType.NextScene:         // 次のシーンへ

                // フェードインが終わったら次のシーンへ移行
                if (fadeContoller.IsFadeEnd)
                {
                    // 次に読むシーンをメインゲームに設定
                    nextScene.SetNextSceneMainGame();

                    // シーンをロード
                    nextScene.ChangeNextScene();

                    // タイトルBGMを停止する
                    AudioPlayer.instance.StopBgm();

                    nowPhase = PhaseType.SceneEnd;
                }
                break;

            case PhaseType.SceneEnd:          // シーンの終了
                // 処理なし
                break;
        }
    }
}

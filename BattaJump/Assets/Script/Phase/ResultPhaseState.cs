﻿using System.Collections;
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
        NextScene,                                   // 次のシーンへ
        SceneEnd                                     // シーンの終了
    }

    PhaseType nowPhase;                             // フェーズの状態

    // フェード管理クラス
    [SerializeField]
    DisplayFadeContoller fadeContoller = default;
    // 広告管理クラス
    [SerializeField]
    AdManager adManager = default;
    // クレーター生成クラス
    [SerializeField]
    CraterCreater craterCreater = default;
    // スコアカウントアップクラス
    [SerializeField]
    ScoreCountUp scoreCountUp = default;
    // 動画広告勧誘クラス
    [SerializeField]
    AdVideoRecommender adVideoRecommender = default;
    // プレイデータ管理クラス
    [SerializeField]
    PlayDataManager playData = default;
    // 実績コントロールクラス
    [SerializeField]
    AchievementController achievementController = default;
    // リザルト終了検知クラス
    [SerializeField]
    ResultEnd resultEnd = default;
    // シーン移行クラス
    [SerializeField]
    NextSceneChanger nextScene = default;

    // ボタン等のカンバス
    [SerializeField]
    GameObject resultCanvas = default;
    // プレイヤーのオブジェクト
    [SerializeField]
    GameObject playerObj = default;

    float landingTime = 0;                           // 着地のアニメーション再生時間
    const float LandingCraterCreateTime = 0.1f;      // 着地アニメーション中にクレーターを生成する時間

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

        // プレイ回数を加算
        playData.IncreasePlayCount();

        // 動画広告勧誘クラス初期化
        adVideoRecommender.Init();
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

                    //プレイヤーを表示
                    playerObj.SetActive(true);

                    nowPhase = PhaseType.CountScore;
                }
                break;

            case PhaseType.CountScore:        // スコアカウント中

                landingTime += Time.deltaTime;
                // 着地アニメーションの再生時間が指定した時間まで行ったら
                if (landingTime >= LandingCraterCreateTime && craterCreater.enabled)
                {
                    // クレーターを生成
                    craterCreater.Create();
                    // クレーターを消す処理が入ってるので、スクリプトをオフにする
                    craterCreater.enabled = false;
                }

                // カウントが終わったら次の処理へ
                if (scoreCountUp.IsEnd)
                {
                    // スコアカウントアップ処理終了
                    scoreCountUp.enabled = false;

                    // リザルト用カンバスを表示
                    resultCanvas.SetActive(true);

                    // インタースティシャル広告を表示
                    adManager.ShowInterstitial();

                    // 実績解除できるかチェック
                    achievementController.CheckRelease();

                    nowPhase = PhaseType.ViewResult;
                }
                break;

            case PhaseType.ViewResult:        // リザルト表示中

                // インタースティシャル広告が閉じられて、勧誘済みでないなら
                if (adManager.IsInterstitialClosed() && !adVideoRecommender.IsRecommend)
                {
                    // 動画広告勧誘
                    adVideoRecommender.Recommend();
                }

                // リザルトが終了したら次の処理へ
                if (resultEnd.IsEnd || adVideoRecommender.IsEnd)
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
                    // 
                    playData.SaveData();

                    // シーンをロード
                    nextScene.ChangeNextScene();

                    nowPhase = PhaseType.SceneEnd;
                }
                break;

            case PhaseType.SceneEnd:          // シーンの終了
                // 処理なし
                break;
        }
    }
}

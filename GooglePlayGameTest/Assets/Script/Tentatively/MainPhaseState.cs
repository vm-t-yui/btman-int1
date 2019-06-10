using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインフェーズ管理クラス
/// FIXME: m.tanaka ループを通すため仮で実装、ゲーム部分とくっつけたら削除します
/// </summary>
public class MainPhaseState : MonoBehaviour
{
    /// <summary>
    /// フェーズの状態
    /// </summary>
    enum PhaseState
    {
        WaitFadeOut,                                 // フェードアウト待機
        PlayMainGame,                                // ゲームプレイ中
        NextScene                                    // 次のシーンへ
    }

    PhaseState nowPhase;                             // フェーズの状態

    [SerializeField]
    DisplayFadeContoller fadeContoller = default;    // フェード管理クラス

    [SerializeField]
    MainGameEnd mainGame = default;                  // メインゲーム終了検知クラス

    [SerializeField]
    NextSceneChanger nextScene = default;            // シーン遷移クラス

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 黒いパネルをフェードアウト
        fadeContoller.OnFade(DisplayFadeContoller.FadeType.FadeOut, DisplayFadeContoller.PanelType.Black);

        nowPhase = PhaseState.WaitFadeOut;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 現在のフェーズの状態により処理を分岐
        switch (nowPhase)
        {
            case PhaseState.WaitFadeOut:       // フェードアウト待機

                // フェードアウトが終わったら次の処理へ
                if (fadeContoller.IsFadeEnd)
                {
                    nowPhase = PhaseState.PlayMainGame;
                }
                break;

            case PhaseState.PlayMainGame:      // ゲームプレイ中

                // メインゲームが終了したら次の処理へ
                if (mainGame.IsEnd)
                {
                    // フェードイン開始
                    fadeContoller.OnFade(DisplayFadeContoller.FadeType.FadeIn, DisplayFadeContoller.PanelType.White);

                    nowPhase = PhaseState.NextScene;
                }
                break;

            case PhaseState.NextScene:         // 次のシーンへ

                // フェードインが終わったら次のシーンへ移行
                if (fadeContoller.IsFadeEnd)
                {
                    // 次に読むシーンをリザルトに設定
                    nextScene.SetNextSceneResult();

                    // シーンをロード
                    nextScene.ChangeNextScene();
                }
                break;
        }
    }
}

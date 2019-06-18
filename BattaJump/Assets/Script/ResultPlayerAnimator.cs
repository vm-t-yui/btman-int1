﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルト時プレイヤーアニメーター管理クラス
/// </summary>
public class ResultPlayerAnimator : MonoBehaviour
{
    [SerializeField]
    Animator animator = default;             // アニメーター

    [SerializeField]
    ScoreCountUp scoreCountUp = default;     // スコアカウントアップクラス

    [SerializeField]
    ScoreDataManager scoreData = default;    // スコアデータクラス

    [SerializeField]
    int GoodScore = 10000;                   // この値を超えたら喜ぶアニメーション再生（定数ですがデバッグ用にシリアライズにしてます）

    bool isEnd = false;                      // 処理終了フラグ

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 処理終了フラグがtrueなら処理を抜ける
        if (isEnd) { return; }

        // スコアのカウントアップが終わったら
        if (scoreCountUp.IsEnd)
        {
            // スコアが指定した値を超えていれば
            if (scoreData.GetNowScore() > GoodScore)
            {
                // 喜ぶアニメーション再生
                animator.SetTrigger("Rejoice");
            }
            else
            {
                // 悲しむアニメーション再生
                animator.SetTrigger("Sad");
            }

            isEnd = true;
        }
    }
}
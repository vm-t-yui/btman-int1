﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコア表示クラス
/// </summary>
public class ScoreDraw : MonoBehaviour
{
    [SerializeField]
    Text scoreText = default;               // テキスト

    [SerializeField]
    ScoreCountUp scoreCountUp = default;    // スコアカウントアップクラス

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // スコアをstringに変換して代入
        scoreText.text = scoreCountUp.countScore.ToString("N0") + " km";
    }
}

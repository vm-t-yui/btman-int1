using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコアクラス
/// </summary>
public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    Text scoreUI = default;   //テキスト

    /// <summary>
    /// スコアUIのセット関数
    /// </summary>
    /// <param name="Score">獲得スコア</param>
    public void SetScoreUI(float Score)
    {
        scoreUI.text = Score.ToString();
    }

    /// <summary>
    /// スコアUIのハイセット関数
    /// </summary>
    /// <param name="highScore">ハイスコア</param>
    public void SetHighScoreUI(float highScore)
    {
        scoreUI.text = highScore.ToString();
    }
}

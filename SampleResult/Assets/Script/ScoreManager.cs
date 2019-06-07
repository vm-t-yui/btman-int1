using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアクラス
/// </summary>
public class ScoreManager
{
    [SerializeField]
    ScoreUI scoreUI = default;   //スコアのUIクラス

    static float score = 0;      //スコア
    static float highScore = 0;  //ハイスコア

    /// <summary>
    /// スコアのセット関数
    /// </summary>
    /// <param name="getScore">獲得スコア</param>
    public void SetHighScore(float getScore)
    {
        highScore = getScore;

        //UIクラスにもハイスコアを送る
        scoreUI.SetHighScoreUI(highScore);
    }

    /// <summary>
    /// スコアのゲット関数
    /// </summary>
    /// <returns>スコア</returns>
    public float GetHighScore()
    {
        if(highScore < score)
        {
            score = highScore;
        }

        return highScore;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルト用スコアカウントアップクラス
/// </summary>
public class ScoreCountUp : MonoBehaviour
{
    [SerializeField]
    ScoreDataManager scoreData = default;               // スコアデータクラス

    public int countScore { get; private set; } = 0;    // カウントアップ用スコア

    [SerializeField]
    int getScore = 1000;                                // ゲーム内で獲得したスコア（デバッグ用にSerializeFiedを設定）

    public bool IsEnd { get; private set; } = false;    // 処理終了フラグ

    /// <summary>
    /// オブジェクト起動時
    /// </summary>
    void OnEnable()
    {
        // 獲得スコアを取得
        getScore = scoreData.GetNowScore();
    }

    /// <summary>
    /// 更新
    /// </summary>
    void FixedUpdate()
    {
        // ゲーム内で獲得したスコアまでカウントアップする
        if (countScore < getScore)
        {
            countScore += (int)(getScore * Time.deltaTime);
        }
        // ゲーム内で獲得したスコアを超えたらカウントアップ終了
        else
        {
            countScore = getScore;
            IsEnd = true;
        }
    }
}

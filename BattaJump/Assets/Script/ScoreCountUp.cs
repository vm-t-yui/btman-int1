using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルト用スコアカウントアップクラス
/// </summary>
public class ScoreCountUp : MonoBehaviour
{
    [SerializeField]
    PlayDataManager playData = default;                      // スコアデータクラス

    [SerializeField]
    RectTransform textRect = default;                        // スコアテキストのRectTransform

    public int countScore { get; private set; } = 0;         // カウントアップ用スコア

    int getScore = 0;                                        // ゲーム内で獲得したスコア（デバッグ用にSerializeFiedを設定）
    const int SpendTime = 4;                                 // カウントアップにかける時間（大体これ+1秒くらいになる）

    float waitTime = 0;                                      // 待機時間計測用
    const float WaitMaxTime = 1.5f;                          // カウント終了後待機時間

    public bool IsCountEnd { get; private set; } = false;    // カウント終了フラグ
    public bool IsEnd { get; private set; } = false;         // 処理終了フラグ

    /// <summary>
    /// オブジェクト起動時
    /// </summary>
    void OnEnable()
    {
        // 獲得スコアを取得
        getScore = playData.GetNowScore();

        // カウントアップ中のドラムロール音を再生
        AudioPlayer.instance.PlaySe(AudioPlayer.SeType.DramRoll);
    }

    /// <summary>
    /// 更新
    /// </summary>
    void FixedUpdate()
    {
        // タップされたら即時カウントアップ終了
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                countScore = getScore;
                textRect.localScale = Vector3.one;
            }
        }

        // ゲーム内で獲得したスコアまでサイズを大きくしながらカウントアップする
        if (countScore < getScore)
        {
            countScore += (int)(getScore * (Time.deltaTime / SpendTime));
            textRect.localScale += Vector3.one * (Time.deltaTime / SpendTime);
        }
        // ゲーム内で獲得したスコアを超えたらカウントアップ終了
        else
        {
            // 待機時間計測
            waitTime += Time.deltaTime;

            countScore = getScore;
            textRect.localScale = Vector3.one;
            if (!IsCountEnd)
            {
                // ドラムロール音を終了する
                AudioPlayer.instance.StopSe(AudioPlayer.SeType.DramRoll);

                // ロール終了サウンドを再生
                AudioPlayer.instance.PlaySe(AudioPlayer.SeType.RollFinish);
                IsCountEnd = true;
            }

            // 指定した時間待機したら
            if (waitTime >= WaitMaxTime)
            {
                // 処理終了
                IsEnd = true;
            }
        }
    }
}

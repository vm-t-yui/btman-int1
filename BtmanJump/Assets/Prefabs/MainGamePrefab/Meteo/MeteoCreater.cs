using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宇宙の隕石生成クラス
/// </summary>
public class MeteoCreater : MonoBehaviour
{
    [SerializeField]
    GameObject[] meteos = default;                        // 使用する隕石のオブジェクト

    [SerializeField]
    AnimationEndChecker[] meteoAnimationEnd = default;    // 隕石のアニメーション終了検知クラス

    [SerializeField]
    JumpHeightCounter jumpHeightCounter = default;        // ジャンプ高さ計測クラス

    float timer = 0f;                                     // 時間計測用変数
    float interval = 0f;                                  // 生成間隔（秒単位）

    [SerializeField]
    float IntervalMin = 0f, IntervalMax = 0f;             // 生成間隔の最大・最小

    [SerializeField]
    Vector3 CreatePosMin = Vector3.zero,                  // 生成位置の最大・最小
            CreatePosMax = Vector3.zero;

    bool isAble = false;                                  // 処理許可フラグ

    int useCount = 0;                                     // 使用している数

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 処理が許可されていなければ処理を抜ける
        if (!isAble) { return; }

        // タイマーをカウント
        timer += Time.deltaTime;
        // 指定された時間に達したら
        if (timer >= interval)
        {
            // 隕石表示
            Create();
        }

        // ジャンプ高さの結果がでたら
        if (jumpHeightCounter.IsJumpHeightResult)
        {
            // プレイヤーとの親子関係を解除する
            transform.parent = null;
        }

        // 画面外に行ったかチェック
        CheckViewOut();
    }

    /// <summary>
    /// 生成開始処理
    /// </summary>
    public void StartCreate()
    {
        // 1つ目の隕石生成
        Create();

        // 処理を許可
        isAble = true;
    }

    /// <summary>
    /// 生成
    /// </summary>
    public void Create()
    {
        // 表示
        meteos[useCount].SetActive(true);

        // 生成位置を指定された範囲からランダムに決定
        meteos[useCount].transform.localPosition = new Vector3(0, Random.Range(CreatePosMin.y, CreatePosMax.y), Random.Range(CreatePosMin.z, CreatePosMax.z));

        timer = 0f;

        // 次の生成間隔を指定された範囲からランダムに決定
        interval = Random.Range(IntervalMin, IntervalMax);

        // 使用数をカウント
        useCount++;

        // 2個とも使用済みになったら使用数を0に戻す
        if (useCount >= 2)
        {
            useCount = 0;
        }
    }

    /// <summary>
    /// 画面外に行ったかチェック
    /// </summary>
    void CheckViewOut()
    {
        for (int i = 0; i < meteos.Length; i++)
        {
            // 表示されていて画面外にあるなら非表示にする
            if (meteos[i] && meteoAnimationEnd[i].IsEnd)
            {
                meteos[i].SetActive(false);
            }
        }
    }
}

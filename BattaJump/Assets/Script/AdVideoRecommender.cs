using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 動画広告勧誘クラス
/// </summary>
public class AdVideoRecommender : MonoBehaviour
{
    [SerializeField]
    AdRewardVideoController rewardVideo = default;            //動画リワード広告クラス

    [SerializeField]
    PlayDataManager playData = default;                       // プレイデータ管理クラス

    [SerializeField]
    GameObject recommendWindow = default;                     // 勧誘用カンバス

    bool isAble = false;                                      // 勧誘許可フラグ
    bool isCancel = false;                                    // キャンセルフラグ
    public bool IsRecommend { get; private set; } = false;    // 勧誘済みフラグ
    public bool IsEnd { get; private set; } = false;          // 処理終了フラグ
    
    const int RecommendCount = 3;                             // 勧誘を行うプレイ回数間隔

    /// <summary>
    /// 初期化
    /// </summary>
    public void Init()
    {
        // プレイ回数が指定した値で割り切れたら
        if (playData.PlayCount > 0 && playData.PlayCount % RecommendCount == 0)
        {
            // 動画広告を生成
            rewardVideo.RequestRewardVideo();

            // 勧誘を許可
            isAble = true;
        }
    }

    /// <summary>
    /// 動画広告勧誘
    /// </summary>
    public void Recommend()
    {
        // 勧誘が許可されているなら
        if (isAble)
        {
            // 専用カンバスを表示
            recommendWindow.SetActive(true);
        }

        // 勧誘済みにする
        IsRecommend = true;
    }

    /// <summary>
    /// 動画広告勧誘ウィンドウ削除
    /// </summary>
    public void Cancel()
    {
        isCancel = true;
        recommendWindow.SetActive(false);
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 勧誘していなければ処理を抜ける
        if (!IsRecommend) { return; }

        // 動画広告を閉じたら処理終了
        if (rewardVideo.IsClosed)
        {
            IsEnd = true;
        }
    }
}

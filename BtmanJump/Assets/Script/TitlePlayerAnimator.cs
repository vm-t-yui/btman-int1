using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトル時プレイヤーアニメーター管理クラス
/// </summary>
public class TitlePlayerAnimator : MonoBehaviour
{
    [SerializeField]
    Animator animator = default;   // アニメーター

    float pointingInterval = 0;    // アニメーション再生間隔

    float countTime = 0f;          // 経過時間

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 再生間隔を８秒から１２秒の間で設定
        pointingInterval = Random.Range(8f, 12f);
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 時間をカウント
        countTime += Time.deltaTime;

        // 指定した時間たったら
        if (countTime >= pointingInterval)
        {
            // 各変数を初期化
            countTime = 0f;
            pointingInterval = Random.Range(5f, 8f);

            // 看板を叩くアニメーション再生
            animator.SetTrigger("Pointing");
        }
    }
}

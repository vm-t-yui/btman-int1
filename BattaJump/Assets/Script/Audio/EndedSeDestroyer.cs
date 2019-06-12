using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 再生が終了したSEオブジェクトの削除処理
/// </summary>
public class EndedSeDestroyer : MonoBehaviour
{
    // 子オブジェクトの最大数
    [SerializeField] int childAmountMax = 0;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 子オブジェクトの最大数を超えたら
        if (transform.childCount > childAmountMax)
        {
            // 再生が終了したSEの子オブジェクトを一斉に削除する
            foreach (Transform endedSeChild in transform)
            {
                Destroy(endedSeChild.gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 再生中のSEのオブジェクトを監視して、親オブジェクトの切り替えを行う
/// </summary>
public class PlayingSeParentSwitcher : MonoBehaviour
{
    // 再生が終了しているSEの親オブジェクトのトランスフォーム
    [SerializeField] Transform parentEndSe = default;
    // 前フレームの子オブジェクト数
    int prevChildCount = 0;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 子オブジェクトの数が前フレームから変動したら
        if (prevChildCount != transform.childCount)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                // 子オブジェクトを取得
                GameObject childSe = transform.GetChild(i).gameObject;

                // 子オブジェクトの中で既に再生が終了しているSEは専用の親オブジェクトに切り替える
                if (!childSe.activeSelf)
                {
                    childSe.transform.SetParent(parentEndSe);
                }
            }
        }

        // 現在の子オブジェクトの数を前フレームとして登録
        prevChildCount = transform.childCount;
    }
}

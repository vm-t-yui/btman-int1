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

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
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
}

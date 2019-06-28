using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 複製された雲の削除クラス
/// </summary>
public class CloudDeleter : MonoBehaviour
{
    bool isView = false;            // 描画範囲に入ったフラグ

    [SerializeField]
    Renderer renderer = default;    // このオブジェクトのレンダラー

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 初めて描画範囲に入ったらフラグを立てる
        if (!isView && renderer.isVisible)
        {
            isView = true;
        }

        // 描画範囲を抜けたらオブジェクト削除
        if (isView && !renderer.isVisible)
        {
            Destroy(gameObject);
        }
    }
}

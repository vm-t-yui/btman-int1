using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パーティクルを1フレームだけ表示させるクラス
/// </summary>
public class ParticleDisplayOnce : MonoBehaviour
{
    int frameCount = 0;    // フレーム数
    
    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 1フレーム以上たったら
        if (frameCount > 0)
        {
            // このクラスをアタッチしているオブジェクトを非表示に
            gameObject.SetActive(false);
            enabled = false;
        }

        // フレーム数をカウント
        frameCount++;
    }
}

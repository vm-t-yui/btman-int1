using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトのセットアクティブ切替クラス
/// NOTE: m.tanaka カンバスなどのアニメーションが終わった際、アニメーションイベントで非表示にするために作成
/// </summary>
public class SetActiveChanger : MonoBehaviour
{
    /// <summary>
    /// trueをセット
    /// </summary>
    void SetActiveTrue()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// falseをセット
    /// </summary>
    void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}

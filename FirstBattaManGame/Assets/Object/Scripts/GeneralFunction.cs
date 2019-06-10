using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 他のクラスで扱う補助関数をまとめたクラス
/// </summary>
public class GeneralFuncion
{
    /// <summary>
    /// 全ての子オブジェクトにSetActiveを行う
    /// </summary>
    /// <param name="parentTransform">親オブジェクトのトランスフォーム</param>
    /// <param name="value">セットするフラグ</param>
    static public void SetActiveFromAllChild(Transform parentTransform, bool value)
    {
        foreach (Transform child in parentTransform)
        {
            child.gameObject.SetActive(value);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 看板の設置後のカンバス表示
/// </summary>
public class CanvasActive : MonoBehaviour
{
    [SerializeField]
    GameObject dirtSmoke = default;    //土煙

    [SerializeField]
    GameObject canvas = default;       //カンバス

    /// <summary>
    /// カンバス表示
    /// </summary>
    void DisplayCanvas()
    {
        dirtSmoke.SetActive(false);
        canvas.SetActive(true);
    }
}

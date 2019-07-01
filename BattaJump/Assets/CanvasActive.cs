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
    Animator canvasAnim = default;     //カンバスのアニメーター

    /// <summary>
    /// カンバス表示
    /// </summary>
    void DisplayCanvas()
    {
        dirtSmoke.SetActive(false);

        //カンバス表示用アニメーション再生
        canvasAnim.SetTrigger("FadeIn");
    }
}

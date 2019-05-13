using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カンバスの「InputTestCanvas」のオブジェクトクラス
public class InputTestCanvas : MonoBehaviour
{
    // カンバスのゲームオブジェクト
    public static GameObject canvasGameObject;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // カンバスのゲームオブジェクトを取得
        canvasGameObject = GameObject.Find("InputTestCanvas");
    }
}

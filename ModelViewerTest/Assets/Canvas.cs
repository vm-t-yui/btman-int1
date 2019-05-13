using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    // カンバスのゲームオブジェクト
    public static GameObject canvas;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // カンバスのゲームオブジェクトを取得
        canvas = GameObject.Find("Canvas");
    }
}

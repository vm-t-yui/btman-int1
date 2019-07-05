using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// FPS表示フラグ
/// </summary>
public class FpsDraw : MonoBehaviour
{
    [SerializeField]
    Text fpsText = default;

    int frameCount = 0;

    float nextTime = 0;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
#if DEVELOPMENT_BUILD
        nextTime = Time.time + 1;
#else
        gameObject.SetActive(false);
#endif
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        frameCount++;

        if (Time.time >= nextTime)
        {
            fpsText.text = frameCount.ToString() + "fps";

            frameCount = 0;
            nextTime++;
        }
    }
    }

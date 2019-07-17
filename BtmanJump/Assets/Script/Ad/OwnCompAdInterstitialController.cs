using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

/// <summary>
/// 
/// </summary>
public class OwnCompAdInterstitialController : MonoBehaviour
{
    [SerializeField]
    GameObject[] adImages = default;

    [SerializeField]
    PlayDataManager playData = default;

    const string UseCountKey = "UseCount";

    int useCount = 0;

    /// <summary>
    /// 
    /// </summary>
    void OnEnable()
    {
        useCount = PlayerPrefs.GetInt(UseCountKey, 0);

        adImages[useCount].SetActive(true);

        useCount++;
        if (useCount > 3)
        {
            useCount = 0;
        }
        PlayerPrefs.SetInt(UseCountKey, useCount);
        PlayerPrefs.Save();
    }
}

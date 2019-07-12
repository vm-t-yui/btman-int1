using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class URLConnecter : MonoBehaviour
{
    [SerializeField]
    string AndroidURL = default;
    [SerializeField]
    string IosURL = default;

    /// <summary>
    /// タンを押した時のメソッド
    /// </summary>
    public void PushLinkButton()
    {
        string url =
#if UNITY_ANDROID
        AndroidURL;
#elif UNITY_IOS
        IosURL;
#else
        "unexpected_platform";
#endif

        Application.OpenURL(url);
    }
}

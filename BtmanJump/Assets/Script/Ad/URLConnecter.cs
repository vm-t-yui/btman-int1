using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// URLアクセスクラス
/// </summary>
public class URLConnecter : MonoBehaviour
{
    [SerializeField]
    string AndroidURL = default;    //アンドロイド用URL
    [SerializeField]
    string IosURL = default;        //iOS用URL

	/// <summary>
	/// ボタンを押した時のメソッド
	/// </summary>
	public void PushLinkButton()
    {
        string url =
#if UNITY_ANDROID
        AndroidURL;                 //アンドロイド用
#elif UNITY_IOS
        IosURL;                     //iOS用
#else
		"unexpected_platform";      //それ以外
#endif

        //URLにアクセス
        Application.OpenURL(url);
    }
}

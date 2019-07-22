using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 以下の3つはGooglePlayGamesの機能を使用するためのもの
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

/// <summary>
/// サインイン管理クラス
/// </summary>
public class SignInController : MonoBehaviour
{
    public bool IsSignIn { get; private set; }    // サインインしているかどうか

    const int MaxSignInTryCount = 20;             // 最大サインイン処理実行回数
    int signInTryCount = 0;                       // サインイン処理実行回数

    /// <summary>
    /// 開始
    /// </summary>
    void Awake()
    {
#if UNITY_ANDROID
        // PlayGames初期化
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
#endif

        IsSignIn = false;

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            // サインイン実行
            SignIn();
        }
    }

    /// <summary>
    /// 「GooglePlay」、「GameCenter」へのサインイン
    /// </summary>
    void SignIn()
    {
        // サインイン処理
        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                // サインイン成功！
                IsSignIn = true;
            }
        });
    }
}
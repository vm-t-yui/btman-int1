using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 以下の3つはGooglePlayGamesの機能を使用するためのもの
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

#if UNITY_ANDROID
/// <summary>
/// GoogleService機能管理クラス
/// </summary>
public class GoogleServiceManager : MonoBehaviour
{
    public bool IsSignIn { get; private set; }    // サインインしているかどうか

    const int MaxSignInTryCount = 20;             // 最大サインイン処理実行回数
    int signInTryCount = 0;                       // サインイン処理実行回数

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 初期化
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        IsSignIn = false;

        // サインイン実行
        SignIn();
    }

    /// <summary>
    /// GooglePlayへのサインイン処理
    /// </summary>
    void SignIn()
    {
        Social.localUser.Authenticate((bool success) => {

            if (success)
            {
                // サインイン成功！
                IsSignIn = true;
            }
            // サインインに失敗して、処理実行回数が最大に達していないなら再度実行
            else if (signInTryCount < MaxSignInTryCount)
            {
                SignIn();
            }
        });

        // サインイン処理実行回数をカウント
        signInTryCount++;
    }
}
#endif
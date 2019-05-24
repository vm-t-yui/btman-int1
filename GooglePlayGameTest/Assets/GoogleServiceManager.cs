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
            else
            {
                // 失敗したらもう一度
                SignIn();
            }
        });
    }
}
#endif
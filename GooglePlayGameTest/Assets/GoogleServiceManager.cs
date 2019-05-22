using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

#if UNITY_ANDROID
/// <summary>
/// GoogleService機能管理クラス
/// </summary>
public class GoogleServiceManager : MonoBehaviour
{
    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 初期化
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

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
            }
        });
    }
}
#endif
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
                // TODO: m.tanaka テスト用の実績（初ログイン時）の解除処理が未実装
            }
        });
    }
}
#endif
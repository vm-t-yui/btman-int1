using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 共有機能クラス
/// </summary>
public class ShareOtherApplication : MonoBehaviour
{
    [SerializeField]
    LocalizeController localizeController = default;    //ローカライズクラス

    /// <summary>
    /// 共有
    /// </summary>
    public void Share()
    {
        StartCoroutine(_Share());
    }

    /// <summary>
    /// 共有処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator _Share()
    {
        // 画像のパスを取得
        string imgPath = Application.persistentDataPath + "/image.png";

        // 前回のデータを削除
        File.Delete(imgPath);

        //スクリーンショットを撮影
        ScreenCapture.CaptureScreenshot("image.png");

        // 撮影画像の保存が完了するまで待機
        while (true)
        {
            if (File.Exists(imgPath)) break;
            yield return null;
        }

        string tweetText = null;   //共有するテキスト
        string tweetURL = null;    //共有するURL

        //ローカライズされた言語が日本語なら
        if (localizeController.GetLanguageNum() == (int)LocalizeScriptableObject.LocalizeLanguage.Japanese)
        {
            tweetText = "君も一緒にバッタージャンプ！！";
        }
        //それ以外
        else
        {
            tweetText = "Let's BATTA JUMP together！！";
        }

        //IOSならAppStore、それ以外ならGooglePlayのURL
#if UNITY_IOS
        tweetURL = "https://itunes.apple.com/jp/app/id1464833025?mt=8";
#else
        tweetURL = "https://play.google.com/store/apps/details?id=com.vikingmaxx.btmanJump";
#endif

        // 投稿する
        SocialConnector.SocialConnector.Share(tweetText, tweetURL, imgPath);
    }
}

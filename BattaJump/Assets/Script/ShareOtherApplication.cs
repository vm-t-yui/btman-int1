using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 共有機能クラス
/// </summary>
public class ShareOtherApplication : MonoBehaviour
{
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

        // 投稿する
        string tweetText = "患者の運命は....をえが変える！！！！";
        string tweetURL = "https://www.google.com/?hl=ja";
        SocialConnector.SocialConnector.Share(tweetText, tweetURL, imgPath);
    }
}

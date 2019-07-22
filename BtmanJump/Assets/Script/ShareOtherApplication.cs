using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// 共有機能クラス
/// </summary>
public class ShareOtherApplication : MonoBehaviour
{
    [SerializeField]
    LocalizeController localizeController = default;    //ローカライズクラス

    [SerializeField]
    GameObject[] speechBubble = default;                //吹き出し
    [SerializeField]
    PlayDataManager playDataManager = default;          //プレイデータ管理クラス

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // 今のスコアがハイスコアの7割以上だったら共有促しの吹き出し表示
        if (playDataManager.GetNowScore() >= playDataManager.HighScore * 0.7f && playDataManager.HighScore > 0)
        {
            // ローカライズされた言語が日本語なら
            if (localizeController.GetLanguageNum() == (int)LocalizeScriptableObject.LocalizeLanguage.Japanese)
            {
                speechBubble[0].SetActive(true);
            }
            // それ以外
            else
            {
                speechBubble[1].SetActive(true);
            }
        }
    }

    /// <summary>
    /// 共有
    /// </summary>
    public void Share()
    {
        speechBubble[0].SetActive(false);
        speechBubble[1].SetActive(false);
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
            if (SceneManager.GetActiveScene().name == "Title")
            {
                tweetText = "君も一緒にバッタージャンプ！！";
            }
            else
            {
                tweetText = playDataManager.GetNowScore().ToString("N0") + "km跳んだぞ！" + " 君も一緒にバッタージャンプ！！";
            }
        }
        //それ以外
        else
        {
            if (SceneManager.GetActiveScene().name == "Title")
            {
                tweetText = "Let's BATTA JUMP together！！";
            }
            else
            {
                tweetText = "I jumped " + playDataManager.GetNowScore().ToString("N0") + "km!" + " Let's BATTA JUMP together！！";
            }
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

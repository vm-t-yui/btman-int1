using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

/// <summary>
/// 各ボタンのスプライトセットクラス
/// </summary>
public class ButtonSpriteSetter : MonoBehaviour
{
    [SerializeField]
    Image title = default;                     // タイトルの画像

    [SerializeField]
    Image howToPlay = default;                 // やり方説明の画像

    [SerializeField]
    Image leaderboardButtonImage = default;    // リーダーボード表示ボタンの画像

    [SerializeField]
    Image achievementButtonImage = default;    // 実績表示ボタンの画像

    [SerializeField]
    Image itemViewButtonImage = default;       // アイテム欄表示ボタンの画像

    [SerializeField]
    Image shareButtonImage = default;          // 共有ボタンの画像

    [SerializeField]
    Image settingButtonImage = default;        // 設定画面表示ボタンの画像

    [SerializeField]
    Image titleLogo = default;                 // 設定画面木の看板の画像

    [SerializeField]
    Image[] newItemDisplay = default;            // 設定画面木の看板の画像

    [SerializeField]
    Image[] volume = default;                  // 設定画面木の看板の画像

    [SerializeField]
    Image[] woodenButten = default;            // 設定画面木の看板の画像

    [SerializeField]
    Image[] woodenFrame = default;             // 設定画面木の看板の画像

    [SerializeField]
    Image[] woodenOutFrame = default;          // 設定画面木の看板の画像

    [SerializeField]
    SpriteAtlas canvasAtlas = default;         // カンバス用のスプライト

    /// <summary>
    /// 開始
    /// </summary>
    void Awake()
    {
        // 各ボタン画像のスプライトをセット
        leaderboardButtonImage.sprite = canvasAtlas.GetSprite("leaderboard");

        achievementButtonImage.sprite = canvasAtlas.GetSprite("achievement");

        itemViewButtonImage.sprite = canvasAtlas.GetSprite("itemView");

        shareButtonImage.sprite = canvasAtlas.GetSprite("share");

        // 看板はある分だけセット
        foreach (var sign in woodenButten)
        {
            sign.sprite = canvasAtlas.GetSprite("woodButton");
        }

        // 看板はある分だけセット
        foreach (var display in newItemDisplay)
        {
            display.sprite = canvasAtlas.GetSprite("ItemSpeechBubble");
        }

        // フレームもある分だけセット
        foreach (var frame in woodenFrame)
        {
            frame.sprite = canvasAtlas.GetSprite("woodFrame");
        }

        // フレームもある分だけセット
        foreach (var outFrame in woodenOutFrame)
        {
            outFrame.sprite = canvasAtlas.GetSprite("woodOutFrame");
        }

        // リザルトではタイトルはないので、defaultじゃない場合のみスプライトをセット
        if (title != default)
        {
            title.sprite = canvasAtlas.GetSprite("btmanLogo");
        }

        // リザルトではやり方説明表示しないので、defaultじゃない場合のみスプライトをセット
        if (howToPlay != default)
        {
            howToPlay.sprite = canvasAtlas.GetSprite("HowToPlay");
        }

        // リザルトではオプションはないので、defaultじゃない場合のみスプライトをセット
        if (settingButtonImage != default)
        {
            settingButtonImage.sprite = canvasAtlas.GetSprite("setting");
        }

        // リザルトでは音量設定はないので、defaultじゃない場合のみスプライトをセット
        if (volume != default)
        {
            // 音量はある分だけセット
            foreach (var vol in volume)
            {
                vol.sprite = canvasAtlas.GetSprite("volume");
            }
        }

        // リザルトではおすすめアプリ表示はないので、defaultじゃない場合のみスプライトをセット
        if (titleLogo != default)
        {
            titleLogo.sprite = canvasAtlas.GetSprite("vikingmaxxLogo");
        }
    }
}

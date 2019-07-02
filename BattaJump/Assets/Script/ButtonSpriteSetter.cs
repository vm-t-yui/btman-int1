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
    Image[] volume = default;                 // 設定画面木の看板の画像

    [SerializeField]
    Image[] woodenButten = default;              // 設定画面木の看板の画像

    [SerializeField]
    Image[] woodenFrame = default;              // 設定画面木の看板の画像

    [SerializeField]
    Image[] woodenOutFrame = default;              // 設定画面木の看板の画像

    [SerializeField]
    SpriteAtlas buttonAtlas;                   // ボタン用のスプライト

    /// <summary>
    /// 開始
    /// </summary>
    void Awake()
    {
        // 各ボタン画像のスプライトをセット
        leaderboardButtonImage.sprite = buttonAtlas.GetSprite("leaderboard");

        achievementButtonImage.sprite = buttonAtlas.GetSprite("achievement");

        itemViewButtonImage.sprite = buttonAtlas.GetSprite("itemView");

        shareButtonImage.sprite = buttonAtlas.GetSprite("share");

        titleLogo.sprite = buttonAtlas.GetSprite("vikingmaxxLogo");

        foreach (var vol in volume)
        {
            vol.sprite = buttonAtlas.GetSprite("volume");
        }

        // 看板はある分だけセット
        foreach (var sign in woodenButten)
        {
            sign.sprite = buttonAtlas.GetSprite("woodButton");
        }

        // フレームもある分だけセット
        foreach (var frame in woodenFrame)
        {
            frame.sprite = buttonAtlas.GetSprite("woodenFrame");
        }

        // フレームもある分だけセット
        foreach (var outFrame in woodenOutFrame)
        {
            outFrame.sprite = buttonAtlas.GetSprite("woodenOutFrame");
        }

        // リザルトでは設定画面表示ボタンはないので、defaultじゃない場合のみスプライトをセット
        if (settingButtonImage != default)
        {
            settingButtonImage.sprite = buttonAtlas.GetSprite("setting");
        }
    }
}

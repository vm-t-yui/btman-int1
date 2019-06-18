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

        // リザルトでは設定画面表示ボタンはないので、defaultじゃない場合のみスプライトをセット
        if (settingButtonImage != default)
        {
            settingButtonImage.sprite = buttonAtlas.GetSprite("setting");
        }
    }
}

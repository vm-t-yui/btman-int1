using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

/// <summary>
/// 自社広告コントロールクラス
/// </summary>
public class OwnCompAdInterstitialController : MonoBehaviour
{
    [SerializeField]
    GameObject[] adImages = default;          // 広告画像オブジェクト

    const int OwnCompAdNum = 3;               // 自社広告の総数

    int useAdNum = 0;                         // 使用する広告番号
    const string UseAdNumKey = "UseAdNum";    // 使用する広告番号のデータキー


    /// <summary>
    /// 起動時
    /// </summary>
    void OnEnable()
    {
        // 使用する広告番号を取得
        useAdNum = PlayerPrefs.GetInt(UseAdNumKey, 0);

        // 取得した広告番号に応じた自社広告を表示
        adImages[useAdNum].SetActive(true);

        // 次回使用する広告番号をセットしてセーブ
        useAdNum++;
        if (useAdNum > OwnCompAdNum)
        {
            useAdNum = 0;
        }
        PlayerPrefs.SetInt(UseAdNumKey, useAdNum);
        PlayerPrefs.Save();
    }
}

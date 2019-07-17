using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NendUnityPlugin.AD;
using NendUnityPlugin.Common;

/// <summary>
/// nendインタースティシャル広告コントロールクラス
/// </summary>
public class NendInterstitialController : MonoBehaviour
{
    [SerializeField]
    NendAdInterstitial nendAdInterstitial = default;

    const string ApiKey =    // APIキー
#if UNITY_ANDROID
        "34789a78a8b123a244ca12a6197b5cca5e945689";
#elif UNITY_IOS
        "7099efd9e94efe0ff932a1f4ffd410ee5264af54";
#else
        "unexpected_platform";
#endif

    const string SpotID =    // スポットID
#if UNITY_ANDROID
        "964517";
#elif UNITY_IOS
        "964518";
#else
        "unexpected_platform";
#endif

    public bool IsLoaded { get; private set; } = false;    // 広告ロード完了フラグ

    /// <summary>
    /// 広告ロード
    /// </summary>
    public void Load()
    {
        nendAdInterstitial.Load(ApiKey, SpotID);

        nendAdInterstitial.AdLoaded += OnAdLoaded;
    }

    /// <summary>
    /// 広告表示
    /// </summary>
    public void Show()
    {
        nendAdInterstitial.Show();
    }

    /// <summary>
    /// ロード完了時コールバック
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    void OnAdLoaded(object sender, NendAdInterstitialLoadEventArgs args)
    {
        IsLoaded = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スカイボックス切り替えクラス
/// </summary>
public class SkyboxChanger : MonoBehaviour
{
    [SerializeField]
    JumpHeightCounter jumpHeightCounter = default;    // ジャンプ高さ計測クラス

    [SerializeField]
    CloudCreater cloudCreater = default;

    [SerializeField]
    Material universeSkyboxMat;                       // 宇宙のスカイボックスのマテリアル

    [SerializeField]
    GameObject rocketObj = default;                   // ロケットのオブジェクト

    [SerializeField]
    int ReachUniverseHeight;            // 宇宙に達する高さ

    const float RocketHideDisplayHeight = -1.2f;      // ロケットが画面を覆い隠す高さ（ロケットのローカル座標）

    bool isChange = false;                            // 変更完了フラグ

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // スカイボックスを切替済みなら処理を抜ける
        if (isChange) { return; }

        // ジャンプの高さが指定した値まで達したら
        if (jumpHeightCounter.JumpHeightToKiloMetre > ReachUniverseHeight)
        {
            // ロケットを出現させる
            rocketObj.SetActive(true);
        }

        // ロケットが画面を覆い隠したら
        if (rocketObj.transform.localPosition.y > RocketHideDisplayHeight && !isChange)
        {
            // スカイボックス切り替え
            RenderSettings.skybox = universeSkyboxMat;

            cloudCreater.enabled = false;

            isChange = true;
        }
    }
}

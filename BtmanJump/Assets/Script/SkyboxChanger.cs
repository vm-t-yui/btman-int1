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
    Transform playerPos = default;                    // プレイヤー

    [SerializeField]
    CloudCreater cloudCreater = default;              // 雲生成クラス

    [SerializeField]
    MeteoCreater meteoCreater = default;              // 隕石生成クラス

    [SerializeField]
    Material universeSkyboxMat = default;             // 宇宙のスカイボックスのマテリアル

    [SerializeField]
    SkyboxRotater skyboxRotater = default;            // スカイボックス回転処理クラス

    [SerializeField]
    GameObject rocketObj = default;                   // ロケットのオブジェクト

    [SerializeField]
    int ReachUniverseHeight = 0;                      // 宇宙に達する高さ

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
        if (playerPos.position.y >= ReachUniverseHeight)
        {
            // ロケットを出現させる
            rocketObj.SetActive(true);

            // 雲の生成を停止
            cloudCreater.enabled = false;
        }

        // ロケットが画面を覆い隠したら
        if (rocketObj.transform.localPosition.y > RocketHideDisplayHeight && !isChange)
        {
            // スカイボックス切り替え
            RenderSettings.skybox = universeSkyboxMat;

            // 隕石の生成開始
            meteoCreater.StartCreate();

            // 回転させるスカイボックスマテリアルを宇宙のものに変更
            skyboxRotater.OnChangeUniverse();

            isChange = true;
        }
    }
}

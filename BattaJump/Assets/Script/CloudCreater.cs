using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 雲生成クラス
/// </summary>
public class CloudCreater : MonoBehaviour
{
    [SerializeField]
    GameObject cloud = default;                                  // 複製する雲

    float interval = 0f;                                         // 生成する間隔

    Vector3 createPos = Vector3.zero;                            // 生成時の位置
    Vector3 createScale = Vector3.zero;                          // 生成時のサイズ
    readonly Vector3 CreateRotation = new Vector3(0, -20, 0);    // 生成時回転角

    [SerializeField]
    Vector3 CreateMinPos, CreateMaxPos;                          // 生成時の位置の最大・最小

    [SerializeField]
    Vector3 CreateScaleMin, CreateScaleMax;                      // 生成時のサイズの最大・最小

    [SerializeField]
    float intervalMin, intervalMax;                              // 生成間隔の最大・最小

    [SerializeField]
    Transform playerPos = default;                               // プレイヤーの位置

    Vector3 prevCreatePos = Vector3.zero;                        // 前に雲を生成した位置

    bool isAble = false;                                         // 処理許可フラグ

    /// <summary>
    /// 起動時処理
    /// </summary>
    void OnEnable()
    {
        // 1つ目の雲生成
        Create();

        // 処理を許可
        isAble = true;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 処理が許可されていなければ処理を抜ける
        if (!isAble) { return; }

        // 前の生成位置からプレイヤーまでの距離が指定した値を超えたら
        if (Vector3.Distance(prevCreatePos, playerPos.transform.position) >= interval)
        {
            // 雲を生成
            Create();
        }
    }

    /// <summary>
    /// 雲生成
    /// </summary>
    void Create()
    {
        // 生成位置・生成サイズをランダムに決定
        createPos = new Vector3(Random.Range(CreateMinPos.x, CreateMaxPos.x), playerPos.position.y + Random.Range(CreateMinPos.y, CreateMaxPos.y), Random.Range(CreateMinPos.z, CreateMaxPos.z));
        createScale = new Vector3(Random.Range(3f, 5), Random.Range(3, 4f), Random.Range(1, 2));

        // 指定された位置・回転角で複製
        GameObject newCloud = Instantiate(cloud, createPos, Quaternion.Euler(CreateRotation));

        // 指定されたサイズに更新
        newCloud.transform.localScale = createScale;

        // 次の生成間隔をランダムに決定
        interval = Random.Range(intervalMin, intervalMax);

        // 前の生成位置を更新
        prevCreatePos = playerPos.transform.position;
    }
}

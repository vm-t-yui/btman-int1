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

    List<GameObject> cloudClones = new List<GameObject>();       // 複製した雲のリスト

    const int CloneNum = 5;                                      // 複製する最大数
    float interval = 0f;                                         // 生成する間隔
    int useCount = 0;

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
    /// 開始
    /// </summary>
    void Start()
    {
        // 先に使う分だけ複製して非表示にしておく
        for (int i = 0; i < CloneNum; i++)
        {
            GameObject newCloud = Instantiate(cloud, Vector3.zero, Quaternion.Euler(CreateRotation));
            newCloud.transform.parent = transform;
            newCloud.SetActive(false);
            cloudClones.Add(newCloud);
        }
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

        // 画面外に行ったかチェック
        CheckViewOut();
    }

    /// <summary>
    /// 終了時処理
    /// </summary>
    void OnDisable()
    {
        // 全ての雲を非表示にする
        for (int i = 0; i < CloneNum; i++)
        {
            cloudClones[i].SetActive(false);
        }
    }

    /// <summary>
    /// 生成開始処理
    /// </summary>
    public void StartCreate()
    {
        // 1つ目の雲生成
        Create();

        // 処理を許可
        isAble = true;
    }

    /// <summary>
    /// 雲生成
    /// </summary>
    void Create()
    {
        // 生成位置・生成サイズをランダムに決定
        createPos = new Vector3(Random.Range(CreateMinPos.x, CreateMaxPos.x), playerPos.position.y + Random.Range(CreateMinPos.y, CreateMaxPos.y), Random.Range(CreateMinPos.z, CreateMaxPos.z));
        createScale = new Vector3(Random.Range(3f, 5), Random.Range(3, 4f), Random.Range(1, 2));

        // 指定された位置・サイズに更新
        cloudClones[useCount].transform.position = createPos;
        cloudClones[useCount].transform.localScale = createScale;

        cloudClones[useCount].SetActive(true);

        // 次の生成間隔をランダムに決定
        interval = Random.Range(intervalMin, intervalMax);

        // 前の生成位置を更新
        prevCreatePos = playerPos.transform.position;

        // 使用数をカウント
        useCount++;

        // 5個使用済みになったら使用数を0に戻す
        if (useCount >= CloneNum)
        {
            useCount = 0;
        }
    }

    /// <summary>
    /// 画面外に行ったかチェック
    /// </summary>
    void CheckViewOut()
    {
        for (int i = 0; i < CloneNum; i++)
        {
            // 表示されていて画面外にあるなら非表示にする
            if (cloudClones[i] && cloudClones[i].transform.position.y < playerPos.position.y - 10)
            {
                cloudClones[i].SetActive(false);
            }
        }
    }
}

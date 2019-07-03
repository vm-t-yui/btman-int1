using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジャンプ時のクレーター生成クラス
/// </summary>
public class CraterCreater : MonoBehaviour
{
    [SerializeField]
    GameObject[] debris;                                         // 破片のオブジェクト

    [SerializeField]
    GameObject crater = default;                                 // クレーターのオブジェクト

    [SerializeField]
    GameObject takeOffParticle = default;                        // ジャンプの瞬間のパーティクル

    const int DebrisNum = 10;                                    // 生成する破片の総数

    GameObject[] debriss = new GameObject[DebrisNum];            // 生成した破片を入れる配列

    Rigidbody[] debrisRigit = new Rigidbody[DebrisNum];          // 生成した破片のRigitBody用配列

    float moveTime = 0;                                          // 破片が動いている間カウントする変数
    const float MoveMaxTime = 2;                                 // 破片を動かす秒数

    bool isCreate = false;                                       // 生成フラグ

    readonly Vector3 DebrisBornPos = new Vector3(0, 0.2f, 0);    // 破片の初期位置

    [SerializeField]
    Vector3 VelocityMin, VelocityMax;                            // velocityの各値の最大・最小

    [SerializeField]
    int RotateForceMagMin, RotateForceMagMax;                    // 回転する力の倍率の最大・最小

    /// <summary>
    /// 破片の初期化
    /// </summary>
    void InitDebris()
    {
        for (int i = 0; i < DebrisNum; i++)
        {
            // 座標を初期位置にする
            debriss[i].transform.position = DebrisBornPos;
            // velocityをゼロにする
            debrisRigit[i].velocity = Vector3.zero;
            // 向きを初期化
            debrisRigit[i].rotation = Quaternion.Euler(Vector3.zero);
            // 非表示にする
            debriss[i].SetActive(false);
        }
    }

    /// <summary>
    /// クレーター生成
    /// </summary>
    public void Create()
    {
        Vector3 direction;    // 向き

        for (int i = 0; i < DebrisNum; i++)
        {
            // 破片を表示
            debriss[i].SetActive(true);
            // velocityを指定した範囲からランダムに決定
            debrisRigit[i].velocity = new Vector3(Random.Range(VelocityMin.x, VelocityMax.x),
                                                  Random.Range(VelocityMin.y, VelocityMax.y),
                                                  Random.Range(VelocityMin.z, VelocityMax.z));
            // velocityから向きベクトルを取得
            direction = Vector3.Normalize(debrisRigit[i].velocity);
            // 向きを更新
            debrisRigit[i].rotation = Quaternion.Euler(direction);
            // 回転する力を加える（倍率は指定した値からランダムに決定）
            debrisRigit[i].AddTorque(direction * Random.Range(RotateForceMagMin, RotateForceMagMax));
        }

        // クレーターを表示
        crater.SetActive(true);

        // パーティクルを表示
        takeOffParticle.SetActive(true);

        // クレーター爆発音を再生
        AudioPlayer.instance.PlaySe(AudioPlayer.SeType.CraterExplosion);

        isCreate = true;
    }

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 指定した個数破片を生成
        for (int i = 0; i < DebrisNum; i++)
        {
            // 配列に入っている破片の中からランダムに生成
            debriss[i] = Instantiate(debris[Random.Range(0, debris.Length)]);
            debriss[i].transform.parent = transform;
            // RigitBodyを取得
            debrisRigit[i] = debriss[i].GetComponent<Rigidbody>();
        }

        // 破片の初期化
        InitDebris();
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 生成フラグがfalseなら処理を抜ける
        if (!isCreate) { return; }

        // 移動時間をカウント
        moveTime += Time.deltaTime;

        // 移動時間が指定した時間までいったら
        if (moveTime >= MoveMaxTime)
        {
            // 破片の初期化
            InitDebris();

            moveTime = 0;

            // クレーターを非表示
            crater.SetActive(false);
            // パーティクルを非表示
            takeOffParticle.SetActive(false);

            isCreate = false;
        }
    }
}

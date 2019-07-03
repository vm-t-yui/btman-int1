using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スカイボックス回転クラス
/// </summary>
public class SkyboxRotater : MonoBehaviour
{
    [SerializeField]
    Material[] skyboxMats = default;    // 回転させるマテリアル

    [SerializeField]
    float RotateSpeed;                  // 回転速度

    float rotateValue = 0f;             // 最終的にマテリアルに適応させる値

    bool isUniverse = false;            // 宇宙に到達しているかどうか

    /// <summary>
    /// 宇宙に入ったら呼ぶ
    /// </summary>
    public void ChageUniverse()
    {
        isUniverse = true;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 宇宙のスカイボックス回転処理
        if (isUniverse)
        {
            RotateMat(skyboxMats[1]);
        }
        // 空のスカイボックス回転処理
        else
        {
            RotateMat(skyboxMats[0]);
        }
    }

    /// <summary>
    /// 回転処理
    /// </summary>
    /// <param name="mat">回転させるマテリアル</param>
    void RotateMat(Material mat)
    {
        // 回転させる値を計算
        rotateValue = Mathf.Repeat(mat.GetFloat("_Rotation") + RotateSpeed, 360f);

        // Rotationを更新
        mat.SetFloat("_Rotation", rotateValue);
    }
}

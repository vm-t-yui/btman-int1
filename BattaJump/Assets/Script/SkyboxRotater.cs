using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotater : MonoBehaviour
{
    [SerializeField]
    Material skyboxMat = default;    // 回転させるマテリアル

    [SerializeField]
    float RotateSpeed;               // 回転速度

    float rotateValue = 0f;          // 最終的にマテリアルに適応させる値

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 回転させる値を計算
        rotateValue = Mathf.Repeat(skyboxMat.GetFloat("_Rotation") + RotateSpeed, 360f);

        // Rotationを更新
        skyboxMat.SetFloat("_Rotation", rotateValue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム出現管理クラス(親にアタッチ)
/// </summary>
public class ItemAppearance : MonoBehaviour
{
    /// <summary>
    /// 範囲に入ると表示アイテム(子)を表示
    /// </summary>
    /// <param name="collision">プレイヤー</param>
    void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 最初は非表示
    /// </summary>
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 場所番号のセット関数
    /// </summary>
    /// <param name="pos">ポジション</param>
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}

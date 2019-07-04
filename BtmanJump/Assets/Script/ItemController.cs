using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム制御クラス(子にアタッチ)
/// </summary>
public class ItemController : MonoBehaviour
{
    [SerializeField]
    GameObject player = default;

    [SerializeField]
    int myNum;                  //自分のアイテム番号

    bool isCollider = false;    //当たり判定フラグ

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        //当たり判定開始
        isCollider = true;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        //常にプレイヤーと同じx,z軸で、カメラの方に向かせる
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));
    }

    /// <summary>
    /// 範囲に入るとアイテム取得(非表示)
    /// </summary>
    /// <param name="other">プレイヤー</param>
    void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player" && isCollider)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 自分の番号のセット関数
    /// </summary>
    /// <param name="num">Number.</param>
    public void SetMyNum(int num)
    {
        myNum = num;
    }

    /// <summary>
    /// 自分の番号のゲット関数
    /// </summary>
    public int GetMyNum()
    {
        return myNum;
    }

    /// <summary>
    /// プレイヤーオブジェクトのセット関数
    /// </summary>
    /// <param name="playerObj">Player object.</param>
    public void SetPlayer(GameObject playerObj)
    {
        player = playerObj;
    }
}
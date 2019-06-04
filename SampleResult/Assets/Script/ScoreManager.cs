using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアクラス
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    float score = 0;    //スコア

    [SerializeField]
    DataManager dataManager;        //データクラス

    public void SendData()
    {
        //スコアをデータ管理クラスに送る
        dataManager.SetHighScore(score);
    }
}

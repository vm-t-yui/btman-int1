using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコアクラス
/// </summary>
public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    Text text;                      //テキスト

    [SerializeField]
    DataManager dataManager;        //データクラス

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        //点数を更新
        text.text = dataManager.GetHighScore().ToString();
    }
}

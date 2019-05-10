using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputLog : MonoBehaviour
{
    // テキストのコンポーネント
    Text log;

    // タッチ入力情報
    Touch touchState0;     // １本目の指
    Touch touchState1;     // ２本目の指

    // 初期化
    void Start()
    {
        // テキストのコンポーネントを取得
        log = GetComponent<Text>();
    }

    // 更新
    void Update()
    {
        // 入力がない場合は、ログを表示してそれ以降の処理をスキップする
        if (Input.touchCount <= 0)
        {
            // 入力がないことを表すログを表示
            log.text = "nonInput";
            // スキップ
            return;
        }

        // 以下、入力があった場合の処理

        // タッチされている数を表示
        log.text = "touchNum : " + Input.touchCount.ToString();

        // 入力状態の取得
        touchState0 = Input.GetTouch(0);    // １本目の指
        touchState1 = Input.GetTouch(1);    // ２本目の指
    }
}

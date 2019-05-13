using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 入力関連のテストプログラムのクラス
public class InputTest: MonoBehaviour
{
    // タッチされている位置を表す画像のソース
    [SerializeField] GameObject sourceTouchPosImage = null;

    // 画像を格納するリスト
    List<GameObject> touchPosImages = new List<GameObject>();

    // テキストのコンポーネント
    Text log;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // テキストのコンポーネントを取得
        log = GetComponent<Text>();
    }

   /// <summary>
   /// 更新
   /// </summary>
    void Update()
    {
        // リスト内の画像を全て削除
        DestroyAllTouchPosImage();

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

        // タッチされた位置を表す画像を描画
        DrawTouchPosImage();
    }
   
    /// <summary>
    /// タップした位置に画像を描画
    /// </summary>
    void DrawTouchPosImage()
    {
        // リストの要素を全削除
        touchPosImages.Clear();

        // タッチされている数を取得
        int touchNum = Input.touchCount;

        // タッチされている数だけ処理を行う
        for (int i = 0; i < touchNum; i++)
        {
            // タッチされている箇所のそれぞれの入力情報を取得
            Touch touchInfo = Input.GetTouch(i);

            // 取得した情報をもとに、位置を表す画像を複製する
            GameObject tmpImage = Instantiate(sourceTouchPosImage,                          // 複製元のソース画像
                                              touchInfo.position,                           // 初期位置
                                              Quaternion.Euler(0,0,0),                      // 初期回転角
                                              InputTestCanvas.canvasGameObject.transform);  // 紐づける親オブジェクト（親：カンバス）

            // 複製したオブジェクトをリストに加える
            touchPosImages.Add(tmpImage);
        }
    }

    /// <summary>
    /// 残っている全ての画像を削除
    /// </summary>
    void DestroyAllTouchPosImage()
    {
        touchPosImages.ForEach(element => Destroy(element));
    }
}

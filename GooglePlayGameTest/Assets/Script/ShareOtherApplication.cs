using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 共有機能クラス
/// </summary>
public class ShareOtherApplication : MonoBehaviour
{
    /// <summary>
    /// 共有処理（共有先のアプリを選択するウィンドウが表示される）
    /// </summary>
    public void Share()
    {
        // Share("送るテキスト", "URL", "画像のURL")
        SocialConnector.SocialConnector.Share("ShareTest");
    }
}

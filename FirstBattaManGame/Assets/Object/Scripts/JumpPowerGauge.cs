using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのジャンプ力を表したゲージ
/// </summary>
public class JumpPowerGauge : MonoBehaviour
{
    [SerializeField] Image gaugeImage;                // ゲージのイメージ
    [SerializeField] Text  magnificationText;         // 倍率のテキストUI 

    public float currentAmount { get; set; } = 0;     // ゲージの現在の量
    const  float GaugeAmountMax              = 100;   // ゲージの最大量
    public int   gaugeMagnification          = 0;     // ゲージの倍率


    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // ゲージのUIの処理
        if (currentAmount != 0)
        {
            // ゲージの現在の量からパーセントを算出（百分率：current / max * 100）
            float amountToParcent = currentAmount / GaugeAmountMax * 100.0f;
            // 算出したパーセントを０～１に丸め込む（0.4 = 40% * 0.01）
            gaugeImage.fillAmount = amountToParcent * 0.01f;
        }

        // ゲージが上限を超えたら
        if (currentAmount > GaugeAmountMax)
        {
            // 倍率を増やす
            gaugeMagnification++;
            // ゲージをリセット
            currentAmount = 0;
        }
        // ゲージが無くなったら
        else if (currentAmount < 0)
        {
            // 倍率が０でなければ
            if (gaugeMagnification != 0)
            {
                // 倍率を減らす
                gaugeMagnification--;
                // ゲージを最大に設定
                currentAmount = GaugeAmountMax;
            }
        }

        // 倍率をUIに反映
        magnificationText.text = "×" + gaugeMagnification.ToString("f0");
    }
}

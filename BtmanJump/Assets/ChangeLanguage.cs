using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ドロップダウンで言語変更
/// </summary>
public class ChangeLanguage : MonoBehaviour
{
    //Dropdownを格納する変数
    [SerializeField] Dropdown dropdown = default;

    [SerializeField] Text label = default;

    //ローカライズクラス
    [SerializeField] LocalizeController localizeController = default;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // セーブしてあった言語にドロップダウンを合わせる
        dropdown.value = localizeController.GetLanguageNum();
        Change();
    }

    // オプションが変更されたときに言語変更
    public void Change()
    {
        localizeController.LanguageNumLocalize(dropdown.value);

        label.text = localizeController.GetLocalizeText(dropdown.value);

        for (int i = 0; i < dropdown.options.Count; i++)
        {
            dropdown.options[i].text = localizeController.GetLocalizeText(i);
        }
    }
}

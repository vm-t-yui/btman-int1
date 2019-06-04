using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オーディオソースクラス
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// サウンドの番号
    /// </summary>
    enum SoundNum
    {
        //BGMが先、SEが後の順番でサウンドをセットしてくれるとやりやすい
        SampleBGM1,        //サンプルBGM1
        SampleBGM2,        //サンプルBGM2
        SampleBGM3,        //サンプルBGM3
        SampleSE1,         //サンプルSE1
        SampleSE2,         //サンプルSE2
        SampleSE3,         //サンプルSE3
    }

    [SerializeField] GameObject sourceObj = default;        //生成元のオーディオソースオブジェクト
    List<GameObject> audioObj = new List<GameObject>();     //生成後のオーディオソースオブジェクト管理リスト
    AudioClip[] audioClips;     //サウンド

    int maxBGMNum = 0;          //BGMの最小番号
    int minBGMNum = 0;          //BGMの最大番号

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        //サウンドの数だけオーディオソースをもったオブジェクトを生成
        for (int i = 0; i < audioClips.Length; i++)
        {
            audioObj.Add(Instantiate(sourceObj, transform.parent));
            audioObj[i].GetComponent<AudioSource>().clip = audioClips[i];
        }

        //BGMの最小番号、最大番号設定
        minBGMNum = (int)SoundNum.SampleBGM1;
        maxBGMNum = (int)SoundNum.SampleBGM3;
    }

    /// <summary>
    /// サウンド再生
    /// </summary>
    /// <param name="audioNum">サウンドの番号</param>
    void SoundPlay(int audioNum)
    {
        //BGM停止処理
        StopBGM(audioNum);

        //再生
        audioObj[audioNum].GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// BGM停止処理
    /// </summary>
    /// <param name="audioNum">サウンドの番号</param>
    void StopBGM(int audioNum)
    {
        //まず今から流すサウンドがBGMかどうか確かめる
        if (CheackBGM(audioNum))
        {
            //BGMが流れるなら既に流れているBGMを止める
            SoundStop(GetPlayBGMNum());
        }
    }

    /// <summary>
    /// サウンドストップ
    /// </summary>
    /// <param name="audioNum">サウンドの番号</param>
    void SoundStop(int audioNum)
    {
        audioObj[audioNum].GetComponent<AudioSource>().Stop();
    }

    /// <summary>
    /// 引数のサウンドがBGMなのかどうか
    /// </summary>
    /// <returns> BGMである <c>true</c>, 引数のサウンドがBGMなのかどうか <c>false</c> BGMではない </returns>
    /// <param name="audioNum">サウンドの番号</param>
    bool CheackBGM(int audioNum)
    {
        bool flag = false;   //return用のbool

        //サウンド番号がBGMの番号内なら
        if(audioNum >= minBGMNum && audioNum <= maxBGMNum)
        {
            flag = true;
        }

        return flag;
    }

    /// <summary>
    /// 今なんのBGMが流れているか
    /// </summary>
    /// <returns> 今流れているBGMの番号 </returns>
    int GetPlayBGMNum()
    {
        int bgmNum = 0;    //return用のint

        //BGMの数分だけ回して、今再生されているBGMの番号を取得する
        for (int i = minBGMNum; i < maxBGMNum; i++)
        {
            if(audioObj[i].GetComponent<AudioSource>().isPlaying)
            {
                bgmNum = i;
            }
        }

        return bgmNum;
    }
}

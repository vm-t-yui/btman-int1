using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オーディオ再生クラス
/// </summary>
public class AudioPlayer : MonoBehaviour
{
    /// <summary>
    /// BGMの種類
    /// </summary>
    public enum BgmType
    {
        Title,    // タイトル
        Jumping,  // プレイヤージャンプ
        Result,   // リザルト
    }

    /// <summary>
    /// SEの種類
    /// </summary>
    public enum SeType
    {
        SelectButton,   // 決定
        CancelButton,   // キャンセル
        PanelOpen,      // パネル開く
        PanelClose,     // パネル閉じる
        BirdTwitter,    // 鳥のさえずり
        JumpChargeing,  // ジャンプ溜め
        ChargeingTap1,  // チャージ中のタップ音１
        ChargeingTap2,  // チャージ中のタップ音２
        JumpVoice,      // ジャンプボイス
        CraterExplosion,// クレーター爆発
        WindNoise,      // 風切り
        FallingCry,     // 落下時の叫び声
        DramRoll,       // ドラムロール
        RollFinish,     // ロール終わり
        Cheers,         // 歓声
        Tin,            // チーン
        ItemGet,        // アイテム取得
    }

    // シングルトンインスタンス
    static public AudioPlayer instance = null;

    [SerializeField] Transform parentBgm            = default;     // BGMの親オブジェクトのトランスフォーム
    [SerializeField] Transform parentSe             = default;     // SEの親オブジェクトのトランスフォーム
    [SerializeField] Transform parentPlayingSe      = default;     // 再生中のSEの親オブジェクトのトランスフォーム

    // BGMが再生中かどうか
    bool isPlayingBgm = false;

    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        if (null == instance)
        {
            // 自分自身をインスタンスとして渡す
            instance = this;
            // シーンが切り替わってもインスタンスが破棄されないように設定
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// BGMの再生
    /// </summary>
    /// <param name="type">再生するBGMの種類</param>
    public void PlayBgm(BgmType type)
    {
        // BGMが既に再生中（フラグが立っていれば）であれば停止する
        if (isPlayingBgm)
        {
            // 全てBGMに対してSetActiveを行いBGMを停止させる
            GeneralFuncion.SetActiveFromAllChild(parentBgm, false);
            // フラグを倒す
            isPlayingBgm = false;
        }

        // 指定のBGMを再生する
        parentBgm.GetChild((int)type).gameObject.SetActive(true);
        // フラグを起こす
        isPlayingBgm = true;
    }

    /// <summary>
    /// BGMを停止
    /// </summary>
    public void StopBgm()
    {
        if (isPlayingBgm)
        {
            // 全てBGMに対してSetActiveを行いBGMを停止させる
            GeneralFuncion.SetActiveFromAllChild(parentBgm, false);
            // フラグを倒す
            isPlayingBgm = false;
        }
    }

    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="type">再生するSEの種類</param>
    public void PlaySe(SeType type)
    {
        // 再生するSEのオブジェクトを複製する（再生中のSEを持つ親オブジェクトにセット）
        GameObject playingSe = Instantiate(parentSe.GetChild((int)type).gameObject, parentPlayingSe);
        // 再生するSEのオブジェクトをオンにする
        playingSe.SetActive(true);
    }

    /// <summary>
    /// 指定のSEを停止する
    /// </summary>
    /// <param name="type">停止するSEの種類</param>
    public void StopSe(SeType type)
    {
        // 再生中のSEで指定されたSEと同じものは停止する
        foreach (Transform playingSeChild in parentPlayingSe)
        {
            // オブジェクト名とEnumの種類名で判定（クローンされたオブジェクトの名前には"(Clone)"が付加される）
            if (playingSeChild.gameObject.name == type.ToString() + "(Clone)")
            {
                // 名前が一致したSEは停止する
                playingSeChild.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 全てのSEを停止する
    /// </summary>
    public void StopAllSe()
    {
        // 全ての再生中のSEに対してSetActiveを行う
        GeneralFuncion.SetActiveFromAllChild(parentPlayingSe, false);
    }

    /// <summary>
    /// 指定のBGMが再生中か判定する
    /// </summary>
    /// <param name="type">判定するBGMの種類</param>
    /// <returns></returns>
    public bool IsPlayingBgm(BgmType type)
    {
        // BGMのオブジェクトがアクティブであれば再生中
        return parentBgm.GetChild((int)type).gameObject.activeSelf;
    }

    /// <summary>
    /// 指定のSEが再生中か判断する
    /// </summary>
    /// <param name="type">判定するSEの種類</param>
    /// <returns>再生中かどうか返す</returns>
    public bool IsPlayingSe(SeType type)
    {
        // 再生中のSEで指定されたSEと同じものは停止する
        foreach (Transform playingSeChild in parentPlayingSe)
        {
            // オブジェクト名とEnumの種類名で判定（クローンされたオブジェクトの名前には"(Clone)"が付加される）
            if (playingSeChild.gameObject.name == type.ToString() + "(Clone)")
            {
                // オブジェクトがアクティブかどうかも判定する。
                // NOTE : 再生が終了したSEが子オブジェクトとして残ったままになっている場合があるため
                if (playingSeChild.gameObject.activeSelf)
                {
                    // 名前が一致した場合はtrueを返す
                    return true;
                }
            }
        }
        // 無ければfalseを返す
        return false;
    }
}

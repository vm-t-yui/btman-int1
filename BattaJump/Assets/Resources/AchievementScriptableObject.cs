using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(menuName = "DataObject/Create AchievementScriptableObject", fileName = "AchievementScriptableObjects")]
public class AchievementScriptableObject : ScriptableObject
{
    static readonly string ResourcePath = "AchievementScriptableObjects";    //リソースのパス

    static AchievementScriptableObject staticInstance = null;               //実績用のScriptableObjectクラス

    //リソース内のScriptableObjectロード
    public static AchievementScriptableObject LoadResources()
    {
        return Resources.Load(ResourcePath) as AchievementScriptableObject;
    }

    //ScriptableObjectインスタンス取得
    public static AchievementScriptableObject Instance
    {
        get
        {
            if (staticInstance == null)
            {
                var asset = LoadResources();

                if (asset == null)
                {
                    asset = CreateInstance<AchievementScriptableObject>();
                }
                
                staticInstance = asset;
            }

            return staticInstance;
        }
    }

    //↓こっから実績用のScriptableObjectの要素

    /// <summary>
    /// 実績の種類
    /// </summary>
    public enum AchievementType
    {
        FirstJump = 0,
        ReachedUniverse,
        ConqueredSolarSystem,
        NotJump,
        JumpedFifteen,
        JumpedThirty,
        ItemComplete,
    }

    public const int AchievementNum = 7;    // 実績の総数

    [SerializeField]
    string[] androidAchievementIDs = new string[AchievementNum];         // Android用実績ID
    [SerializeField]
    string[] iosAchievementIDs = new string[AchievementNum];             // IOS用実績ID

    [SerializeField]
    int firstJumpNum;                       // 初ジャンプの値
    [SerializeField]
    int reachedUniverseNum;                 // 宇宙到達の値
    [SerializeField]
    int conqueredSolarSystemNum;            // 太陽系制覇の値
    [SerializeField]
    int notJumpNum;                         // 開始後、放置の値
    [SerializeField]
    int jumpedFifteenNum;                   // 15回ジャンプの値
    [SerializeField]
    int jumpedThirtyNum;                    // 30回ジャンプの値
    [SerializeField]
    int itemCompleteNum;                    // アイテムコンプリートの値

    /// <summary>
    /// Android用実績IDゲット
    /// </summary>
    /// <returns></returns>
    public string[] GetAndroidIDs()
    {
        return androidAchievementIDs;
    }

    /// <summary>
    /// IOS用実績IDゲット
    /// </summary>
    /// <returns></returns>
    public string[] GetIosIDs()
    {
        return iosAchievementIDs;
    }

    /// <summary>
    /// 初ジャンプの値ゲット
    /// </summary>
    /// <returns></returns>
    public int GetFirstJumpNum()
    {
        return firstJumpNum;
    }

    /// <summary>
    /// 宇宙到達の値ゲット
    /// </summary>
    /// <returns></returns>
    public int GetReachedUniverseNum()
    {
        return reachedUniverseNum;
    }

    /// <summary>
    /// 太陽系制覇の値ゲット
    /// </summary>
    /// <returns></returns>
    public int GetConqueredSolarSystemNum()
    {
        return conqueredSolarSystemNum;
    }

    /// <summary>
    /// 開始後、放置の値ゲット
    /// </summary>
    /// <returns></returns>
    public int GetNotJumpNum()
    {
        return notJumpNum;
    }

    /// <summary>
    /// 15回ジャンプの値ゲット
    /// </summary>
    /// <returns></returns>
    public int GetJumpedFifteenNum()
    {
        return jumpedFifteenNum;
    }

    /// <summary>
    /// // 30回ジャンプの値ゲット
    /// </summary>
    /// <returns></returns>
    public int GetJumpedThirtyNum()
    {
        return jumpedThirtyNum;
    }

    /// <summary>
    /// アイテムコンプリートの値ゲット
    /// </summary>
    /// <returns></returns>
    public int GetItemCompleteNum()
    {
        return itemCompleteNum;
    }
}

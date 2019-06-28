using UnityEngine;
using System.Collections;

/// <summary>
/// パーティクルの終了検知
/// </summary>
public class DisableParticle : MonoBehaviour
{
    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        StartCoroutine(ParticleWorking());
    }

    /// <summary>
    /// パーティクルが終了したら非表示
    /// </summary>
    IEnumerator ParticleWorking()
    {
        var particle = GetComponent<ParticleSystem>();

        yield return new WaitWhile(() => particle.IsAlive(true));

        gameObject.SetActive(false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class MainSpriteSetter : MonoBehaviour
{
    [SerializeField]
    Image gage = default;

    [SerializeField]
    Image gageZero = default;

    [SerializeField]
    SpriteAtlas canvasAtlas = default;

    void Awake()
    {
        gage.sprite = canvasAtlas.GetSprite("timer");

        gageZero.sprite = canvasAtlas.GetSprite("timerZero");
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// アウトラインクラス
/// </summary>
public class CircleOutline : BaseMeshEffect
{
    [SerializeField]
    Color effectColor = new Color(255, 255, 255, 255);  //アウトラインの色

    [SerializeField]
    float effectDistance = 1.5f;                      //アウトラインをつける文字からの距離(離れればその分だけアウトラインが太くなる)

    [SerializeField]
    int effectNumber = 10;                            //アウトラインの数(増えれば増えるほど綺麗になるが重くなる)

    //NOTE:配布コードのためコメント省略
    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive())
            return;

        List<UIVertex> list = new List<UIVertex>();
        vh.GetUIVertexStream(list);

        ModifyVertices(list);

        vh.Clear();
        vh.AddUIVertexTriangleStream(list);
    }

    //NOTE:配布コードのためコメント省略
    void ModifyVertices(List<UIVertex> verts)
    {
        int start = 0;
        int end = verts.Count;

        for (int n = 0; n < effectNumber; ++n)
        {
            float rad = 2.0f * Mathf.PI * n / effectNumber;
            float x = effectDistance * Mathf.Cos(rad);
            float y = effectDistance * Mathf.Sin(rad);

            ApplyShadow(verts, start, end, x, y);

            start = end;
            end = verts.Count;
        }
    }

    //NOTE:配布コードのためコメント省略
    void ApplyShadow(List<UIVertex> verts, int start, int end, float x, float y)
    {
        UIVertex vt;

        for (int i = start; i < end; ++i)
        {
            vt = verts[i];
            verts.Add(vt);

            Vector3 v = vt.position;
            v.x += x;
            v.y += y;
            vt.position = v;
            Color32 newColor = effectColor;
            vt.color = newColor;
            verts[i] = vt;
        }
    }
}

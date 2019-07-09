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
    private Color m_EffectColor = new Color(255, 255, 255, 0);  //アウトラインの色

    [SerializeField]
    private float m_EffectDistance = 1.5f;                      //アウトラインをつける文字からの距離(離れればその分だけアウトラインが太くなる)

    [SerializeField]
    private int m_nEffectNumber = 10;                           //アウトラインの数(増えれば増えるほど綺麗になるが重くなる)

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

        for (int n = 0; n < m_nEffectNumber; ++n)
        {
            float rad = 2.0f * Mathf.PI * n / m_nEffectNumber;
            float x = m_EffectDistance * Mathf.Cos(rad);
            float y = m_EffectDistance * Mathf.Sin(rad);

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
            Color32 newColor = m_EffectColor;
            vt.color = newColor;
            verts[i] = vt;
        }
    }
}

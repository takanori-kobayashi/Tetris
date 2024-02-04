using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クリア時の文字ブロックの色
/// </summary>
public class CongratulationsColor : MonoBehaviour
{
    public int m_firstIndex;

    private int m_MyIndex;

    private int m_Count;

    private readonly byte[,] COLOR_TBL = new byte[16, 3]
    {
        {47 ,202,253},
        {253,47,248},
        {250,240,50},
        {47,57,253},
        {254,166,46},
        {62,242,57},
        {255,0,0},
        {254,166,46},
        {47 ,202,253},
        {253,47,248},
        {250,240,50},
        {47,57,253},
        {254,166,46},
        {62,242,57},
        {255,0,0},
        {254,166,46},
    };

    // Start is called before the first frame update
    void Start()
    {
        m_MyIndex = m_firstIndex;
        m_Count = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color32(COLOR_TBL[m_MyIndex, 0], COLOR_TBL[m_MyIndex, 1], COLOR_TBL[m_MyIndex, 2], 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        m_Count++;
        if (20 <= m_Count)
        {
            m_Count = 0;


            //m_MyIndex++;
            //
            //if (16 <= m_MyIndex)
            //{
            //    m_MyIndex = 0;
            //}
            if (0 < m_MyIndex)
            {
                m_MyIndex--;
            }
            else
            {
                m_MyIndex = 15;
            }


            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color32(COLOR_TBL[m_MyIndex,0], COLOR_TBL[m_MyIndex, 1], COLOR_TBL[m_MyIndex, 2], 255);
            }

        }

    }
}

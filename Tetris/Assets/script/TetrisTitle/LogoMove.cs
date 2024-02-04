using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ロゴの文字の動作
/// </summary>
public class LogoMove : MonoBehaviour
{
    public static int m_order;

    public int m_MyOrdrNum;

    private bool m_sw;

    private int m_Ycount;

    private int m_Rcount;

    public byte m_r;
    public byte m_g;
    public byte m_b;

    // Start is called before the first frame update
    void Start()
    {
        m_sw = false;

        m_Ycount = 0;
        m_Rcount = 0;


        //byte r;
        //byte g;
        //byte b;

        //switch (m_NowMino)
        //{
        //    case I_MINO:
        //        r = 47;
        //        g = 202;
        //        b = 253;
        //        break;
        //    case T_MINO:
        //        r = 253;
        //        g = 47;
        //        b = 248;
        //        break;
        //    case O_MINO:
        //        r = 250;
        //        g = 240;
        //        b = 50;
        //        break;
        //    case J_MINO:
        //        r = 47;
        //        g = 57;
        //        b = 253;
        //        break;
        //    case L_MINO:
        //        r = 254;
        //        g = 166;
        //        b = 46;
        //        break;
        //    case S_MINO:
        //        r = 62;
        //        g = 242;
        //        b = 57;
        //        break;
        //    case Z_MINO:
        //        r = 255;
        //        g = 0;
        //        b = 0;
        //        break;
        //    default:
        //        r = 100;
        //        g = 100;
        //        b = 100;
        //        break;
        //}

        for (int i = 0; i < transform.childCount; i++)
        {
            //transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color32(m_r, m_g, m_b, 255);
            transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color32(m_r, m_g, m_b, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (m_MyOrdrNum == 3 && m_order == 6)          
        {
            transform.Rotate(0.0f, 10.0f, 0.0f);

            m_Rcount++;
            if(35 < m_Rcount)
            {
                m_order = 0;
                m_Rcount = 0;
            }
        }


        if (m_MyOrdrNum == m_order)
        {


            if (m_sw == false)
            {
                transform.Translate(0.0f, 0.1f, 0.0f);
            }
            else
            {
                transform.Translate(0.0f, -0.1f, 0.0f);
            }

            m_Ycount++;
            if (10 <= m_Ycount)
            {
                m_Ycount = 0;
                m_sw ^= true;

                if(m_sw==false)
                {
                    m_order++;

                    //if( <= m_order)
                    //{
                     //   m_order = 0;
                    //}
                }
            }
        }
    }
}

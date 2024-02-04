using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クリア時の文字ブロックの動作
/// </summary>
public class CongratulationsMove : MonoBehaviour
{
    public static int m_order;

    public int m_MyOrdrNum;

    private bool m_sw;

    private int m_Ycount;


    // Start is called before the first frame update
    void Start()
    {
        m_sw = false;

        m_Ycount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
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

                if (m_sw == false)
                {
                    m_order++;

                    if(16 <= m_order)
                    {
                        m_order = 0;
                    }
                }
            }
        }
    }
}

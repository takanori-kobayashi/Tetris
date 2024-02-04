using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クリア時の花火の表示
/// </summary>
public class HanabiDisp : MonoBehaviour
{
    private int m_delay;

    private byte m_cnt;

    // Start is called before the first frame update
    void Start()
    {
        //開始時は非表示
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        m_cnt = 0;

        m_delay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ///クリア時表示
        if (Move.m_OperationState == Move.OperationState.OP_STATE_CLEAR)
        {
            if(200 <= m_delay && m_cnt == 2)
            {
                transform.GetChild(2).gameObject.SetActive(true);
                //サウンド再生(花火)
                Sound.PlaySE(Sound.SE.HANABI, 0.2f);
                m_cnt++;
            }

            else if (100 <= m_delay && m_cnt == 1)
            {
                transform.GetChild(1).gameObject.SetActive(true);
                //サウンド再生(花火)
                Sound.PlaySE(Sound.SE.HANABI, 0.2f);
                m_cnt++;
            }
            else if(0 <= m_delay && m_cnt == 0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                //サウンド再生(花火)
                Sound.PlaySE(Sound.SE.HANABI, 0.2f);
                m_cnt++;
            }

            if (m_delay <= 200)
            {
                m_delay++;
            }
        }
    }
}

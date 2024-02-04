using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スタート時の文字ブロック
/// </summary>
public class StartCharBlock : MonoBehaviour
{
    public byte m_r;
    public byte m_g;
    public byte m_b;

    public bool m_Ready;

    int m_CmnCount;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color32(m_r, m_g, m_b, 255);
        }

        if(m_Ready == false)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
            }
        }

        m_CmnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (m_Ready == true)
        {
            if (Move.m_OperationState != Move.OperationState.OP_STATE_START && Move.m_OperationState != Move.OperationState.OP_STATE_STARTUP)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
                }
            }
        }
        else
        {
            if (Move.m_OperationState == Move.OperationState.OP_STATE_PLAY && m_CmnCount <= 30)
            {
                m_CmnCount++;
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;
                }
            }

            else if (Move.m_OperationState == Move.OperationState.OP_STATE_PLAY && 30 <= m_CmnCount)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
                }
            }

        }
    }
}

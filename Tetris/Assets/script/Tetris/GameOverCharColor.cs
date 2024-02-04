using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームオーバー時の文字ブロックの色と動作
/// </summary>
public class GameOverCharColor : MonoBehaviour
{
    public byte m_r;
    public byte m_g;
    public byte m_b;

    private Vector3 m_defpos;

    bool sw = false;

    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color32(m_r, m_g, m_b, 255);
        }

        m_defpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 postmp = m_defpos;

        if(sw == false)
        {
            postmp.x += Random.Range(0.0f, 0.1f);
            postmp.y += Random.Range(0.0f, 0.1f);
        }
        else
        {
            postmp.x -= Random.Range(0.0f, 0.1f);
            postmp.y -= Random.Range(0.0f, 0.1f);
        }

        count++;
        if(7 <= count)
        {
            count = 0;
            sw ^= true;
        }

        transform.position = postmp;
    }
}

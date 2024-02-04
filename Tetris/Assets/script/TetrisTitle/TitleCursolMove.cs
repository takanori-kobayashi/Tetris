using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルのカーソルの動作
/// </summary>
public class TitleCursolMove : MonoBehaviour
{
    /// <summary>
    /// スタート表示
    /// </summary>
    public GameObject m_start;

    /// <summary>
    /// 終了表示
    /// </summary>
    public GameObject m_exit;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Renderer>().material.color = new Color32(253, 47, 248, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 pos;

        transform.Rotate(0.0f, 2.5f, 0.0f);

        switch(TitleOperation.m_Cursol)
        {
            case TitleOperation.CURSOL_START:
                pos = transform.position;
                pos.y = m_start.transform.position.y;
                transform.position = pos;
                break;
            case TitleOperation.CURSOL_EXIT:
                pos = transform.position;
                pos.y = m_exit.transform.position.y;
                transform.position = pos;
                break;
            default:
                break;
        }
    }
}

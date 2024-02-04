using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/// <summary>
/// HOLDの文字色変更
/// </summary>
public class HoldColor : MonoBehaviour
{
    /// <summary>
    /// テキストオブジェクト
    /// </summary>
    public GameObject m_Hold_object = null;

    /// <summary>
    /// テキストクラス
    /// </summary>
    Text m_Hold_text;

    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトからTextコンポーネントを取得
        m_Hold_text = m_Hold_object.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(FallBlock.m_HoldFlg == true)
        {
            m_Hold_text.color = Color.red;
        }
        else
        {
            m_Hold_text.color = Color.black;
        }
    }
}

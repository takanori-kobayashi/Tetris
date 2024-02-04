using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/// <summary>
/// (UI)ラインカウント
/// </summary>
public class LineCount : MonoBehaviour
{
    /// <summary>
    /// テキストオブジェクト
    /// </summary>
    public GameObject m_LineCount_object = null;

    /// <summary>
    /// テキストクラス
    /// </summary>
    Text m_LineCount_text;

    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトからTextコンポーネントを取得
        m_LineCount_text = m_LineCount_object.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //三桁
        var strtmp = Move.m_EraseLineCount.ToString("D3");
        m_LineCount_text.text = "LINE\n"+ strtmp + "/" + Move.MAX_LINE_COUNT;
    }
}

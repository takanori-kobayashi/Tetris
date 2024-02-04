using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/// <summary>
/// (UI)スコア加算
/// </summary>
public class ScoreCount : MonoBehaviour
{
    /// <summary>
    /// テキストオブジェクト
    /// </summary>
    public GameObject m_ScoreCount_object = null;

    /// <summary>
    /// テキストクラス
    /// </summary>
    Text m_ScoreCount_text;

    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトからTextコンポーネントを取得
        m_ScoreCount_text = m_ScoreCount_object.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //三桁
        var strtmp = Move.m_Score.ToString("D6");
        m_ScoreCount_text.text = "SCORE\n" + strtmp;
    }
}

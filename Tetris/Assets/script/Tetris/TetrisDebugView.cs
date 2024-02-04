using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // 追加しましょう

public class TetrisDebugView : MonoBehaviour
{
    public GameObject m_TextObject = null; // Textオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        //Text score_text = m_TextObject.GetComponent<Text>();
        // テキストの表示を入れ替える
       // score_text.text = "000000\naaaa";
    }

    public void SetText(string str)
    {
        // オブジェクトからTextコンポーネントを取得
        Text score_text = m_TextObject.GetComponent<Text>();
        // テキストの表示を入れ替える
        score_text.text = str;
    }
}

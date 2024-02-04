using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TetrisDebugText : MonoBehaviour
{
    public GameObject m_TextObject = null; // Textオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        //transform.Translate(0.0f, 0.0f, 0.0f, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string str,float x,float y)
    {
        // オブジェクトからTextコンポーネントを取得
        Text score_text = m_TextObject.GetComponent<Text>();
        // テキストの表示を入れ替える
        score_text.text = str;

        //transform.Translate(x,y, 0.0f, Space.World);
        transform.position = new Vector3(x, y, 0.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クリア時の文字ブロックの表示
/// </summary>
public class CongratulationsDisp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //開始時は非表示
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
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
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}

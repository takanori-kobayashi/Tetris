using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// (UI)ポーズ時
/// </summary>
public class Pause : MonoBehaviour
{
    /// <summary>
    /// ポーズ時の色
    /// </summary>
    Color PauseOnClor;

    /// <summary>
    /// 非ポーズ時の色
    /// </summary>
    Color PauseOffClor;

    // Start is called before the first frame update
    void Start()
    {
        PauseOnClor = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        PauseOffClor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        this.gameObject.GetComponent<Image>().color = PauseOffClor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Move.m_OperationState == Move.OperationState.OP_STATE_PAUSE)
        {
            this.gameObject.GetComponent<Image>().color = PauseOnClor;
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = PauseOffClor;
        }
    }

    private void FixedUpdate()
    {

    }
}

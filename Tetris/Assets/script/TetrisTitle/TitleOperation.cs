using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトルの操作
/// </summary>
public class TitleOperation : MonoBehaviour
{
    /// <summary>
    /// カーソル(スタート)
    /// </summary>
    public const int CURSOL_START = 0;

    /// <summary>
    /// カーソル(終了)
    /// </summary>
    public const int CURSOL_EXIT = 1;

    /// <summary>
    /// カーソルの位置
    /// </summary>
    public static int m_Cursol;

    /// <summary>
    /// 水平方向の入力
    /// </summary>
    float m_inputHorizontal;

    /// <summary>
    /// 垂直方向の入力
    /// </summary>
    float m_inputVertical;

    /// <summary>
    /// ボタンが押されいているかどうか
    /// </summary>
    bool m_push;

    // Start is called before the first frame update
    void Start()
    {
        m_Cursol = CURSOL_START;

        //ロゴの動作の順番クリア
        LogoMove.m_order = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //コントローラーやキーボードの入力時
        m_inputHorizontal = Input.GetAxisRaw("Horizontal");
        m_inputVertical = Input.GetAxisRaw("Vertical");

        EspKeyQuit();
    }

    private void FixedUpdate()
    {
        switch (m_Cursol)
        {
            case CURSOL_START:
                GameStart();
                break;
            case CURSOL_EXIT:
                ExitGame();
                break;
        }

        if (m_push == false && m_inputVertical != 0 && (m_inputVertical <= -0.5f || 0.5f <= m_inputVertical))
        {
            m_push = true;

            if (m_Cursol < CURSOL_EXIT)
            {
                m_Cursol++;
            }
            else
            {
                m_Cursol = 0;
            }
        }
        else if (m_inputVertical == 0)
        {
            m_push = false;
        }

    }

    private void GameStart()
    {
        //ゲーム開始
        if (Input.GetAxisRaw("Rotate") == 1)
        {
            SceneManager.LoadScene("Tetris");
        }

    }

    private void ExitGame()
    {
        if (Input.GetAxisRaw("Rotate") == 1)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
        }
    }

    /// <summary>
    /// ゲーム終了(Escキー押下)
    /// </summary>
    private void EspKeyQuit()
    {
        //if (Input.GetKeyDown("q"))
        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
        }
    }

}

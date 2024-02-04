using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンド再生
/// </summary>
public class Sound : MonoBehaviour
{
    public enum SE
    {
        MINO_SET,
        MINO_ERASE,
        HANABI,
        HOLD,
    };

    /// <summary>
    /// SE
    /// </summary>
    public AudioClip[] m_SetSe;
    private static AudioClip[] m_Se;


    /// <summary>
    /// AudioSourceクラス
    /// </summary>
    public static AudioSource m_audioSource;


    // Start is called before the first frame update
    void Start()
    {
        //Componentを取得
        m_audioSource = GetComponent<AudioSource>();

        m_Se = new AudioClip[m_SetSe.Length];
        for (int i=0; i<m_SetSe.Length; i++)
        {
            m_Se[i] = m_SetSe[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// SE再生
    /// </summary>
    /// <param name="SeNum">SEの番号</param>
    /// <param name="volumeScale">ボリューム</param>
    public static void PlaySE(SE SeNum,float volumeScale = 0.2f)
    {
        int tmp = (int)SeNum;
        //再生
        if (m_audioSource != null && m_Se[tmp] != null)
        {
            m_audioSource.PlayOneShot(m_Se[tmp], volumeScale);
        }
    }

    private void FixedUpdate()
    {

    }
}

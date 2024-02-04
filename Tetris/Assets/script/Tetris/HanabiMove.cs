using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クリア時の花火の動作
/// </summary>
public class HanabiMove : MonoBehaviour
{
    private ParticleSystem m_particul;
    private ParticleSystem.MainModule m_particulMain;

    public float m_XposMin;
    public float m_XposMAX;

    public float m_YposMin;
    public float m_YposMAX;

    // Start is called before the first frame update
    void Start()
    {
        m_particulMain = GetComponent<ParticleSystem>().main;
        m_particul = GetComponent<ParticleSystem>();


        int color = Random.Range(0, 5);
        switch (color)
        {
            case 0:
                m_particulMain.startColor = new Color(1, 0, 0, 0.7f);
                break;
            case 1:
                m_particulMain.startColor = new Color(1, 1, 0, 0.7f);
                break;
            case 2:
                m_particulMain.startColor = new Color(0.4f, 0.9f, 1.0f, 0.7f);
                break;
            case 3:
                m_particulMain.startColor = new Color(0.9f, 0.2f, 0.9f, 0.7f);
                break;
            case 4:
                m_particulMain.startColor = new Color(0.4f, 0.9f, 0.3f, 0.7f);
                break;
            default:
                m_particulMain.startColor = new Color(1.0f, 0.7f, 0.4f, 0.7f);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!m_particul.isPlaying)
        {
            m_particul.Play();

            //花火の色設定
            int color = Random.Range(0, 5);
            switch (color)
            {
                case 0:
                    m_particulMain.startColor = new Color(1, 0, 0, 0.7f);
                    break;
                case 1:
                    m_particulMain.startColor = new Color(1, 1, 0, 0.7f);
                    break;
                case 2:
                    m_particulMain.startColor = new Color(0.4f, 0.9f, 1.0f, 0.7f);
                    break;
                case 3:
                    m_particulMain.startColor = new Color(0.9f, 0.2f, 0.9f, 0.7f);
                    break;
                case 4:
                    m_particulMain.startColor = new Color(0.4f, 0.9f, 0.3f, 0.7f);
                    break;
                default:
                    m_particulMain.startColor = new Color(1.0f, 0.7f, 0.4f, 0.7f);
                    break;

            }
        

            //花火の位置設定
            var pos = new Vector3(Random.Range(m_XposMin, m_XposMAX), Random.Range(m_YposMin, m_YposMAX), 1);
            transform.localPosition = pos;

            //サウンド再生(花火)
            Sound.PlaySE(Sound.SE.HANABI);

        }
    }
}

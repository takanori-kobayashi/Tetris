using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホールドブロックの表示処理
/// </summary>
public class HoldBlocks : MonoBehaviour
{
    Texture texture1;

    private int[,] m_HoldBlockTemp = new int[5, 5]
    {
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,0,0,0},
    };

    // Start is called before the first frame update
    void Start()
    {
        //Resourcesフォルダからテクスチャ読み込み
        texture1 = (Texture)Resources.Load($"BlockType01");

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Renderer>().material.mainTexture = texture1;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }


    }

    public void SetHoldBlocks(int HoldMino)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //Iミノ
                if (HoldMino == FallBlock.I_MINO)
                {
                    m_HoldBlockTemp[i, j] = FallBlock.I_MINO_TBL1[i, j];
                }

                //Tミノ
                else if (HoldMino == FallBlock.T_MINO)
                {
                    m_HoldBlockTemp[i, j] = FallBlock.T_MINO_TBL1[i, j];
                }

                //Jミノ
                else if (HoldMino == FallBlock.J_MINO)
                {
                    m_HoldBlockTemp[i, j] = FallBlock.J_MINO_TBL1[i, j];
                }

                //Lミノ
                else if (HoldMino == FallBlock.L_MINO)
                {
                    m_HoldBlockTemp[i, j] = FallBlock.L_MINO_TBL1[i, j];
                }

                //Sミノ
                else if (HoldMino == FallBlock.S_MINO)
                {
                    m_HoldBlockTemp[i, j] = FallBlock.S_MINO_TBL1[i, j];
                }


                //Zミノ
                else if (HoldMino == FallBlock.Z_MINO)
                {
                    m_HoldBlockTemp[i, j] = FallBlock.Z_MINO_TBL1[i, j];
                }

                //Oミノ
                else if (HoldMino == FallBlock.O_MINO)
                {
                    m_HoldBlockTemp[i, j] = FallBlock.O_MINO_TBL1[i, j];
                }

            }


        }


        byte r;
        byte g;
        byte b;

        switch (HoldMino)
        {
            case FallBlock.I_MINO:
                r = 47;
                g = 202;
                b = 253;
                break;
            case FallBlock.T_MINO:
                r = 253;
                g = 47;
                b = 248;
                break;
            case FallBlock.O_MINO:
                r = 250;
                g = 240;
                b = 50;
                break;
            case FallBlock.J_MINO:
                r = 47;
                g = 57;
                b = 253;
                break;
            case FallBlock.L_MINO:
                r = 254;
                g = 166;
                b = 46;
                break;
            case FallBlock.S_MINO:
                r = 62;
                g = 242;
                b = 57;
                break;
            case FallBlock.Z_MINO:
                r = 255;
                g = 0;
                b = 0;
                break;
            default:
                r = 100;
                g = 100;
                b = 100;
                break;
        }

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //   transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
        //}
        for (int i = 0; i < transform.childCount; i++)
        {
            //transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
            transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (1 == m_HoldBlockTemp[i, j])
                {
                    //this.transform.GetChild((i * 5) + j).gameObject.SetActive(true);
                    transform.GetChild((i * 5) + j).gameObject.SetActive(true);
                }
                else
                {
                    //this.transform.GetChild((i * 5) + j).gameObject.SetActive(false);
                    transform.GetChild((i * 5) + j).gameObject.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

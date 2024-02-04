using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ストックされたブロックの動作
/// </summary>
public class StockBlocks : MonoBehaviour
{
    //static GameObject refObj_FallBlocks;
    //private FallBlock m_FallBlock;
    Texture texture1;

    private int[,] m_StockBlockTemp = new int[5, 5]
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
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).gameObject.GetComponent<Renderer>().material.mainTexture = texture1;
            }
        }

        //FallBlocksのオブジェクト情報取得
        //refObj_FallBlocks = GameObject.Find("FallBlocks");
        //m_FallBlock = refObj_FallBlocks.GetComponent<FallBlock>();

        //for (int i = 0; i < 4; i++)
        //{
        //    SetStockBlock(i, FallBlock.T_MINO);
        //}
    }

    public void SetStockBlock(int index,int StockMino)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //Iミノ
                if (StockMino == FallBlock.I_MINO)
                {
                    m_StockBlockTemp[i, j] = FallBlock.I_MINO_TBL1[i, j];
                }

                //Tミノ
                else if (StockMino == FallBlock.T_MINO)
                {
                    m_StockBlockTemp[i, j] = FallBlock.T_MINO_TBL1[i, j];
                }

                //Jミノ
                else if (StockMino == FallBlock.J_MINO)
                {
                    m_StockBlockTemp[i, j] = FallBlock.J_MINO_TBL1[i, j];
                }

                //Lミノ
                else if (StockMino == FallBlock.L_MINO)
                {
                    m_StockBlockTemp[i, j] = FallBlock.L_MINO_TBL1[i, j];
                }

                //Sミノ
                else if (StockMino == FallBlock.S_MINO)
                {
                    m_StockBlockTemp[i, j] = FallBlock.S_MINO_TBL1[i, j];
                }


                //Zミノ
                else if (StockMino == FallBlock.Z_MINO)
                {
                    m_StockBlockTemp[i, j] = FallBlock.Z_MINO_TBL1[i, j];
                }

                //Oミノ
                else if (StockMino == FallBlock.O_MINO)
                {
                    m_StockBlockTemp[i, j] = FallBlock.O_MINO_TBL1[i, j];
                }

            }


        }


        byte r;
        byte g;
        byte b;

        switch (StockMino)
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
        for (int i = 0; i < transform.GetChild(index).childCount; i++)
        {
            //transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
            transform.GetChild(index).GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (1 == m_StockBlockTemp[i, j])
                {
                    //this.transform.GetChild((i * 5) + j).gameObject.SetActive(true);
                    transform.GetChild(index).GetChild((i * 5) + j).gameObject.SetActive(true);
                }
                else
                {
                    //this.transform.GetChild((i * 5) + j).gameObject.SetActive(false);
                    transform.GetChild(index).GetChild((i * 5) + j).gameObject.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }
}

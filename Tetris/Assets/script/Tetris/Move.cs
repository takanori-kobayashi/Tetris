using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// ゲームの動作
/// </summary>
public class Move : MonoBehaviour
{
    const int MAX_LINE_X = 10;
    const int MAX_LINE_Y = 20;

    static FallBlock m_FallBlockObj;

    private float m_EraseBlockAlpha = 1.0f;

    //public const int OP_STATE_PLAY = 0;
    //public const int OP_STATE_ERASETRANS = 1;
    //public const int OP_STATE_ERASE = 2;
    //public const int OP_STATE_SHIFTBLOCK = 3;
    //public const int OP_STATE_FALLBLOCK_SET = 4;

    /// <summary>
    /// ゲームの状態
    /// </summary>
    public enum OperationState
    {
        OP_STATE_STARTUP,
        OP_STATE_START,
        OP_STATE_PLAY,
        OP_STATE_ERASETRANS,
        OP_STATE_ERASE,
        OP_STATE_SHIFTBLOCK,
        OP_STATE_FALLBLOCK_SET,
        OP_STATE_HOLD,
        OP_STATE_CLEAR,
        OP_STATE_GAMEOVER,
        OP_STATE_PAUSE,
    }

    public static OperationState m_OperationState { get; private set; }
    public static OperationState m_OperationStateOld { get; private set; }

    struct StaticBlckInfo
    {
        public int color;  //0はブロック無し
        public GameObject BlockObjectsName;

        public StaticBlckInfo(int c, GameObject o)
        {
            color = c;
            BlockObjectsName = o;

        }
    }

    /// <summary>
    /// ブロックのテクスチャ
    /// </summary>
    private Texture texture1;

    /// <summary>
    /// 下枠も含める
    /// </summary>
    StaticBlckInfo[,] m_Square = new StaticBlckInfo[MAX_LINE_Y, MAX_LINE_X]
    {
     //   {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        {new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),new StaticBlckInfo(0,null),},
        };


    public GameObject StaticBlock;


    int[,] m_EraseLine = new int[4, 2];

    public TetrisDebugView m_DebugDraw;
    static GameObject refObj;

    private GameObject refObj_StockBlocks;
    private StockBlocks m_StockBlocks;

    private GameObject refObj_HoldBlocks;
    private HoldBlocks m_HoldBlocks;

    private bool m_PushButton;

    /// <summary>
    /// クリアまでのライン数
    /// </summary>
    public static readonly int MAX_LINE_COUNT = 100;
    
    /// <summary>
    /// 消したライン数
    /// </summary>
    public static int m_EraseLineCount { get; private set; }

    /// <summary>
    /// スコア
    /// </summary>
    public static int m_Score { get; private set; }


    /// <summary>
    /// 共通カウント
    /// </summary>
    int m_CmnCount;

    // Start is called before the first frame update
    void Start()
    {
        // 弾丸の複製
        GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(-1, -1, 0.0f);
        StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(0, -1, 0.0f);
        StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(1, -1, 0.0f);
        StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(2, -1, 0.0f);
        StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(3, -1, 0.0f);
        StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(4, -1, 0.0f);
        StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(5, -1, 0.0f);
        StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(6, -1, 0.0f);
        StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(7, -1, 0.0f);
        StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(8, -1, 0.0f);
        StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(9, -1, 0.0f);
        StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(10, -1, 0.0f);

        transform.position = new Vector3(5.0f, 20.0f, 0.0f);

        m_OperationState = OperationState.OP_STATE_STARTUP;

        //FallBlocksのオブジェクト情報取得
        refObj = GameObject.Find("FallBlocks");
        m_FallBlockObj = refObj.GetComponent<FallBlock>();

        //StockBlocksのオブジェクト情報取得
        refObj_StockBlocks = GameObject.Find("StockBlocksSet");
        m_StockBlocks = refObj_StockBlocks.GetComponent<StockBlocks>();

        //HoldBlocksのオブジェクト情報取得
        refObj_HoldBlocks = GameObject.Find("HoldBlocks");
        m_HoldBlocks = refObj_HoldBlocks.GetComponent<HoldBlocks>();

        //GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
        //StaticBlocks.transform.position = new Vector3(2, 2, 0.0f);
        // 弾丸の複製
        //GameObject StaticBlocks = Instantiate(FallBlock) as GameObject;
        //StaticBlocks.transform.parent = this.transform;

        refObj = GameObject.Find("Text");
        if (refObj != null)
        {
            m_DebugDraw = refObj.GetComponent<TetrisDebugView>();
        }

        //Resourcesフォルダからテクスチャ読み込み
        texture1 = (Texture)Resources.Load($"BlockType01");
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //transform.GetChild(i).gameObject.GetComponent<Renderer>().material.mainTexture = texture1;
        //}

        m_EraseLineCount = 0;
        m_Score = 0;

        m_CmnCount = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void StaticBlockSet(int x, int y)
    {
        // 弾丸の複製
        GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
        StaticBlocks.transform.position = new Vector3(x, y, 0.0f);

        m_Square[x, y].color = 1;
        m_Square[x, y].BlockObjectsName = StaticBlocks;
    }


    public void OperationStateSet(OperationState state,int mino)
    {
        m_OperationState = state;
        m_HoldBlocks.SetHoldBlocks(mino);
    }

    private bool StaticBlockArrayCheck(int x, int y)
    {

        if ((0 <= y && y < MAX_LINE_Y) && (0 <= x && x < MAX_LINE_X))
        {
            return true;
        }

        return false;

    }
    
    void StaticBlockSetRenew()
    {
        
        for(int i=0; i<MAX_LINE_Y; i++)
        {
            for (int j = 0; j < MAX_LINE_X; j++)
            {
                //StaticBlocks.transform.position = new Vector3(0, -1, 0.0f);
                if(m_Square[i, j].BlockObjectsName != null)
                { 
                float x = (float)j;
                float y = (float)i;
                m_Square[i, j].BlockObjectsName.transform.position = new Vector3(x, y, 0.0f);
                }
            }
        }

        /*
        var str = "";
        //デバッグ表示
        for (int i = 0; i < MAX_LINE_Y; i++)
        {
            for (int j = 0; j < MAX_LINE_X; j++)
            {
                if (m_Square[i, j].color == 1)
                {
                    str += "○";
                }
                else
                {
                    str += "●";
                }
            }
            str += "\n";
        }

        m_DebugDraw.SetText(str);
        */
    }

    public void StaticBlockSet(int x, int y, int minotype)
    {
        // ブロックのの複製
        if (GameObject.FindGameObjectsWithTag("StaticBlock").Length < 210)
        {

            byte r;
            byte g;
            byte b;


            switch (minotype)
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

            if (1 == m_FallBlockObj.m_FallBlock[0, 0] && true == StaticBlockArrayCheck(x - 2, y + 2))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x - 2, y + 2, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y + 2, x - 2].color = 1;
                m_Square[y + 2, x - 2].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[0, 1] && true == StaticBlockArrayCheck(x - 1, y + 2))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x - 1, y + 2, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y + 2, x - 1].color = 1;
                m_Square[y + 2, x - 1].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[0, 2] && true == StaticBlockArrayCheck(x, y + 2))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x, y + 2, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y + 2, x].color = 1;
                m_Square[y + 2, x].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[0, 3] && true == StaticBlockArrayCheck(x + 1, y + 2))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x + 1, y + 2, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y + 2, x + 1].color = 1;
                m_Square[y + 2, x + 1].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[0, 4] && true == StaticBlockArrayCheck(x + 2, y + 2))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x + 2, y + 2, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y + 2, x + 2].color = 1;
                m_Square[y + 2, x + 2].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[1, 0] && true == StaticBlockArrayCheck(x - 2, y + 1))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x - 2, y + 1, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y + 1, x - 2].color = 1;
                m_Square[y + 1, x - 2].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[1, 1] && true == StaticBlockArrayCheck(x - 1, y + 1))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x - 1, y + 1, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y + 1, x - 1].color = 1;
                m_Square[y + 1, x - 1].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[1, 2] && true == StaticBlockArrayCheck(x, y + 1))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x, y + 1, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y + 1, x].color = 1;
                m_Square[y + 1, x].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[1, 3] && true == StaticBlockArrayCheck(x + 1, y + 1))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x + 1, y + 1, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y + 1, x + 1].color = 1;
                m_Square[y + 1, x + 1].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[1, 4] && true == StaticBlockArrayCheck(x + 2, y + 1))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x + 2, y + 1, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y + 1, x + 2].color = 1;
                m_Square[y + 1, x + 2].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[2, 0] && true == StaticBlockArrayCheck(x - 2, y))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x - 2, y, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y, x - 2].color = 1;
                m_Square[y, x - 2].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[2, 1] && true == StaticBlockArrayCheck(x - 1, y))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x - 1, y, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y, x - 1].color = 1;
                m_Square[y, x - 1].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[2, 2] && true == StaticBlockArrayCheck(x, y))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x, y, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y, x].color = 1;
                m_Square[y, x].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[2, 3] && true == StaticBlockArrayCheck(x + 1, y))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x + 1, y, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y, x + 1].color = 1;
                m_Square[y, x + 1].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[2, 4] && true == StaticBlockArrayCheck(x + 2, y))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x + 2, y, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y, x + 2].color = 1;
                m_Square[y, x + 2].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[3, 0] && true == StaticBlockArrayCheck(x - 2, y - 1))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x - 2, y - 1, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y - 1, x - 2].color = 1;
                m_Square[y - 1, x - 2].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[3, 1] && true == StaticBlockArrayCheck(x - 1, y - 1))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x - 1, y - 1, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y - 1, x - 1].color = 1;
                m_Square[y - 1, x - 1].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[3, 2] && true == StaticBlockArrayCheck(x, y - 1))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x, y - 1, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y - 1, x].color = 1;
                m_Square[y - 1, x].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[3, 3] && true == StaticBlockArrayCheck(x + 1, y - 1))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x + 1, y - 1, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y - 1, x + 1].color = 1;
                m_Square[y - 1, x + 1].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[3, 4] && true == StaticBlockArrayCheck(x + 2, y - 1))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x + 2, y - 1, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y - 1, x + 2].color = 1;
                m_Square[y - 1, x + 2].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[4, 0] && true == StaticBlockArrayCheck(x - 2, y - 2))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x - 2, y - 2, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y - 2, x - 2].color = 1;
                m_Square[y - 2, x - 2].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[4, 1] && true == StaticBlockArrayCheck(x - 1, y - 2))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x - 1, y - 2, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y - 2, x - 1].color = 1;
                m_Square[y - 2, x - 1].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[4, 2] && true == StaticBlockArrayCheck(x, y - 2))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x, y - 2, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y - 2, x].color = 1;
                m_Square[y - 2, x].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[4, 3] && true == StaticBlockArrayCheck(x + 1, y - 2))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x + 1, y - 2, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y - 2, x + 1].color = 1;
                m_Square[y - 2, x + 1].BlockObjectsName = StaticBlocks;
            }
            if (1 == m_FallBlockObj.m_FallBlock[4, 4] && true == StaticBlockArrayCheck(x + 2, y - 2))
            {
                GameObject StaticBlocks = Instantiate(StaticBlock) as GameObject;
                StaticBlocks.transform.position = new Vector3(x + 2, y - 2, 0.0f);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
                StaticBlocks.gameObject.GetComponent<Renderer>().material.mainTexture = texture1;

                m_Square[y - 2, x + 2].color = 1;
                m_Square[y - 2, x + 2].BlockObjectsName = StaticBlocks;
            }

            //m_FallBlockObj.transform.position = new Vector3(5.0f, 20.0f, 0.0f);
            //m_FallBlockObj.FallBlock_SetMino();
            //いったん非表示に
            m_FallBlockObj.FallBlock_HideMino();

            if (true == LineCheck())
            {
                //状態をライン消去へ
                m_OperationState = OperationState.OP_STATE_ERASETRANS;

                //サウンド再生(ライン消去)
                Sound.PlaySE(Sound.SE.MINO_ERASE, 0.2f);
            }
            else
            {
                //テトリミノをいったん消去
                m_OperationState = OperationState.OP_STATE_FALLBLOCK_SET;

                //サウンド再生(ブロックセット)
                Sound.PlaySE(Sound.SE.MINO_SET, 0.2f);
            }


            /*
            var str = "";
            //デバッグ表示
            for (int i = 0; i < MAX_LINE_Y; i++)
            {
                for (int j = 0; j < MAX_LINE_X; j++)
                {
                    if (m_Square[i, j].color == 1)
                    {
                        if(i==19 && j == 4)
                        {
                            str += "×";
                        }
                        else if (i == 19 && j == 5)
                        {
                            str += "×";
                        }
                        else
                        { 
                        str += "○";
                        }
                    }
                    else
                    {
                        str += "●";
                    }
                }
                str += ":"+i+"\n";
            }
            m_DebugDraw.SetText(str);
            */

            //ゲームオーバー
            if(m_Square[19, 4].color == 1 || m_Square[19, 5].color == 1)
            {
                //ラインが消されていなければゲームオーバーに
                if (false == LineCheck())
                {
                    m_OperationState = OperationState.OP_STATE_GAMEOVER;
                }
            }

        }

    }

    bool LineCheck()
    {
        bool erase = false;
        var count = 0;

        for (int i = 0; i < MAX_LINE_Y; i++)
        {
            var line = true;

            for (int j = 0; j < MAX_LINE_X; j++)
            {
                if (m_Square[i, j].color == 0)
                {
                    line = false;
                }
            }

            if (line == true)
            {
                /*
                for (int j = 0; j < 10; j++)
                {
                    m_Square[i, j].color = 0;
                    //Destroy(m_Square[i, j].BlockObjectsName);
                    m_Square[i, j].BlockObjectsName = null;

                }
                */

                m_EraseLine[count, 0] = i;
                m_EraseLine[count, 1] = 1;
                count++;

                erase = true;
            }
        }

        return erase;
    }

    //内部データの消去
    void LineErase()
    {
        var cnttmp = 0;

        for (int i = 0; i < 4; i++)
        {
            if (m_EraseLine[i, 1] == 1)
            {
                var lineTmp = m_EraseLine[i, 0];
                for (int j = 0; j < MAX_LINE_X; j++)
                {
                    m_Square[lineTmp, j].color = 0;
                    m_Square[lineTmp, j].BlockObjectsName = null;
                }

                cnttmp++;
            }
        }
        
        //消したラインを加算
        m_EraseLineCount += cnttmp;

        //スコア加算
        switch(cnttmp)
        {
            case 1:
                m_Score += 100;
                break;
            case 2:
                m_Score += 200;
                break;
            case 3:
                m_Score += 500;
                break;
            case 4:
                m_Score += 1000;
                break;
            default:
                m_Score += 100;
                break;
        }
        

        //消去したラインをずらす
        for (int i = 4-1; 0 <= i; i--)
        {
            if (m_EraseLine[i, 1] == 1)
            {
                var lineTmp = m_EraseLine[i, 0];

                for(int k = lineTmp; k < MAX_LINE_Y - 1; k++)
                {
                    for (int j = 0; j < MAX_LINE_X; j++)
                    {
                        m_Square[k, j].color = m_Square[k + 1, j].color;
                        m_Square[k, j].BlockObjectsName = m_Square[k + 1, j].BlockObjectsName;
                    }
                }
            }
        }

        //一番上のラインは必ず消す
        for (int i = 0; i < MAX_LINE_X; i++)
        {
            m_Square[MAX_LINE_Y - 1, i].color = 0;
            m_Square[MAX_LINE_Y - 1, i].BlockObjectsName = null;
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                m_EraseLine[i, j] = 0;
                m_EraseLine[i, j] = 0;
            }
        }
    }

    /// <summary>
    /// 表示の消去
    /// </summary>
    void LineDrawErase()
    {
        
        for (int i = 0; i < 4; i++)
        {
            if (m_EraseLine[i, 1] == 1)
            {
                var lineTmp = m_EraseLine[i, 0];
                for (int j = 0; j < MAX_LINE_X; j++)
                {
                    Destroy(m_Square[lineTmp, j].BlockObjectsName);
                }
            }
        }

        m_EraseBlockAlpha = 1.0f;

    }

    /// <summary>
    /// 表示の半透明化
    /// </summary>
    void LineDrawTransparent()
    {

        m_EraseBlockAlpha -= 0.05f;

        for (int i = 0; i < 4; i++)
        {
            if (m_EraseLine[i, 1] == 1)
            {
                var lineTmp = m_EraseLine[i, 0];
                for (int j = 0; j < MAX_LINE_X; j++)
                {
                    Color color = m_Square[lineTmp, j].BlockObjectsName.GetComponent<Renderer>().material.color;
                    color.a = m_EraseBlockAlpha;
                    m_Square[lineTmp, j].BlockObjectsName.GetComponent<Renderer>().material.color = color;
                }               
            
            }
        }

        if (m_EraseBlockAlpha <= 0.0f)
        {
            m_EraseBlockAlpha = 0.0f;
            m_OperationState = OperationState.OP_STATE_ERASE;
        }
    }

    void LineDrawRenew()
    {

    }

    /// <summary>
    /// ポーズ状態に切り替え
    /// </summary>
    public static void GamePause()
    {
        m_OperationStateOld = m_OperationState;
        m_OperationState = OperationState.OP_STATE_PAUSE;
        Time.timeScale = 0f;
    }

    /// <summary>
    /// ポーズから前の状態に切り替え
    /// </summary>
    public static void GamePlayResume()
    {
        m_OperationState = m_OperationStateOld;
        Time.timeScale = 1f;
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public static void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("hold") == 1 || Input.GetAxisRaw("Rotate") == 1)
        {
            m_PushButton = true;
        }
        else
        {
            m_PushButton = false;
        }

        if (Input.GetKeyDown("q"))
        {
            if (m_OperationState != Move.OperationState.OP_STATE_PAUSE)
            {
                GamePause();
            }
            else
            {
                GamePlayResume();
            }
        }

    }

    private void RenewStockBlocks()
    {
        for (int i = 0; i < m_FallBlockObj.m_StockMino.Length; i++)
        {
            //for (int j = 0; j < m_FallBlockObj.m_StockMino.Length; j++)
            //{
                //m_StockBlocks.SetStockBlock(m_StockBlocks.transform.GetChild(), m_FallBlockObj.m_StockMino[i]);
                m_StockBlocks.SetStockBlock(i, m_FallBlockObj.m_StockMino[i]);
            //}
        }
    }

    //public OperationState Get_OperationState()
    //{
        //return OperationState.m_OperationState;
    //}

    private void FixedUpdate()
    {
        //const int OP_STATE_PLAY = 0;
        //const int OP_STATE_ERASETRANS = 1;
        //const int OP_STATE_ERASE = 2;
        switch (m_OperationState)
        {
            case OperationState.OP_STATE_STARTUP:
                m_OperationState = OperationState.OP_STATE_START;
                break;
            case OperationState.OP_STATE_START:
                RenewStockBlocks();

                if (60 < m_CmnCount)
                {
                    m_OperationState = OperationState.OP_STATE_PLAY;
                    m_CmnCount = 0;
                }
                m_CmnCount++;

                break;
            case OperationState.OP_STATE_ERASETRANS:
                ///ラインを半透明(表示)
                LineDrawTransparent();
                break;
            case OperationState.OP_STATE_ERASE:
                ///ラインを消す(表示)
                LineDrawErase();
                //内部
                LineErase();

                m_OperationState = OperationState.OP_STATE_SHIFTBLOCK;
                break;
            case OperationState.OP_STATE_SHIFTBLOCK:
                StaticBlockSetRenew();
                m_OperationState = OperationState.OP_STATE_FALLBLOCK_SET;
                break;
            case OperationState.OP_STATE_FALLBLOCK_SET:
                m_FallBlockObj.transform.position = new Vector3(5.0f, 20.0f, 0.0f);
                m_FallBlockObj.FallBlock_SetMino();
                RenewStockBlocks();

                //クリア判定
                if (MAX_LINE_COUNT <= m_EraseLineCount)
                {
                    m_EraseLineCount = MAX_LINE_COUNT;
                    m_OperationState = OperationState.OP_STATE_CLEAR;
                }
                //それ以外は続行
                else
                {
                    m_OperationState = OperationState.OP_STATE_PLAY;
                }
                break;
            case OperationState.OP_STATE_HOLD:
                m_FallBlockObj.transform.position = new Vector3(5.0f, 20.0f, 0.0f);
                RenewStockBlocks();
                m_OperationState = OperationState.OP_STATE_PLAY;
                break;
            case OperationState.OP_STATE_CLEAR:
                //一定時間後ボタンが反応するように                
                if (180 < m_CmnCount)
                {
                    if(m_PushButton == true)
                    {
                        SceneManager.LoadScene("Title");
                    }
                }
                else
                {
                    m_CmnCount++;
                }
                break;
            case OperationState.OP_STATE_GAMEOVER:
                //一定時間後ボタンが反応するように                
                if (180 < m_CmnCount)
                {
                    if (m_PushButton == true)
                    {
                        SceneManager.LoadScene("Title");
                    }
                }
                else
                {
                    m_CmnCount++;
                }
                break;
            case OperationState.OP_STATE_PAUSE:
                break;
            default:
                break;
        }

    }
}

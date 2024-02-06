using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 落下ブロックの動作
/// </summary>
public class FallBlock : MonoBehaviour
{
    readonly float MOVE_SPEED_LEVEL1 = 1.0f;
    readonly float MOVE_SPEED_LEVEL2 = 2.0f;
    readonly float MOVE_SPEED_LEVEL3 = 3.0f;
    readonly float MOVE_SPEED_LEVEL4 = 4.0f;
    readonly float MOVE_SPEED_LEVEL5 = 5.0f;
    readonly float MOVE_SPEED_LEVEL6 = 6.0f;
    readonly float MOVE_SPEED_LEVEL7 = 7.0f;
    readonly float MOVE_SPEED_LEVEL8 = 8.0f;
    readonly float MOVE_SPEED_LEVEL9 = 9.0f;
    readonly float MOVE_SPEED_LEVEL10 = 10.0f;
    readonly float MOVE_SPEED_LEVEL11 = 11.0f;
    readonly float MOVE_SPEED_LEVEL12 = 12.0f;
    readonly float MOVE_SPEED_LEVEL13 = 13.0f;
    readonly float MOVE_SPEED_LEVEL14 = 14.0f;
    readonly float MOVE_SPEED_LEVEL15 = 15.0f;
    readonly float MOVE_SPEED_LEVEL16 = 16.0f;
    readonly float MOVE_SPEED_LEVEL17 = 17.0f;
    readonly float MOVE_SPEED_LEVEL18 = 18.0f;
    readonly float MOVE_SPEED_LEVEL19 = 19.0f;
    readonly float MOVE_SPEED_LEVEL20 = 20.0f;


    readonly int RIGIDITY_MAX_CONT = 20;

    public TetrisDebugText m_DebugText;

    private int m_RigidityCnt = 0;

    enum PUSH_DIRECT
    {
        NEUTRAL,
        UP,
        LEFT,
        RIGHT,
        DOWN,
        HOLD,
    }

    PUSH_DIRECT m_keypush = PUSH_DIRECT.NEUTRAL;

    /// <summary>
    /// 水平方向の入力
    /// </summary>
    float m_inputHorizontal;
    readonly int IMPUT_HORIZONTALCNT_MAX = 15;
    int m_inputHorizontalRCnt;
    int m_inputHorizontalLCnt;

    /// <summary>
    /// 垂直方向の入力
    /// </summary>
    float m_inputVertical;

    /// <summary>
    /// Rigidbodyのコンポーネント
    /// </summary>
    Rigidbody m_rb;

    public bool m_rotate { get; private set; }
    int m_rotateInterval = 0;
    int m_rotateDirection = 1;

    static GameObject refObj_Text;

    public GameObject refObj_GameMain;
    Move mv;

    public int[,] m_FallBlock = new int[5, 5]
    {
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,0,0,0},
    };

    private int[,] m_FallBlockTemp = new int[5, 5]
    {
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,0,0,0},
    };



    public const int I_MINO = 0;
    public const int T_MINO = 1;
    public const int O_MINO = 2;
    public const int J_MINO = 3;
    public const int L_MINO = 4;
    public const int S_MINO = 5;
    public const int Z_MINO = 6;

    public int m_NowMino { get; private set; } = I_MINO;
    public int[] m_StockMino { get; private set; } = new int[5];

    private int m_Roatate = 0;

    private bool m_HoldMinoStock;
    public static bool m_HoldFlg { get; private set; }
    private int m_HoldMino;

    /// <summary>
    /// ホールドの実行フラグ
    /// </summary>
    private bool m_HoldExFlg = false;


    /// <summary>
    /// Iミノ
    /// </summary>
    public static readonly int[,] I_MINO_TBL1 = new int[5, 5]
    {
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] I_MINO_TBL2 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,0,0,0},
        { 0,1,1,1,1},
        { 0,0,0,0,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] I_MINO_TBL3 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
    };
    public static readonly int[,] I_MINO_TBL4 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,0,0,0},
        { 1,1,1,1,0},
        { 0,0,0,0,0},
        { 0,0,0,0,0},
    };

    /// <summary>
    /// Tミノ
    /// </summary>
    public static readonly int[,] T_MINO_TBL1 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,1,0,0},
        { 0,1,1,1,0},
        { 0,0,0,0,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] T_MINO_TBL2 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,1,0,0},
        { 0,0,1,1,0},
        { 0,0,1,0,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] T_MINO_TBL3 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,0,0,0},
        { 0,1,1,1,0},
        { 0,0,1,0,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] T_MINO_TBL4 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,1,0,0},
        { 0,1,1,0,0},
        { 0,0,1,0,0},
        { 0,0,0,0,0},
    };

    /// <summary>
    /// Oミノ
    /// </summary>
    public static readonly int[,] O_MINO_TBL1 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,1,1,0,0},
        { 0,1,1,0,0},
        { 0,0,0,0,0},
        { 0,0,0,0,0},
    };

    /// <summary>
    /// Jミノ
    /// </summary>
    public static readonly int[,] J_MINO_TBL1 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,1,0,0,0},
        { 0,1,1,1,0},
        { 0,0,0,0,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] J_MINO_TBL2 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,1,1,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] J_MINO_TBL3 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,0,0,0},
        { 0,1,1,1,0},
        { 0,0,0,1,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] J_MINO_TBL4 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,1,1,0,0},
        { 0,0,0,0,0},
    };

    /// <summary>
    /// Lミノ
    /// </summary>
    public static readonly int[,] L_MINO_TBL1 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,0,1,0},
        { 0,1,1,1,0},
        { 0,0,0,0,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] L_MINO_TBL2 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,1,1,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] L_MINO_TBL3 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,0,0,0},
        { 0,1,1,1,0},
        { 0,1,0,0,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] L_MINO_TBL4 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,1,1,0,0},
        { 0,0,1,0,0},
        { 0,0,1,0,0},
        { 0,0,0,0,0},
    };

    /// <summary>
    /// Sミノ
    /// </summary>
    public static readonly int[,] S_MINO_TBL1 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,1,1,0},
        { 0,1,1,0,0},
        { 0,0,0,0,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] S_MINO_TBL2 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,1,0,0,0},
        { 0,1,1,0,0},
        { 0,0,1,0,0},
        { 0,0,0,0,0},
    };

    /// <summary>
    /// Zミノ
    /// </summary>
    public static readonly int[,] Z_MINO_TBL1 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,1,1,0,0},
        { 0,0,1,1,0},
        { 0,0,0,0,0},
        { 0,0,0,0,0},
    };
    public static readonly int[,] Z_MINO_TBL2 = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,0,1,0},
        { 0,0,1,1,0},
        { 0,0,1,0,0},
        { 0,0,0,0,0},
    };

    /// <summary>
    /// 回転確定前の角度
    /// </summary>
    Vector3 m_PreAngles;

    /// <summary>
    /// 回転確定前の座標
    /// </summary>
    Vector3 m_PrePosition;

    /// <summary>
    /// ボタン押下状態(共通)
    /// </summary>
    bool m_PushKeyDown;

    Texture texture1;

    bool m_colisionstay;

    /// <summary>
    /// デバッグ用変数
    /// </summary>
    int debug1;
    int debug2;
    int debug3;

    // Start is called before the first frame update
    void Start()
    {

        //GameMainのオブジェクト情報取得
        refObj_GameMain = GameObject.Find("GameMain");
        mv = refObj_GameMain.GetComponent<Move>();

        m_rb = GetComponent<Rigidbody>();

        //Resourcesフォルダからテクスチャ読み込み
        texture1 = (Texture)Resources.Load($"BlockType01");

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Renderer>().material.mainTexture = texture1;
        }

        //ストックの初期化
        for (int i = 0; i < m_StockMino.Length; i++)
        {
            if (m_StockMino[i] == 0)
            {
                m_StockMino[i] = Random.Range(0, Z_MINO + 1);
            }
        }

        m_PreAngles = new Vector3(0.0f, 0.0f, 0.0f);

        m_NowMino = Random.Range(0, Z_MINO + 1);
        FallBlock_SetMino();

        m_PushKeyDown = false;

        m_HoldFlg = false;

        //デバッグ
        refObj_Text = GameObject.Find("DebugText");
        m_DebugText = refObj_Text.GetComponent<TetrisDebugText>();

        debug1 =DebugText.AddText("aaaa", 100, 100, 100, 100);
        debug2 = DebugText.AddText("debug", 100, 100, 100, 100);
        debug3 = DebugText.AddText("debug", 100, 130, 100, 100);
    }

    // Update is called once per frame
    void Update()
    {
        m_inputHorizontal = Input.GetAxisRaw("Horizontal");
        m_inputVertical = Input.GetAxisRaw("Vertical");

        var keydown = false;
        //if (Input.GetKey(KeyCode.UpArrow))

        if (m_inputVertical == 1)
        {
            m_keypush = PUSH_DIRECT.UP;
            keydown = true;
        }
        //else if (Input.GetKey(KeyCode.DownArrow))
        else if (m_inputVertical == -1)
        {
            keydown = true;
            m_keypush = PUSH_DIRECT.DOWN;
        }
        //else if (Input.GetKeyDown(KeyCode.LeftArrow))
        else if (m_inputHorizontal == -1)
        {
            if(m_rotate == false)
            {
                keydown = true;
                m_keypush = PUSH_DIRECT.LEFT;
            }
        }
        //else if (Input.GetKeyDown(KeyCode.RightArrow))
        else if (m_inputHorizontal == 1)
        {
            if (m_rotate == false)
            {
                keydown = true;
                m_keypush = PUSH_DIRECT.RIGHT;

            }
        }
        //else if (Input.GetKeyDown(KeyCode.LeftControl))
        else if (Input.GetAxisRaw("hold") == 1 && m_PushKeyDown == false)
        {
            if (m_rotate == false && m_HoldFlg == false)
            {
                keydown = true;
                m_keypush = PUSH_DIRECT.HOLD;

                m_HoldExFlg = true;

                DebugText.SetText("hold", 100.0f, 100.0f, 100.0f, 100.0f, debug2);
            }
            m_PushKeyDown = true;
        }
        else
        {
            DebugText.SetText("neutral", 100.0f, 100.0f, 100.0f, 100.0f, debug2);
            m_keypush = PUSH_DIRECT.NEUTRAL;
        }

      

        // 小数第一位で四捨五入
        float x = (float)System.Math.Round(transform.position.x, System.MidpointRounding.AwayFromZero);
        transform.position = new Vector3(x, transform.position.y, 0.0f);

        if (keydown == false)
        {
            //transform.Translate(0.0f, -0.01f, 0.0f, Space.World);
            m_rb.velocity = new Vector3(0, 0, 0);
        }

        //if (Input.GetKeyDown(KeyCode.LeftAlt) && Move.OperationState.OP_STATE_PLAY == mv.m_OperationState)
        if (Input.GetAxisRaw("Rotate") == 1 && Move.OperationState.OP_STATE_PLAY == Move.m_OperationState && m_PushKeyDown == false)
        {

            if (m_rotate == false)
            {
                m_rotate = true;
                m_PrePosition = transform.position;

#if false
                m_Roatate++;
                if (3 < m_Roatate)
                {
                    m_Roatate = 0;
                }



                FallBlock_Rotate();


                for (int i = 0; i < 5; i++)
                {

                    for (int j = 0; j < 5; j++)

                    {
                        m_FallBlock[i, j] = m_FallBlockTemp[i, j];
                    }
                }
#endif
            }
            m_PushKeyDown = true;
        }

        //ボタンが押されていない場合は押下フラグをクリア
        if(Input.GetAxisRaw("Rotate") == 0 && Input.GetAxisRaw("hold") == 0)
        {
            m_PushKeyDown = false;
        }

        /*
        else if (m_rotate == true)
        {

            m_rotateInterval++;
            transform.Rotate(new Vector3(0, 0, 10 * m_rotateDirection));
            if (9 <= m_rotateInterval)
            {
                m_rotateInterval = 0;
                m_rotate = false;
            }

        }
        */



    }

    public void FallBlock_SetMino()
    {
        m_NowMino = m_StockMino[0];
        m_StockMino[0] = m_StockMino[1];
        m_StockMino[1] = m_StockMino[2];
        m_StockMino[2] = m_StockMino[3];
        m_StockMino[3] = m_StockMino[4];
        m_StockMino[4] = Random.Range(0, Z_MINO + 1);
        

        //回転のリセット
        m_Roatate = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        m_PreAngles = new Vector3(0.0f, 0.0f, 0.0f);
        FallBlock_Rotate();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (1 == m_FallBlockTemp[i, j])
                {
                    this.transform.GetChild((i * 5) + j).gameObject.SetActive(true);
                }
                else
                {
                    this.transform.GetChild((i * 5) + j).gameObject.SetActive(false);
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {

            for (int j = 0; j < 5; j++)

            {
                m_FallBlock[i, j] = m_FallBlockTemp[i, j];
            }
        }
    }

    //ホールド
    public void FallBlock_Hold()
    {
        if(m_HoldMinoStock == false)
        {
            m_HoldMino = m_NowMino;

            m_NowMino = m_StockMino[0];
            m_StockMino[0] = m_StockMino[1];
            m_StockMino[1] = m_StockMino[2];
            m_StockMino[2] = m_StockMino[3];
            m_StockMino[3] = m_StockMino[4];
            m_StockMino[4] = Random.Range(0, Z_MINO + 1);
        }
        else
        {
            var minotmp = m_NowMino;
            m_NowMino = m_HoldMino;
            m_HoldMino = minotmp;
        }



        //回転のリセット
        m_Roatate = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        FallBlock_Rotate();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (1 == m_FallBlockTemp[i, j])
                {
                    this.transform.GetChild((i * 5) + j).gameObject.SetActive(true);
                }
                else
                {
                    this.transform.GetChild((i * 5) + j).gameObject.SetActive(false);
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {

            for (int j = 0; j < 5; j++)

            {
                m_FallBlock[i, j] = m_FallBlockTemp[i, j];
            }
        }

        m_HoldFlg = true;
        m_HoldMinoStock = true;

        //サウンド再生(ホールド)
        Sound.PlaySE(Sound.SE.HOLD, 0.2f);
    }

    //ブロックを非表示に
    public void FallBlock_HideMino()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                this.transform.GetChild((i * 5) + j).gameObject.SetActive(false);
            }
        }
    }

    private float BlockSpeed()
    {
        var tmp = MOVE_SPEED_LEVEL1;

        //ライン数消去によるブロックのスピードの変化
        if (190 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL20;
        }
        else if (180 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL19;
        }
        else if (170 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL18;
        }
        else if (160 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL17;
        }
        else if (150 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL16;
        }
        else if (140 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL15;
        }
        else if (130 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL14;
        }
        else if (120 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL13;
        }
        else if (110 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL12;
        }
        else if (100 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL11;
        }
        else if (90 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL10;
        }
        else if (80 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL9;
        }
        else if (70 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL8;
        }
        else if (60 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL7;
        }
        else if (50 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL6;
        }
        else if (40 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL5;
        }
        else if (30 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL4;
        }
        else if(20 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL3;
        }
        else if (10 <= Move.m_EraseLineCount)
        {
            tmp = MOVE_SPEED_LEVEL2;
        }
        else
        {
            tmp = MOVE_SPEED_LEVEL1;
        }

        return tmp;
    }

    private void FixedUpdate()
    {
        var speed = 30.0f;
        var auto = BlockSpeed();
        var movetmp = 0.0f;

        //ステージの状態がクリアなら動作させない
        if (Move.m_OperationState == Move.OperationState.OP_STATE_CLEAR)
        {
            //クリア時はブロックを非表示にする
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            return;
        }

        switch(Move.m_OperationState)
        {
            case Move.OperationState.OP_STATE_CLEAR: //ステージの状態がクリアなら動作させない
                return;
            case Move.OperationState.OP_STATE_START: //ゲームの開始時なら動作させない
                return;
            case Move.OperationState.OP_STATE_STARTUP: //ゲームの開始準備なら動作させない
                return;
            case Move.OperationState.OP_STATE_GAMEOVER: //ゲームオーバーなら動作させない
                return;
        }

        DebugText.SetText("m_inputHorizontalRCnt:"+ m_inputHorizontalRCnt+ "\nm_inputHorizontalLCnt:"+ m_inputHorizontalLCnt, 0, 0, 1000.0f, 1000.0f, debug1);


        //キーが押下されていた場合の処理
        switch (m_keypush)
        {
            case PUSH_DIRECT.NEUTRAL:
                m_rb.velocity = new Vector3(0, -auto, 0);
                m_inputHorizontalRCnt = 0;
                m_inputHorizontalLCnt = 0;
                break;
            case PUSH_DIRECT.LEFT:
                if (m_rotate == false)
                {
                    if (m_inputHorizontalLCnt == 0 || IMPUT_HORIZONTALCNT_MAX <= m_inputHorizontalLCnt)
                    {
                        movetmp = -50.0f;
                    }
                    m_rb.velocity = new Vector3(movetmp, -auto, 0);
                    m_inputHorizontalRCnt = 0;
                    if (m_inputHorizontalLCnt < IMPUT_HORIZONTALCNT_MAX)
                    {
                        m_inputHorizontalLCnt++;
                    }
                }
                break;
            case PUSH_DIRECT.RIGHT:

                if (m_rotate == false)
                {

                    if (m_inputHorizontalRCnt == 0 || IMPUT_HORIZONTALCNT_MAX <= m_inputHorizontalRCnt)
                    {
                        movetmp = 50.0f;
                    }
                    m_rb.velocity = new Vector3(movetmp, -auto, 0);
                    m_inputHorizontalLCnt = 0;
                    if (m_inputHorizontalRCnt < IMPUT_HORIZONTALCNT_MAX)
                    {
                        m_inputHorizontalRCnt++;
                    }
                }
                break;
            case PUSH_DIRECT.DOWN:
                m_rb.velocity = new Vector3(0, -speed, 0);
                break;
#if false
            case PUSH_DIRECT.UP:
                m_rb.velocity = new Vector3(0, speed, 0);
                break;
#endif
            case PUSH_DIRECT.HOLD:
                //通常時以外は反応しない
                //if (Move.m_OperationState == Move.OperationState.OP_STATE_PLAY)
                //{
                //    mv.OperationStateSet(Move.OperationState.OP_STATE_HOLD, m_NowMino);
                //    FallBlock_Hold();
                //}
                break;
            default:
                m_rb.velocity = new Vector3(0, -1.5f, 0);
                break;
        }

        //ホールドの実行
        if( true == m_HoldExFlg )
        {
            if (Move.m_OperationState == Move.OperationState.OP_STATE_PLAY)
            {
                mv.OperationStateSet(Move.OperationState.OP_STATE_HOLD, m_NowMino);
                FallBlock_Hold();
                m_HoldExFlg = false;
            }
        }

        //ブロックとの接触を常にチェック
        m_colisionstay = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            if(true == transform.GetChild(i).gameObject.activeSelf)
            {
                Collider[] colList = Physics.OverlapBox(transform.GetChild(i).gameObject.transform.position, new Vector3(0.5f, 0.5f, 0.5f));

                foreach (Collider item in colList)
                {
                    if (item.gameObject.tag == "OverlapCheck")
                    {
                    }

                    if (item.gameObject.tag == "UpHitCheck")
                    {
                        m_RigidityCnt++;
                        m_colisionstay = true;
                        break;
                    }

                }
            }

            if(m_colisionstay == true)
            {
                break;
            }
        }

        //回転
        if (m_rotate == true)
        {
            FallBlock_SetRotateDirection();


            m_rotateInterval++;
            transform.Rotate(new Vector3(0, 0, 10 * m_rotateDirection));

            if (9 <= m_rotateInterval)
            {
                m_rotateInterval = 0;
                m_rotate = false;

                //-------------------------------------------------------
#if true
                m_Roatate++;
                if (3 < m_Roatate)
                {
                    m_Roatate = 0;
                }



                FallBlock_Rotate();
                //m_PreAngles = transform.localEulerAngles;

                for (int i = 0; i < 5; i++)
                {

                    for (int j = 0; j < 5; j++)

                    {
                        m_FallBlock[i, j] = m_FallBlockTemp[i, j];
                    }
                }
#endif
                //-------------------------------------------------------
            }

            ///--------------------------------------
            for (int i = 0; i < transform.childCount; i++)
            {
                int count = 0;
                if (true == transform.GetChild(i).gameObject.activeSelf)
                {
                    Collider[] colList = Physics.OverlapBox(transform.GetChild(i).gameObject.transform.position, new Vector3(0.5f, 0.5f, 0.5f));

                    foreach (Collider item in colList)
                    {
                        if (item.gameObject.tag == "OverlapCheck")
                        {
                            count++;
                            if (1 <= count)
                            {
                                m_rotate = false;
                                m_rotateInterval = 0;
                                transform.localEulerAngles = m_PreAngles;

                                break;
                            }
                        }

                    }
                }

            }
            ///-------------------------------------

        }

        if (m_colisionstay == false)
        {
            m_RigidityCnt = 0;
        }

        if(m_rotate == false && RIGIDITY_MAX_CONT <= m_RigidityCnt)
        {
            Vector3 vec3tmp = transform.position;
            // 小数第一位で四捨五入
            int x = (int)System.Math.Round(vec3tmp.x, System.MidpointRounding.AwayFromZero);
            int y = (int)System.Math.Round(vec3tmp.y, System.MidpointRounding.AwayFromZero);

            //mv.StaticBlockSet(x, y, m_FallBlock.m_NowMino, m_StaticBlock);
            mv.StaticBlockSet(x, y, m_NowMino);

            var test = transform.gameObject.name;

            m_colisionstay = false;
            m_HoldFlg = false;
            m_RigidityCnt = 0;
        }

        if(m_rotate == true)
        {
            m_RigidityCnt = 0;
        }

        //イレギュラー処理
        //範囲外のブロック処理
        if(transform.position.y <= -2)
        {
            var setpos = new Vector3(5.0f, 22.0f, 0.0f);
            transform.position = setpos;
        }

        //デバッグ出力-------------------------------------------------
#if DEBUG_ON
        //m_DebugText.SetText("" + m_colisionstay + " "+ m_RigidityCnt + " " + m_inputHorizontal + " " +m_inputVertical + " " + m_inputHorizontalRCnt + " "+ m_inputHorizontalLCnt + " " +transform.localEulerAngles, 23.0f, 0.0f);
        var str = "";
        //デバッグ表示
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (m_FallBlock[i, j] == 1)
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
        m_DebugText.SetText(str + "\n\n" + transform.position, 37.0f, 0.0f);
#endif
        //--------------------------------------------------------------

    }

    /// <summary>
    /// 回転方向の設定
    /// </summary>
    void FallBlock_SetRotateDirection()
    {
        int NextRoatate = m_Roatate + 1;

        const int NEXTROATATE_00 = 4;
        const int NEXTROATATE_01 = 1;
        const int NEXTROATATE_02 = 2;
        const int NEXTROATATE_03 = 3;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //Iミノ
                if (m_NowMino == I_MINO)
                {
                    if (NextRoatate == NEXTROATATE_00)
                    {
                       // m_FallBlockTemp[i, j] = I_MINO_TBL1[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_01)
                    {
                        //m_FallBlockTemp[i, j] = I_MINO_TBL2[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_02)
                    {
                       // m_FallBlockTemp[i, j] = I_MINO_TBL3[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_03)
                    {
                       // m_FallBlockTemp[i, j] = I_MINO_TBL4[i, j];
                        m_rotateDirection = -1;
                    }

                }

                //Tミノ
                else if (m_NowMino == T_MINO)
                {
                    if (NextRoatate == NEXTROATATE_00)
                    {
                        //m_FallBlockTemp[i, j] = T_MINO_TBL1[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_01)
                    {
                       // m_FallBlockTemp[i, j] = T_MINO_TBL2[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_02)
                    {
                        //m_FallBlockTemp[i, j] = T_MINO_TBL3[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_03)
                    {
                       // m_FallBlockTemp[i, j] = T_MINO_TBL4[i, j];
                        m_rotateDirection = -1;
                    }
                }

                //Jミノ
                else if (m_NowMino == J_MINO)
                {
                    if (NextRoatate == NEXTROATATE_00)
                    {
                        //m_FallBlockTemp[i, j] = J_MINO_TBL1[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_01)
                    {
                        //m_FallBlockTemp[i, j] = J_MINO_TBL2[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_02)
                    {
                        //m_FallBlockTemp[i, j] = J_MINO_TBL3[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_03)
                    {
                        //m_FallBlockTemp[i, j] = J_MINO_TBL4[i, j];
                        m_rotateDirection = -1;
                    }
                }

                //Lミノ
                else if (m_NowMino == L_MINO)
                {
                    if (NextRoatate == NEXTROATATE_00)
                    {
                       // m_FallBlockTemp[i, j] = L_MINO_TBL1[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_01)
                    {
                       // m_FallBlockTemp[i, j] = L_MINO_TBL2[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_02)
                    {
                       // m_FallBlockTemp[i, j] = L_MINO_TBL3[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_03)
                    {
                       // m_FallBlockTemp[i, j] = L_MINO_TBL4[i, j];
                        m_rotateDirection = -1;
                    }
                }

                //Sミノ
                else if (m_NowMino == S_MINO)
                {
                    if (NextRoatate == NEXTROATATE_00)
                    {
                       // m_FallBlockTemp[i, j] = S_MINO_TBL1[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_01)
                    {
                       // m_FallBlockTemp[i, j] = S_MINO_TBL2[i, j];
                        m_rotateDirection = 1;
                    }
                    else if (NextRoatate == NEXTROATATE_02)
                    {
                       // m_FallBlockTemp[i, j] = S_MINO_TBL1[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_03)
                    {
                       // m_FallBlockTemp[i, j] = S_MINO_TBL2[i, j];
                        m_rotateDirection = 1;
                    }
                }


                //Zミノ
                else if (m_NowMino == Z_MINO)
                {
                    if (NextRoatate == NEXTROATATE_00)
                    {
                       // m_FallBlockTemp[i, j] = Z_MINO_TBL1[i, j];
                        m_rotateDirection = 1;
                    }
                    else if (NextRoatate == NEXTROATATE_01)
                    {
                       // m_FallBlockTemp[i, j] = Z_MINO_TBL2[i, j];
                        m_rotateDirection = -1;
                    }
                    else if (NextRoatate == NEXTROATATE_02)
                    {
                       // m_FallBlockTemp[i, j] = Z_MINO_TBL1[i, j];
                        m_rotateDirection = 1;
                    }
                    else if (NextRoatate == NEXTROATATE_03)
                    {
                        //m_FallBlockTemp[i, j] = Z_MINO_TBL2[i, j];
                        m_rotateDirection = -1;
                    }
                }

                //Oミノ
                else if (m_NowMino == O_MINO)
                {
                    //m_FallBlockTemp[i, j] = O_MINO_TBL1[i, j];
                    m_rotateDirection = 0;
                }

            }


        }
    }

    void FallBlock_Rotate()
    {
        //            private const int I_MINO = 0;
        //    private const int T_MINO = 1;
        //    private const int O_MINO = 2;
        //    private const int S_MINO = 3;
        //    private const int Z_MINO = 4;



        //    private int m_NowMino = I_MINO;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //Iミノ
                if (m_NowMino == I_MINO)
                {
                    if (m_Roatate == 0)
                    {
                        m_FallBlockTemp[i, j] = I_MINO_TBL1[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                    else if (m_Roatate == 1)
                    {
                        m_FallBlockTemp[i, j] = I_MINO_TBL2[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 270.0f);
                    }
                    else if (m_Roatate == 2)
                    {
                        m_FallBlockTemp[i, j] = I_MINO_TBL3[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 180.0f);
                    }
                    else if (m_Roatate == 3)
                    {
                        m_FallBlockTemp[i, j] = I_MINO_TBL4[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 90.0f);
                    }

                }

                //Tミノ
                else if (m_NowMino == T_MINO)
                {
                    if (m_Roatate == 0)
                    {
                        m_FallBlockTemp[i, j] = T_MINO_TBL1[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                    else if (m_Roatate == 1)
                    {
                        m_FallBlockTemp[i, j] = T_MINO_TBL2[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 270.0f);
                    }
                    else if (m_Roatate == 2)
                    {
                        m_FallBlockTemp[i, j] = T_MINO_TBL3[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f,180.0f);
                    }
                    else if (m_Roatate == 3)
                    {
                        m_FallBlockTemp[i, j] = T_MINO_TBL4[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 90.0f);
                    }
                }

                //Jミノ
                else if (m_NowMino == J_MINO)
                {
                    if (m_Roatate == 0)
                    {
                        m_FallBlockTemp[i, j] = J_MINO_TBL1[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                    else if (m_Roatate == 1)
                    {
                        m_FallBlockTemp[i, j] = J_MINO_TBL2[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 270.0f);
                    }
                    else if (m_Roatate == 2)
                    {
                        m_FallBlockTemp[i, j] = J_MINO_TBL3[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 180.0f);
                    }
                    else if (m_Roatate == 3)
                    {
                        m_FallBlockTemp[i, j] = J_MINO_TBL4[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 90.0f);
                    }
                }

                //Lミノ
                else if (m_NowMino == L_MINO)
                {
                    if (m_Roatate == 0)
                    {
                        m_FallBlockTemp[i, j] = L_MINO_TBL1[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                    else if (m_Roatate == 1)
                    {
                        m_FallBlockTemp[i, j] = L_MINO_TBL2[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 270.0f);
                    }
                    else if (m_Roatate == 2)
                    {
                        m_FallBlockTemp[i, j] = L_MINO_TBL3[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 180.0f);
                    }
                    else if (m_Roatate == 3)
                    {
                        m_FallBlockTemp[i, j] = L_MINO_TBL4[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 90.0f);
                    }
                }

                //Sミノ
                else if (m_NowMino == S_MINO)
                {
                    if (m_Roatate == 0)
                    {
                        m_FallBlockTemp[i, j] = S_MINO_TBL1[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                    else if (m_Roatate == 1)
                    {
                        m_FallBlockTemp[i, j] = S_MINO_TBL2[i, j];
                        m_rotateDirection = 1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 90.0f);
                    }
                    else if (m_Roatate == 2)
                    {
                        m_FallBlockTemp[i, j] = S_MINO_TBL1[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                    else if (m_Roatate == 3)
                    {
                        m_FallBlockTemp[i, j] = S_MINO_TBL2[i, j];
                        m_rotateDirection = 1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 90.0f);
                    }
                }


                //Zミノ
                else if (m_NowMino == Z_MINO)
                {
                    if (m_Roatate == 0)
                    {
                        m_FallBlockTemp[i, j] = Z_MINO_TBL1[i, j];
                        m_rotateDirection = 1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                    else if (m_Roatate == 1)
                    {
                        m_FallBlockTemp[i, j] = Z_MINO_TBL2[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 270.0f);
                    }
                    else if (m_Roatate == 2)
                    {
                        m_FallBlockTemp[i, j] = Z_MINO_TBL1[i, j];
                        m_rotateDirection = 1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                    else if (m_Roatate == 3)
                    {
                        m_FallBlockTemp[i, j] = Z_MINO_TBL2[i, j];
                        m_rotateDirection = -1;
                        m_PreAngles = new Vector3(0.0f, 0.0f, 270.0f);
                    }
                }

                //Oミノ
                else if (m_NowMino == O_MINO)
                {
                    m_FallBlockTemp[i, j] = O_MINO_TBL1[i, j];
                    m_rotateDirection = 0;
                    m_PreAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }

            }


        }


        byte r;
        byte g;
        byte b;

        switch (m_NowMino)
        {
            case I_MINO:
                r = 47;
                g = 202;
                b = 253;
                break;
            case T_MINO:
                r = 253;
                g = 47;
                b = 248;
                break;
            case O_MINO:
                r = 250;
                g = 240;
                b = 50;
                break;
            case J_MINO:
                r = 47;
                g = 57;
                b = 253;
                break;
            case L_MINO:
                r = 254;
                g = 166;
                b = 46;
                break;
            case S_MINO:
                r = 62;
                g = 242;
                b = 57;
                break;
            case Z_MINO:
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

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color32(r, g, b, 255);
        }

    }
}

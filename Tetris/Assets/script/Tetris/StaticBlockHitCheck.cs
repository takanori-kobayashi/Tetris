using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 硬直したブロックが落下したブロックと接触した時の処理
/// </summary>
public class StaticBlockHitCheck : MonoBehaviour
{
    private const int MAX_TIME = 100;

    public TetrisDebugText m_DebugText;

    //static bool m_CollisionStay;
    //static bool m_CollisionStay2;

    public GameObject m_FallBlockObject;
    public GameObject m_move;
    public GameObject m_StaticBlock;
    static GameObject refObj_GameMain;
    static GameObject refObj_FallBlocks;
    static GameObject refObj_Text;
    static Move mv;
    private FallBlock m_FallBlock;

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < m_FallBlockObject.transform.childCount; i++)
       // {
            co = m_FallBlockObject.transform.GetChild(12).gameObject.GetComponent<Collider>();
        //}

        //GameMainのオブジェクト情報取得
        refObj_GameMain = GameObject.Find("GameMain");
        mv = refObj_GameMain.GetComponent<Move>();

        //FallBlocksのオブジェクト情報取得
        refObj_FallBlocks = GameObject.Find("FallBlocks");
        m_FallBlock = refObj_FallBlocks.GetComponent<FallBlock>();

            //デバッグ
        refObj_Text = GameObject.Find("DebugText");
        m_DebugText = refObj_Text.GetComponent<TetrisDebugText>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(m_CollisionStay == true)
        //{
        //    m_CollisionStay2 = true;
        //}
        //else
        //{
        //    m_CollisionStay2 = false;
        //}

    }
    Collider co;

    private void FixedUpdate()
    {
        // Vector3 hitPos = co.ClosestPointOnBounds(refObj_FallBlocks.transform.position);

        // if (hitPos != null)
        // {
        //    count++;
        // }
        /*
         if (co.gameObject.tag == "FallBlock")
         {
             count++;
         }

         if (m_CollisionStay == true)
         {
             count++;
         }
         else
         {
             m_CollisionStay = false;
             count = 0;
         }
         */

        Collider[] colList = Physics.OverlapBox(transform.position, new Vector3(0.0f, 0.5f, 0.0f));

        //m_CollisionStay = false;

        foreach (Collider item in colList)
        {

            if (item.gameObject.tag == "FallBlock")
            {
               // count++;
               // m_CollisionStay = true;
                ////説明２
                //if (item.transform.position == transform.position + new Vector3(0, -1, 0))
                //{
                //    countA++;  //説明３
                //}
                //if (item.transform.position == transform.position + new Vector3(-1, -1, 0))
                //{
                //    countA++;
                //}
                //if (item.transform.position == transform.position + new Vector3(-1, 0, 0))
                //{
                //    countA++;
                //}
                break;
            }

        }


        //ブロックが回転中は硬直させない
        if (m_FallBlock.m_rotate == false && MAX_TIME <= count)
        {
            //Vector3 vec3tmp = refObj.transform.position;
            Vector3 vec3tmp = refObj_FallBlocks.transform.position;
            // 小数第一位で四捨五入
            int x = (int)Math.Round(vec3tmp.x, MidpointRounding.AwayFromZero);
            int y = (int)Math.Round(vec3tmp.y, MidpointRounding.AwayFromZero);

            //mv.StaticBlockSet(x, y, m_FallBlock.m_NowMino, m_StaticBlock);
            mv.StaticBlockSet(x, y, m_FallBlock.m_NowMino);

            var test = transform.gameObject.name;

            //m_CollisionStay = false;
            count = 0;
        }

       // else
       // {
       //     m_CollisionStay = true;
       // }

        //if(m_CollisionStay == false)
        //{
        //    count = 0;
       // }
    }

    static int count = 0;

    //private void OnCollisionEnter(Collision collision)
    //private void OnCollisionStay(Collision collision)
    private void OnTriggerStay(Collider collision)
    {

#if false
        if (collision.gameObject.tag == "FallBlock")
        {
            //ブロックが回転中は硬直させない
            if (m_FallBlock.m_rotate == false && MAX_TIME <= count)
            {
                //Vector3 vec3tmp = refObj.transform.position;
                Vector3 vec3tmp = refObj_FallBlocks.transform.position;
                // 小数第一位で四捨五入
                int x = (int)Math.Round(vec3tmp.x, MidpointRounding.AwayFromZero);
                int y = (int)Math.Round(vec3tmp.y, MidpointRounding.AwayFromZero);

                mv.StaticBlockSet(x, y, m_FallBlock.m_NowMino, m_StaticBlock);
                var test = transform.gameObject.name;

                m_CollisionStay = false;
                count = 0;
            }
            else
            { 
            m_CollisionStay = true;
            }
        }
#else
        //if (collision.gameObject.tag == "FallBlock")
        //{
        //    //ブロックが回転中は硬直させない
        //    if (m_FallBlock.m_rotate == false && MAX_TIME <= count)
        //    {
        //        //Vector3 vec3tmp = refObj.transform.position;
        //        Vector3 vec3tmp = refObj_FallBlocks.transform.position;
        //        // 小数第一位で四捨五入
        //        int x = (int)Math.Round(vec3tmp.x, MidpointRounding.AwayFromZero);
        //        int y = (int)Math.Round(vec3tmp.y, MidpointRounding.AwayFromZero);

        //        mv.StaticBlockSet(x, y, m_FallBlock.m_NowMino, m_StaticBlock);
        //        var test = transform.gameObject.name;

        //        m_CollisionStay = false;
        //        count = 0;
        //    }
        //    else
        //    {
        //        m_CollisionStay = true;
        //    }
        //}
#endif




    }

    private void OnTriggerExit(Collider other)
    {
    //    m_CollisionStay = false;
    //    RigidityTime = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
     //   m_CollisionStay = false;
     //   RigidityTime = 0;
    }
}

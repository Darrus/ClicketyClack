/**
 * @file : RockAreaManager.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 洞窟の岩の管理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : 洞窟の岩の管理するクラス
 */
public class RockAreaManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Rocks;          ///< 岩を収納 
    private List<GameObject> exitRocks;          ///< 岩を収納 
    public static bool _isStop = false;            ///< 電車が停止していたら
    public bool T = true;                                 ///< 岩があるかどうか


    private void Start()
    {
        exitRocks = new List<GameObject>();
        _isStop = false;
    }

    /**
    *   @brief   毎フレーム呼び出される関数
    *   @param  nothing
    *   @return nothing 
*/
    private void Update()
    {
        if (_isStop)
        {
        }
        // 電車を発車させる
        if (exitRocks.Count == Rocks.Length)
        {
            _isStop = false;
            ArrowControll.RockComp = true;
            if (LevelManager.Singleton != null)
                LevelManager.Instance.Play = true;

        }
    }

///**
//*   @brief   衝突している間呼ばれる関数
//*   @param  nothing
//*   @return nothing 
//*/
//    private void OnTriggerStay(Collider col)
//    {
//        bool temp =true;

//        // 岩がある場合True
//        for (int i = 0; i < 7; i++)
//        {
//            if (col.gameObject == Rocks[i])
//            {
//                temp = true;
//                break;
//            }
//            else
//            {
//                temp = false;
//            }
//        }

//        T = temp;

//    }

/**
*   @brief   衝突から離れた時呼ばれる関数
*   @param  nothing
*   @return nothing 
*/
    void OnTriggerExit(Collider col)
    {
        // 岩が出ていったらfalse
        for (int i = 0; i < 7; i++)
        {
            if (col.gameObject == Rocks[i])
            {
                exitRocks.Add(Rocks[i]);
            }
        }
    }

/**
*   @brief   衝突から離れた時呼ばれる関数
*   @param  nothing
*   @return nothing 
*/
    void OnTriggerEnter(Collider col)
    {
        // 岩が出ていったらfalse
        for (int i = 0; i < 7; i++)
        {
            if (col.gameObject == Rocks[i])
            {
                exitRocks.Remove(Rocks[i]);
            }
        }
    }
}

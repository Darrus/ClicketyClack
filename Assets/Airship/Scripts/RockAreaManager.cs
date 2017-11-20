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
    private GameObject[] Rocks = null;          ///< 岩を収納 
    public static bool _isStop = false;            ///< 電車が停止していたら
    public bool T = true;                                 ///< 岩があるかどうか

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    private void Update()
    {
        // 電車を発車させる
        if (!T)
        {
            _isStop = false;
            LevelManager.Instance.MoveOut = true;
            ArrowControll.RockComp = true;
        }
    }

/**
*   @brief   衝突している間呼ばれる関数
*   @param  nothing
*   @return nothing 
*/
    private void OnTriggerStay(Collider col)
    {
        // 岩がある場合True
        for (int i = 0; i < 7; i++)
        {
            if (col.gameObject == Rocks[i])
            {
                T = true;
                break;
            }
        }
    }

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
                T = false;
            }
        }
    }
}

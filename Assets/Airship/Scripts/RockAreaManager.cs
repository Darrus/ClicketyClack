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
    private GameObject[] Rocks;                      ///< 岩を収納 
    private List<GameObject> exitRocks;          ///< 岩を収納 
    public static bool _isStop = false;               ///< 電車が停止していたら
    public bool T = true;                                    ///< 岩があるかどうか


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

        // 電車を発車させる
        if (exitRocks.Count == Rocks.Length)
        {
            _isStop = false;

            ArrowControll.RockComp = true;

            if (LevelManager.Singleton != null)
                LevelManager.Instance.Play = true;
        }
    }

/**
*   @brief   衝突から離れた時呼ばれる関数
*   @param  nothing
*   @return nothing 
*/
    void OnTriggerExit(Collider col)
    {
        // 衝突から離れた時
        for (int i = 0; i < 7; i++)
        {
            if (col.gameObject == Rocks[i])
            {
                // リストに追加
                exitRocks.Add(Rocks[i]);
            }
        }
    }

/**
*   @brief   衝突したとき呼び出される関数
*   @param  衝突したオブジェクト
*   @return nothing 
*/
    void OnTriggerEnter(Collider col)
    {
        // 衝突したとき
        for (int i = 0; i < 7; i++)
        {

            if (col.gameObject == Rocks[i])
            {
                // リストから外す
                exitRocks.Remove(Rocks[i]);
            }
        }
    }
}

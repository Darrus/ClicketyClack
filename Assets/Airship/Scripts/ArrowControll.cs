/**
 * @file : ArrowControll.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 矢印のアニメーション座標の制御
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : 矢印のアニメーション座標を管理するクラス
 */
public class ArrowControll : MonoBehaviour
{
    public float height;                                    ///< 高さ

    public GameObject WayPoint_0;               ///< 電車の座標
    public GameObject Coal;                           ///< 石炭の座標
    public GameObject RockPos;                     ///< 洞窟の座標

    ///< チュートリアルギミックの完了確認
    public static bool coalComp;
    public static bool cargoComp;
    public static bool ConnectComp;
    public static bool RockComp;

/**
 *   @brief   デバッグ時に１フレームだけ呼び出される関数
 *   @param  nothing
 *   @return nothing 
*/
    void Start()
    {
        coalComp = false;
        cargoComp = false;
        ConnectComp = false;
        RockComp = false;

        // 石炭の座標
        transform.position = new Vector3(Coal.transform.position.x, height, Coal.transform.position.z);
    }

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    private void Update()
    {
        // 電車の座標
        if (coalComp)
        {
            transform.position = new Vector3(WayPoint_0.transform.position.x, height, WayPoint_0.transform.position.z);
            coalComp = false;
        }

        // 待機座標
        if (ConnectCargo._isConnect)
        {
            transform.position = new Vector3(0, 0, 10000);
            ConnectCargo._isConnect = false;
        }

        // 電車が洞窟前で停車したら
        if (CargoCollision._isArrow)
        {
            transform.position = new Vector3(RockPos.transform.position.x, height, RockPos.transform.position.z);
            CargoCollision._isArrow = false;
        }
        // 岩をどけ終わったら
        if (RockComp)
        {
            Destroy(this.gameObject);
        }
    }
}

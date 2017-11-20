/**
 * @file : ConnectCargo.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 貨物の連結
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : 貨物の接続を管理するクラス
 */
public class ConnectCargo : MonoBehaviour
{
    public static bool _isConnect = false;          ///< 接続したかどうか

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    private void Update()
    {
        // 接続したら
        if (_isConnect)
        {

            //Destroy(connector);

            // 電車を発進
            LevelManager.Instance.CargoOn = true;
            
            // 移動先を設定
            this.GetComponent<TrainMovement2>().enabled = true;

            // テキストの変更
            TextControll.textNum = 2;
        }
    }

/**
*   @brief   衝突した時に呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    private void OnTriggerEnter(Collider col)
    {
        // コネクターとあたったら
        if (col.gameObject.tag == "Connector")
        {
            // 接続する
            _isConnect = true;

            // 矢印の座標管理
            ArrowControll.ConnectComp = true;
        }
    }
}
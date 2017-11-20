/**
 * @file : ObjectBillboard.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : オブジェクトのビルボード
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : オブジェクトをビルボードするクラス
 */
public class ObjectBillboard : MonoBehaviour
{
    private Vector3 Target;             ///< ターゲット

/**
*   @brief   デバッグ時に１フレームだけ呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    private void Start()
    {
        // カメラ座標
        Target = Camera.main.transform.position;
    }

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    void Update ()
    {
        Target = Camera.main.transform.position;

        // カメラの座標を見る
        transform.LookAt(Target);
	}
}
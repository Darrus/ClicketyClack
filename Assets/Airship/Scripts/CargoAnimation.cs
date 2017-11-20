/**
 * @file : CargoAnimation.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 貨物のアニメーション制御
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : 貨物のアニメーションのタイミングを管理するクラス
 */
public class CargoAnimation : MonoBehaviour
{
    private Animation anim;             /// アニメーション制御

/**
 *   @brief   デバッグ時に１フレームだけ呼び出される関数
 *   @param  nothing
 *   @return nothing 
*/
    void Start ()
    {
        // アニメーションの取得
        anim = GetComponent<Animation>();

        // アニメーション停止
        anim.Stop();
    }

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    void Update ()
    {
        // 貨物が連結されたら
        if (LevelManager.CargoOn)
        {
            // アニメーション開始
            anim.Play();
        }
    }
}

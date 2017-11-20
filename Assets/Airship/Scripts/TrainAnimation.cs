/**
 * @file : TrainAnimation.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 矢印のアニメーション座標の制御
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : 電車のアニメーションを管理するクラス
 */
public class TrainAnimation : MonoBehaviour
{
    private Animation anim;                 ///< アニメーション

	void Start ()
    {
        // アニメーションの取得
        anim = GetComponent<Animation>();

        // アニメーションの停止
        anim.Stop();
    }
	
	void Update ()
    {
        // 貨物が取り付けられたら
        if (LevelManager.CargoOn)
        {
            // アニメーションを始める
            anim.Play();
        }
        // もし電車が停止したら
        if (LevelManager.ReachStation && LevelManager.MoveOut)
        {
            // アニメーションの停止
            anim.Stop();
        }
    }
}

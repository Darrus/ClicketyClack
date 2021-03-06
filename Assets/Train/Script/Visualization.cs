﻿/**
 * @file : Visualization.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 可視化
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : 可視化をコントロールする
 */
public class Visualization : MonoBehaviour
{
    public GameObject[] victim;                      ///< Victimを収納
    public bool _isTutorial = false;                  ///< TutorialMode

/**
*   @brief   衝突したとき呼び出される関数
*   @param  衝突したオブジェクト
*   @return nothing 
*/
    private void OnTriggerEnter(Collider other)
    {
        // 衝突したとき
        for (int i = 0; i < victim.Length; i++)
        {
            if (other.gameObject == victim[i])
            {
                // 子のメッシュを取得
                SkinnedMeshRenderer visible = victim[i].GetComponentInChildren<SkinnedMeshRenderer>();
                SpriteRenderer spriteUI = victim[i].GetComponentInChildren<SpriteRenderer>();

                // 可視化
                visible.enabled = true;
                spriteUI.enabled = true;

                if (!_isTutorial)
                {
                    // AIのスクリプトをオンにする
                    victim[i].GetComponent<HumanAI>().enabled = true;
                }
            }
        }
    }


/**
*   @brief   衝突から離れたとき呼び出される関数
*   @param  衝突したオブジェクト
*   @return nothing 
*/
    private void OnTriggerExit(Collider other)
    {
        // チュートリアルモードでないとき
        if (!_isTutorial)
        {
            // 衝突したとき
            for (int i = 0; i < victim.Length; i++)
            {
                if (other.gameObject == victim[i])
                {
                    // 子のメッシュを取得
                    SkinnedMeshRenderer visible = victim[i].GetComponentInChildren<SkinnedMeshRenderer>();
                    SpriteRenderer spriteUI = victim[i].GetComponentInChildren<SpriteRenderer>();

                    // 不可視化
                    visible.enabled = false;
                    spriteUI.enabled = false;

                    // AIのスクリプトをオフにする
                    victim[i].GetComponent<HumanAI>().enabled = false;
                }
            }
        }
    }
}
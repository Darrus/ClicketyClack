/**
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
    public GameObject[] victim; ///< Victimを収納


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
                // 子のスキンメッシュを取得
                SkinnedMeshRenderer visible = victim[i].GetComponentInChildren<SkinnedMeshRenderer>();

                // 可視化
                visible.enabled = true;

                // AIのスクリプトをオンにする
                victim[i].GetComponent<HumanAI>().enabled = true;
            }
        }
    }
}
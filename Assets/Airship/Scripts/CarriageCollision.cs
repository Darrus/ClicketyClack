/**
 * @file : CarriageCollision.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 列車の当たり判定
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageCollision : MonoBehaviour
{

    public GameObject[] victim;                     ///< Victimを収納

 /**
 *   @brief   衝突したとき呼び出される関数
 *   @param  衝突したオブジェクト
 *   @return nothing 
*/
    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したとき
        for (int i = 0; i < victim.Length; i++)
        {
            if (collision.gameObject == victim[i])
            {
                // カウント
                CreateCount._isTri = true;

                // 削除
                Destroy(victim[i]);


                VictimManager.VictimRemain_Level[(int)(AppManager.Instance.gameState) - 1] -= 1;

                if(GameBoard.Singleton != null)
                GameBoard.Instance.UpdateVictimText();
            }
        }
    }
}

/**
 * @file : DestroyPoint.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief :落下地点の削除
 */

using UnityEngine;

public class DestroyPoint : MonoBehaviour
{
/**
 *   @brief  衝突したときに呼び出される関数
 *   @param  nothing
 *   @return nothing 
*/
    private void OnTriggerEnter(Collider col)
    {
        // 弾とあたったら
        if (col.gameObject.CompareTag("Bullet"))
        {
            // 削除
            Destroy(this.gameObject);
        }
    }
}


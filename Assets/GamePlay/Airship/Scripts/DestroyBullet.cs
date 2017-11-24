/**
 * @file : DestroyBullet.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 発射された弾の制御
 */

using UnityEngine;

/**
 * @brief : : 発射された弾の削除処理を管理するクラス
 */
public class DestroyBullet : MonoBehaviour
{
    private float delayTime = 10.0f;                     ///< 待機時間

    public GameObject Parent;                           ///< 親子
    public GameObject ParticleEffect;               ///< エフェクト
    public GameObject ParticleEffect2;             ///< エフェクト2
    public PlaySoundAtPosition explosionSFX;    ///< 爆発
    public GameObject m_target;                       ///< ターゲット

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    void Update()
    {
        //  時間計測
        delayTime -= Time.deltaTime;

        // 指定された時間が経過したら
        if (delayTime < 0)
        {
            // オブジェクトの親を削除
            Destroy(Parent);

            // 指定したオブジェクトの削除
            Destroy(m_target);

            // タイマーの再設定
            delayTime = 3.0f;
        }
    }

/**
*   @brief   衝突したときに呼び出される関数
*   @param  衝突したオブジェクト
*   @return nothing 
*/
    void OnTriggerEnter(Collider col)
    {
        // 落下地点か床に当たったら
        if (col.CompareTag("FallPoint")|| col.CompareTag("Ground"))
        {
            // プレハブからエフェクトの生成
            GameObject.Instantiate(ParticleEffect, transform.position, Quaternion.identity);

            // エフェクトの再生座標を指定
            explosionSFX.PlayAtPosition();

            // 親と指定されたオブジェクトを削除
            Destroy(Parent);
            Destroy(m_target);
        }
        // 電車に当たったら
        if (col.gameObject.tag == "Player")
        {
            // プレハブからエフェクトの生成
            GameObject.Instantiate(ParticleEffect2, transform.position, Quaternion.identity);
            
            // エフェクトの再生座標を指定
            explosionSFX.PlayAtPosition();

            if (GameBoard.Singleton != null)
            {
                if (col.gameObject.name == "head")
                {
                    GameBoard.Instance.TheTrainLife.killHead();
                }
                if (col.gameObject.name == "carriage")
                {
                    GameBoard.Instance.TheTrainLife.KillCarriage();
                }
                if (col.gameObject.name == "cargo")
                {
                    GameBoard.Instance.TheTrainLife.KillCargo();
                }
            }

            // 親と指定されたオブジェクトを削除
            Destroy(Parent);
            Destroy(m_target);
        }
    }
}

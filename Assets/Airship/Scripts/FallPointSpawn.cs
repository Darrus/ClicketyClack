/**
 * @file : FallPointSpawn.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 落下ポイントの生成
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : 落下ポイントを生成するクラス
 */
public class FallPointSpawn : MonoBehaviour
{
    public GameObject fallpointPrefab;              ///< 落下ポイントプレハブ

    private GameObject activeFallPoint = null;   ///< FallPointがアクティブかどうか
    private Vector3 FallPos;                               ///< 出現座標
    private float createTime;                             ///< 出現時間
    private int rate;                                            ///< 確率

/**
*   @brief   デバッグ時に１フレームだけ呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    private void Start()
    {
        // 初期値３秒
        createTime = 3.0f;
    }

    /**
    *   @brief   毎フレーム呼び出される関数
    *   @param  nothing
    *   @return nothing 
*/
    void Update()
    {
        if (AirShipPatrol.CanShot)
        {
            // 飛行船が発射可能になった時
            if (AirShipPatrol.CanShot && LevelManager.Singleton != null && GameBoard.Singleton != null)
            {
                if (LevelManager.Instance.Play && LevelManager.Instance.MoveOut &&
                        !LevelManager.Instance.ReachStation && GameBoard.Instance.TheTrainLife.Life > 0)
                {
                    // FallPoint出現
                    CreateFallpoint();
                }

            }
        }
    }
/**
*   @brief   FallPointの生成する関数
*   @param  nothing
*   @return nothing 
*/
    private void CreateFallpoint()
    {
        // 時間を図る
        createTime -= Time.deltaTime;

        // 指定された時間が０になったら
        if (createTime < 0)
        {
            // 乱数
            rate = Random.Range(1, 5);

            // 電車が次に目指すレール上に落下地点をスポーン
            // 20% HIT
            if (rate > 1)
            {
                // ランダムスポーン
                FallPos = transform.position + RandomPosition();
            }
            else
            {
                // 電車の正面の座標
                FallPos = transform.position + transform.forward / 2f;
            }

            FallPos.y += 0.01f;

            // 落下地点の生成
            activeFallPoint = Instantiate((GameObject)fallpointPrefab, FallPos, Quaternion.identity);

            // 時間をランダムにリセット
            createTime = CreateTime();
        }
    }

/**
*   @brief   ランダム座標関数
*   @param  nothing
*   @return Vector3 
*/
    public Vector3 RandomPosition()
    {
        float levelSize = 0.5f;
        return new Vector3(Random.Range(-levelSize, levelSize), transform.position.y + 0.5f, Random.Range(-levelSize, levelSize));
    }

/**
*   @brief   生成時間関数
*   @param  nothing
*   @return Float 
*/
    private float CreateTime()
    {
        return Random.Range(5.0f, 10.0f);
    }
}

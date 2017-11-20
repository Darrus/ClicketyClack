/**
 * @file : AirshipPatrol.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 飛行船の移動制御
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : 飛行船の移動方法、弾の発射を管理するクラス
 */
public class AirShipPatrol : MonoBehaviour
{
    static public bool CanShot = true;          ///< 弾が発射可能か

    public float speed = 1f;                            ///< 移動速度
    public GameObject target;                      ///< 追いかけるオブジェクト

    private bool _isSnag = false;                  ///< 障害物があるかどうか
    private bool _isDeng = false;                  ///< 危険かどうか 
    private float dist;                                   ///< 距離
    private float firstHeight = 0.5f;             ///< 初期高度

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    void Update()
    {
        // 危険だった場合
        if (_isDeng)
        {
            // 避ける
            AvoidMove();
        }
        else
        {
            // 避けながら進む
            AvoidMove();
            FollowObject();
        }
    }


    /**
    *   @brief   飛行船がターゲットにむかって移動する関数
    *   @param  nothing
    *   @return nothing 
    */
    void FollowObject()
    {
        // ターゲットの座標取得
        Vector3 targetPos = target.transform.position;

        // 移動
        transform.position
            = Vector3.MoveTowards
            (this.transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), speed * Time.deltaTime);

        // Y軸を無理やり固定
        Vector3 Temp = new Vector3
            (target.transform.localPosition.x, transform.position.y, target.transform.localPosition.z);
        transform.LookAt(Temp);
    }

    /**
    *   @brief   飛行船の目前にオブジェクトがあったら上昇する関数
    *   @param  nothing
    *   @return nothing 
    */
    void AvoidMove()
    {
        // 上昇
        if (_isSnag)
        {
            transform.position += new Vector3(0, 0.001f, 0);
        }
        // 危なくなかったら下降
        else if(!_isDeng)
        {
            transform.position += new Vector3(0, -0.001f, 0);
             
            // 初期高度よりは下降しない
            if (transform.position.y < firstHeight)
            {
                transform.position = new Vector3(transform.position.x, firstHeight, transform.position.z);
            }
        }
    }

/**
*   @brief   オブジェクトと当たり続けているときに呼ばれる関数
*   @param  衝突したオブジェクト
*   @return nothing 
*/
    private void OnTriggerStay(Collider other)
    {
        // 障害物に当たっていたら
        if (other.gameObject.tag == "Obstacle")
        {
            _isSnag = true;
            CanShot = false;

            dist = Vector3.Distance(transform.position, other.gameObject.transform.position);
            if (dist < 1f)
            {
                _isDeng = true;
            }
        }
    }

/**
*   @brief   衝突から離れた時に呼び出される関数
*   @param  衝突したオブジェクト
*   @return nothing 
*/
    private void OnTriggerExit(Collider other)
    {
        CanShot = true;
        _isSnag = false;

        // 部屋の最大高度
        if (dist > 1.6f)
        {
            _isDeng = false;
        }
    }
}



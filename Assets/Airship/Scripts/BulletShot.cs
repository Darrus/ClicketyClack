/**
 * @file : BulletShot.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 弾の発射
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : 弾の発射を管理するクラス
 */
public class BulletShot : MonoBehaviour
{
    
    public GameObject ParticleEffect;                      ///< 爆発エフェクト

    [SerializeField]
    private Transform m_shootPoint = null;               ///< 発射される場所

    [SerializeField]
    private GameObject m_shootObject = null;        ///< 発射するオブジェクト

    [SerializeField]
    private float m_Time = 1.0f;                               ///< 発射する時間

    private GameObject m_target = null;                 ///< 標的
    private GameObject m_bullet = null;                  ///< 弾

    private bool isShot;                                           ///< 発射しているか
    private float shootDelay = 1.0f;                         ///< 発射間隔時間

/**
 *   @brief   毎フレーム呼び出される関数
 *   @param  nothing
 *   @return nothing 
*/
    private void Update()
    {
        // 発射間隔
        shootDelay -= Time.deltaTime;

        // 時間を計測
        if (shootDelay > 0)
        {
            // 標的のオブジェクト登録
            m_target = GameObject.FindGameObjectWithTag("FallPoint");

            // 更新
            shootDelay = 4.0f;
        }

        // 標的が見つかったら
        if (m_target != null)
        {
            // 発射命令
            isShot = true;

            // 弾のオブジェクト登録
            m_bullet = GameObject.FindGameObjectWithTag("Bullet");
        }

        // もし発射命令がされていてかつ弾のオブジェクトがシーンになかったら
        if (isShot == true && m_bullet == null)
        {
            // 発射
            Shoot(m_target.transform.position);

            // 発射エフェクト
            play_Particle_Effect();

            // １度だけ行う
            isShot = false;  
        }
        else
        {
            // オブジェクトをもう一度検索し２発発射を防ぐ
            isShot = false;
        }
    }



/**
 *   @brief   弾を発射する関数
 *   @param  目標座標
 *   @return nothing 
*/
    private void Shoot(Vector3 i_targetPosition)
    {
        // Nullチェック
        if (m_shootObject == null)
        {
            throw new System.NullReferenceException("m_shootObject");
        }

        if (m_shootPoint == null)
        {
            throw new System.NullReferenceException("m_shootPoint");
        }

        // 発射角度を修正
        ShootFixedAngle(i_targetPosition, m_Time);
    }

/**
 *   @brief   発射角度を修正する関数
 *   @param  目標座標 , 時間
 *   @return nothing 
*/
    private void ShootFixedAngle(Vector3 i_targetPosition, float i_time)
    {

        float speedVec = ComputeVectorFromTime(i_targetPosition, i_time);
        float angle = ComputeAngleFromTime(i_targetPosition, i_time);

        if (speedVec <= 0.0f)
        {
            // その位置に着地させることは不可能
            Debug.LogWarning("!!");
            return;
        }

        Vector3 vec = ConvertVectorToVector3(speedVec, angle, i_targetPosition);

        InstantiateShootObject(vec);
    }

/**
 *   @brief   平面の距離を測りVector3に変換する関数
 *   @param  発射初期位置 , 角度 , 目標座標
 *   @return vec 
*/
    private Vector3 ConvertVectorToVector3(float i_v0, float i_angle, Vector3 i_targetPosition)
    {
        // 初期位置と目標座標を取得
        Vector3 startPos = m_shootPoint.transform.position;
        Vector3 targetPos = i_targetPosition;
        startPos.y = 0.0f;
        targetPos.y = 0.0f;

        // 平面の距離を取得
        Vector3 dir = (targetPos - startPos).normalized;

        // 回転軸をリセット(親またはワールドの)
        Quaternion yawRot = Quaternion.FromToRotation(Vector3.right, dir);

        Vector3 vec = i_v0 * Vector3.right;

        vec = yawRot * Quaternion.AngleAxis(i_angle, Vector3.forward) * vec;

        return vec;
    }

/**
 *   @brief   弾の生成を管理し生成する関数
 *   @param  発射ベクトル
 *   @return vec 
*/
    private void InstantiateShootObject(Vector3 i_shootVector)
    {
        // Nullチェック
        if (m_shootObject == null)
        {
            throw new System.NullReferenceException("m_shootObject");
        }

        if (m_shootPoint == null)
        {
            throw new System.NullReferenceException("m_shootPoint");
        }

        // 弾の生成
        var obj = Instantiate<GameObject>(m_shootObject, m_shootPoint.position, Quaternion.identity);
        
        // RigidBodyをアタッチ
        var rigidbody = obj.AddComponent<Rigidbody>();

        // 力(速さ×重さ)に変換
        Vector3 force = i_shootVector * rigidbody.mass;
        rigidbody.AddForce(force, ForceMode.Impulse);
        
        var temp = GameObject.FindGameObjectWithTag("Bullet");
        temp.GetComponent<DestroyBullet>().m_target = m_target;
    }

/**
 *   @brief   関数
 *   @param  目標座標 , 時間
 *   @return vec 
*/
    private float ComputeVectorFromTime(Vector3 i_targetPosition, float i_time)
    {
        Vector2 vec = ComputeVectorXYFromTime(i_targetPosition, i_time);

        float v_x = vec.x;
        float v_y = vec.y;

        float v0Square = v_x * v_x + v_y * v_y;

        // 負数を平方根計算すると虚数になってしまう。
        // 虚数はfloatでは表現できない
        if (v0Square <= 0.0f)
        {
            return 0.0f;
        }

        float v0 = Mathf.Sqrt(v0Square);

        return v0;
    }

 /**
  *   @brief   VxとVyから角度を求める関数
  *   @param  目標座標 , 時間
  *   @return angle 
*/
    private float ComputeAngleFromTime(Vector3 i_targetPosition, float i_time)
    {
        Vector2 vec = ComputeVectorXYFromTime(i_targetPosition, i_time);

        float v_x = vec.x;
        float v_y = vec.y;

        float rad = Mathf.Atan2(v_y, v_x);
        float angle = rad * Mathf.Rad2Deg;

        return angle;
    }

/**
 *   @brief   三平方の定理からVx , Vyを求める関数
 *   @param  目標座標 , 時間
 *   @return Vx , Vy 
*/
    private Vector2 ComputeVectorXYFromTime(Vector3 i_targetPosition, float i_time)
    {
        // 瞬間移動を制限
        if (i_time <= 0.0f)
        {
            return Vector2.zero;
        }
        
        // xz平面の距離を計算
        Vector2 startPos = new Vector2(m_shootPoint.transform.position.x, m_shootPoint.transform.position.z);
        Vector2 targetPos = new Vector2(i_targetPosition.x, i_targetPosition.z);
        float distance = Vector2.Distance(targetPos, startPos);

        float x = distance;

        // 重力を反転
        float g = -Physics.gravity.y;
        float y0 = m_shootPoint.transform.position.y;
        float y = i_targetPosition.y;
        float t = i_time;

        float v_x = x / t;
        float v_y = (y - y0) / t + (g * t) / 2;

        return new Vector2(v_x, v_y);
    }

/**
 *   @brief   エフェクトを再生する関数
 *   @param  nothing
 *   @return nothing
*/
    private void play_Particle_Effect()
    {
        ParticleEffect.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
    }

}


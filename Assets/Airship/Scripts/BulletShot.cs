using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    public GameObject ParticleEffect;

    [SerializeField]
    private Transform m_shootPoint = null;

    [SerializeField]
    private GameObject m_shootObject = null;

    [SerializeField]
    private float m_Time = 1.0f;

    private GameObject m_target = null;
    private GameObject m_bullet = null;

    private bool isShot;
    private float shootDelay = 1.0f;

    private void Update()
    {
        shootDelay -= Time.deltaTime;

        if (shootDelay > 0)
        {
            m_target = GameObject.FindGameObjectWithTag("FallPoint");
            shootDelay = 4.0f;
        }

        if (m_target != null)
        {
            isShot = true;
            m_bullet = GameObject.FindGameObjectWithTag("Bullet");
        }

        if (isShot == true && m_bullet == null)
        {
            //Shoot(m_target.transform.position);
            Shoot(m_target.transform.position);
            play_Particle_Effect();
            isShot = false;  
        }
        else
        {
            isShot = false;
        }
    }



    private void Shoot(Vector3 i_targetPosition)
    {
        if (m_shootObject == null)
        {
            throw new System.NullReferenceException("m_shootObject");
        }

        if (m_shootPoint == null)
        {
            throw new System.NullReferenceException("m_shootPoint");
        }

        ShootFixedAngle(i_targetPosition, m_Time);
    }

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

    private Vector3 ConvertVectorToVector3(float i_v0, float i_angle, Vector3 i_targetPosition)
    {
        Vector3 startPos = m_shootPoint.transform.position;
        Vector3 targetPos = i_targetPosition;
        startPos.y = 0.0f;
        targetPos.y = 0.0f;

        Vector3 dir = (targetPos - startPos).normalized;
        Quaternion yawRot = Quaternion.FromToRotation(Vector3.right, dir);
        Vector3 vec = i_v0 * Vector3.right;

        vec = yawRot * Quaternion.AngleAxis(i_angle, Vector3.forward) * vec;

        return vec;
    }

    private void InstantiateShootObject(Vector3 i_shootVector)
    {
        if (m_shootObject == null)
        {
            throw new System.NullReferenceException("m_shootObject");
        }

        if (m_shootPoint == null)
        {
            throw new System.NullReferenceException("m_shootPoint");
        }

        var obj = Instantiate<GameObject>(m_shootObject, m_shootPoint.position, Quaternion.identity);
        
        var rigidbody = obj.AddComponent<Rigidbody>();

        // 速さベクトルのままAddForce()を渡してはいけないぞ。力(速さ×重さ)に変換するんだ
        Vector3 force = i_shootVector * rigidbody.mass;
        rigidbody.AddForce(force, ForceMode.Impulse);


        var temp = GameObject.FindGameObjectWithTag("Bullet");
        temp.GetComponent<DestroyBullet>().m_target = m_target;
    }
    private float ComputeVectorFromTime(Vector3 i_targetPosition, float i_time)
    {
        Vector2 vec = ComputeVectorXYFromTime(i_targetPosition, i_time);

        float v_x = vec.x;
        float v_y = vec.y;

        float v0Square = v_x * v_x + v_y * v_y;

        // 負数を平方根計算すると虚数になってしまう。
        // 虚数はfloatでは表現できない。
        // こういう場合はこれ以上の計算は打ち切ろう。
        if (v0Square <= 0.0f)
        {
            return 0.0f;
        }

        float v0 = Mathf.Sqrt(v0Square);

        return v0;
    }

    private float ComputeAngleFromTime(Vector3 i_targetPosition, float i_time)
    {
        Vector2 vec = ComputeVectorXYFromTime(i_targetPosition, i_time);

        float v_x = vec.x;
        float v_y = vec.y;

        float rad = Mathf.Atan2(v_y, v_x);
        float angle = rad * Mathf.Rad2Deg;

        return angle;
    }

    private Vector2 ComputeVectorXYFromTime(Vector3 i_targetPosition, float i_time)
    {
        // 瞬間移動
        if (i_time <= 0.0f)
        {
            return Vector2.zero;
        }


        // xz平面の距離を計算。
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

    private void play_Particle_Effect()
    {
        ParticleEffect.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
    }

}


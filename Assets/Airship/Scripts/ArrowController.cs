using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float time;
    private GameObject Cargo;
    public static bool _isDead = false;

    private Vector3 StartPos;
    private Vector3 EndPos;
    private Vector3 deltaPos;
    private float elapsedTime;
    private bool bStartToEnd = true;

    // Initialization
    void Start()
    {
        Cargo = GameObject.FindGameObjectWithTag("Cargo");

        StartCoroutine("MovePosition");

        // 1秒当たりの移動量を算出
        deltaPos = (EndPos - StartPos) / time;
        elapsedTime = 0;
    }

    void Update()
    {
        if (CreateArrow._isSleep)
        {
            StartCoroutine("MovePosition");

        }
        if (_isDead)
        {
            Destroy(this.gameObject);
        }

        RepetitiveMotion();
    }

    private IEnumerator MovePosition()
    {
        yield return new WaitForSeconds(0.5f);

        StartPos = new Vector3
            (Cargo.transform.position.x, (Cargo.transform.position.y + 0.2f), Cargo.transform.position.z);

        EndPos = new Vector3
            (Cargo.transform.position.x, (Cargo.transform.position.y + 0.5f), Cargo.transform.position.z);

    }

    private void RepetitiveMotion()
    {
        transform.position += deltaPos * Time.deltaTime;

        elapsedTime += Time.deltaTime;

        if (elapsedTime > time)
        {
            if (bStartToEnd)
            {
                deltaPos = (StartPos - EndPos) / time;

                transform.position = EndPos;
            }
            else
            {
                deltaPos = (EndPos - StartPos) / time;

                transform.position = StartPos;
            }

            bStartToEnd = !bStartToEnd;

            elapsedTime = 0;
        }

    }
}

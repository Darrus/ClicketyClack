using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoCollision : MonoBehaviour
{
    private float delayTime = 3.0f;

    private void Start()
    {

    }

    private void Update()
    {
        if (ArrowController._isDead)
        {
            delayTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (delayTime < 0||ArrowController._isDead)
            {
                ArrowController._isDead = false;
                CreateArrow._isSleep = true;
            }
        }
    }
}

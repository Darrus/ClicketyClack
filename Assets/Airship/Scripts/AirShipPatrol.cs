using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipPatrol : MonoBehaviour
{
    [SerializeField]
    private float speed =1.0f;

    public GameObject target;

    void Update()
    {
        // Homing
        Vector3 targetPos = target.transform.position;

        transform.position
            = Vector3.MoveTowards(this.transform.position, new Vector3(targetPos.x, transform.position.y,targetPos.z ), speed * Time.deltaTime);

        // Angle
        Vector3 Temp = new Vector3
            (target.transform.localPosition.x, transform.position.y, target.transform.localPosition.z);

        transform.LookAt(Temp);
    }
}


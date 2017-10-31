using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipPatrol : MonoBehaviour
{
    private float step;
    private bool _isSnag = false;
    public float firstHeight = 0.5f;

    public float speed = 1f;
    public GameObject target;

    private void Start()
    {
        step = speed * Time.deltaTime;
    }

    void Update()
    {
        FollowObject();

        BackDefault();
        
    }

    void FollowObject()
    {
        // Follow
        Vector3 targetPos = target.transform.position;

        transform.position
            = Vector3.MoveTowards
            (this.transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), speed * Time.deltaTime);

        // Angle
        Vector3 Temp = new Vector3
            (target.transform.localPosition.x, transform.position.y, target.transform.localPosition.z);

        transform.LookAt(Temp);
    }

    void BackDefault()
    {
        if (_isSnag)
        {
            transform.position += new Vector3(0, 0.001f, 0);
        }
        if (!_isSnag)
        {
            transform.position += new Vector3(0, -0.001f, 0);
             
            if (transform.position.y < firstHeight)
            {
                transform.position = new Vector3(transform.position.x, firstHeight, transform.position.z);
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        _isSnag = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isSnag = false;
    }
}

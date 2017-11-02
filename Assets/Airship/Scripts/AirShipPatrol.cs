using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipPatrol : MonoBehaviour
{
    static public bool CanShot = true;

    private float step;
    private bool _isSnag = false;
    private bool _isDeng = false;
    private float dist;

    private float firstHeight = 0.5f;
    public float speed = 1f;
    public GameObject target;

    void Update()
    {
        if (_isDeng)
        {
            AvoidMove();
        }
        else
        {
            AvoidMove();
            FollowObject();
        }
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

    void AvoidMove()
    {
        if (_isSnag)
        {
            // !
            transform.position += new Vector3(0, 0.001f, 0);
        }
        else if(!_isDeng)
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

    private void OnTriggerExit(Collider other)
    {
        CanShot = true;
        _isSnag = false;

        // Room Most Height
        if (dist > 1.6f)
        {
            _isDeng = false;
        }
    }
}



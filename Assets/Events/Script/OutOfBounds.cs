using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {
    [TagSelector]
    public string boundTag;

    private Vector3 position;
    private Quaternion rotation;

    private bool once;

    void Start ()
    {
        once = true;

    }

    void Update()
    {
        if (once)
        {
            position = transform.position;
            rotation = transform.rotation;
            once = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(boundTag))
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}

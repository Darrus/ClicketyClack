using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {
    [TagSelector]
    public string boundTag;

    private Vector3 position;
    private Quaternion rotation;

    void Start ()
    {
        position = transform.position;
        rotation = transform.rotation;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {
    [TagSelector]
    public string boundTag;

    [TagSelector]
    public string groundTag;

    private Vector3 position;
    private Quaternion rotation;

    private bool once;

    void Start ()
    {
       // once = true;

    }

    //void Update()
    //{
    //    if (once)
    //    {
    //        position = transform.position;
    //        rotation = transform.rotation;
    //        once = false;
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(boundTag))
        {
            Vector3 height = new Vector3(0.0f, 0.2f, 0.0f);
            transform.position = position + height;
            transform.rotation = rotation;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(groundTag))
        {
            position = transform.position;
            rotation = transform.rotation;
        }
    }
}

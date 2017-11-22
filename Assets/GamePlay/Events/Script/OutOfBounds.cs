/** 
 *  @file    OutOfBounds.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains out of bound script
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 *  @brief   returns the object back to it's last position on the ground when it flys out of bounds
 */
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

    /** 
     *  @brief   Returns the object to it's last saved location upon exiting the out of bound trigger zone
     */
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

    /** 
     *  @brief   As long as the object is touching the ground, saves the object's spawn position as the touched position
     */
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(groundTag))
        {
            position = transform.position;
            rotation = transform.rotation;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildFollowParent : MonoBehaviour {

    private bool once;

    void Awake()
    {
        once = true;


    }
        // Use this for initialization
    void Start () {
        if (once && transform.parent != null)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.identity;
            once = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
      
	}
}

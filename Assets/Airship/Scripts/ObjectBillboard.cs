using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBillboard : MonoBehaviour
{
    private GameObject Target;


    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update
    void Update ()
    {
        Vector3 targetPos = new Vector3
            ((Target.transform.localPosition.x),
              transform.position.y,
             (Target.transform.localPosition.z));

        transform.LookAt(targetPos);
	}
}
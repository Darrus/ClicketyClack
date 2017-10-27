using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBillboard : MonoBehaviour
{
    public GameObject Target;

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
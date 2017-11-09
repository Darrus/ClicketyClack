using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBillboard : MonoBehaviour
{
    private Vector3 Target;


    private void Start()
    {
        Target = Camera.main.transform.position;
    }

    // Update
    void Update ()
    {
        Target = Camera.main.transform.position;
        transform.LookAt(Target);
	}
}
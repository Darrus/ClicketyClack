using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBillboard : MonoBehaviour
{
    public GameObject HoloLenzCamera;

	// Update
	void Update ()
    {
        Vector3 playerPos = new Vector3
            ((HoloLenzCamera.transform.localPosition.x*-1),
              HoloLenzCamera.transform.position.y,
             (HoloLenzCamera.transform.localPosition.z*-1));

        transform.LookAt(playerPos);
	}
}
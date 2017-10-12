using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement2 : MonoBehaviour {

    public TrainMovement PointFollow;
    public float TrainHeightGap;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (LevelManager.TrianConnected)
        {
            transform.position = new Vector3(PointFollow.transform.position.x, PointFollow.transform.position.y + TrainHeightGap, PointFollow.transform.position.z);
            transform.localRotation = PointFollow.transform.localRotation;
            //Vector3 lookPos = transform.position + BezierCurve2.Track_List[PointFollow.Point_ID].tangent;
            //var rotation = Quaternion.LookRotation(lookPos);
            //rotation *= Quaternion.Euler(0, -90, 0); // this adds a 90 degrees Y rotation
            //transform.localRotation = rotation;
        }
	}
}

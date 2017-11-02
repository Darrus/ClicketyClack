using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement2 : MonoBehaviour {

    public TrainMovement PointFollow;
    public GameObject ParticleEffect;
    public float TrainHeightGap;

    // Use this for initialization
    void Start () {
        ParticleEffect.GetComponentInChildren<ParticleSystem>().Stop();
    }
	
    // Update is called once per frame
    void Update () 
    {
        if (BezierCurve2.Go)
        {
            if (LevelManager.TrianConnected)
            {
                transform.position = new Vector3(PointFollow.transform.position.x, PointFollow.transform.position.y + TrainHeightGap, PointFollow.transform.position.z);
                transform.localRotation = PointFollow.transform.localRotation;
                //Vector3 lookPos = transform.position + BezierCurve2.TrackData_List[PointFollow.Point_ID].tangent;
                //var rotation = Quaternion.LookRotation(lookPos);
                //rotation *= Quaternion.Euler(0, -90, 0); // this adds a 90 degrees Y rotation
                //transform.localRotation = rotation;
                ParticleEffect.GetComponentInChildren<ParticleSystem>().Stop();
            }
            else
            {
                ParticleEffect.GetComponentInChildren<ParticleSystem>().Play();
                ParticleEffect.transform.rotation = Quaternion.identity;
            }
        }
        else if(LevelManager.CargoOn)
        {
            transform.position = new Vector3(PointFollow.transform.position.x, PointFollow.transform.position.y + TrainHeightGap, PointFollow.transform.position.z);
            transform.localRotation = PointFollow.transform.localRotation;
        }

    }
}

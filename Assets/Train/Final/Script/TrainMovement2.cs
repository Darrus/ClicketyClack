using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement2 : MonoBehaviour {

    public TrainMovement PointFollow;
    public GameObject ParticleEffect;
    public float TrainHeightGap;
    private bool connected;
    // Use this for initialization
    void Start () {
        ParticleEffect.GetComponentInChildren<ParticleSystem>().Stop();
        connected = true;
    }
	
    // Update is called once per frame
    void Update () 
    {
        if (BezierCurve2.Go)
        {
            if (connected)
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

            checkConnection();
        }
        else if(LevelManager.CargoOn)
        {
            transform.position = new Vector3(PointFollow.transform.position.x, PointFollow.transform.position.y + TrainHeightGap, PointFollow.transform.position.z);
            transform.localRotation = PointFollow.transform.localRotation;
        }
    }

    void checkConnection()
    {
        if(PointFollow.ID == 1)
            connected = LevelManager.TheTrainLife.head;
        if (PointFollow.ID == 2)
            connected = LevelManager.TheTrainLife.Carriage;
        if (PointFollow.ID == 3)
            connected = LevelManager.TheTrainLife.Cargo;
    }
}

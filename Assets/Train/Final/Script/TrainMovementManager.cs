using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct TrainPart
{
    public int ID;
    public float distanceGap; // gap from the front part of the train
}

public class TrainMovementManager : MonoBehaviour {
    
    public float MainSpeed;
    public float TotalTrackDistance;

    public TrainPart[] TheTrain;
    public bool once;

    // Use this for initialization
    void Start () {
        once = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!once)
        {
            TotalTrackDistance = BezierCurve2.Track_List[BezierCurve2.Track_List.Length - 1].distance;
            once = true;
        }
    }
}

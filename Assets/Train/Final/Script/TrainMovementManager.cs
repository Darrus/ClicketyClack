using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode()]
#endif
public class TrainMovementManager : MonoBehaviour {

    [System.Serializable]
    public struct TrainPart
    {
        public int ID;
        public float distanceGap; // gap from the front part of the train
    }

    public float MainSpeed;
    public float RenderSpeed;
    public float UnRenderSpeed;
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
        if (!once && BezierCurve2.Go)
        {
            TotalTrackDistance = BezierCurve2.TrackData_List[BezierCurve2.TrackData_List.Length - 1].distance;
            once = true;
        }
    }
}

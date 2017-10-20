using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode()]
#endif
public class PointManager : MonoBehaviour {

    public GameObject Point;

    public GameObject WayPointList;

    public int curveSteps;

    public int startSize;

    void Start () {
        BezierCurve2.CruveSteps = curveSteps;
        BezierCurve2.ClearAllData();
        BezierCurve2.IncreaseSize(startSize);
        BezierCurve2.updateCurvePoints();

        BezierCurve2.updateTrack = true;
        BezierCurve2.Go = false;

        BezierCurve2.CalcAllTrackLength();

    }
	
	void Update () {
        BezierCurve2.CruveSteps = curveSteps;

#if UNITY_EDITOR 
        if (Input.GetKeyDown("1"))
        {
            BezierCurve2.updateTrack = true;
            BezierCurve2.Go = false;
        }

        if (!Application.isPlaying)
        {
            BezierCurve2.ClearAllData();
            BezierCurve2.IncreaseSize(startSize);
            BezierCurve2.updateCurvePoints();
            BezierCurve2.CalcAllTrackLength();
            DrawLine_Edtior();
        }
#endif
    }

    void DrawLine_Edtior()
    {
        for (int i = 0; i < BezierCurve2.Track_List.Length; i++)
        {
            if (i != BezierCurve2.Track_List.Length - 1)
                Debug.DrawLine(BezierCurve2.Track_List[i].position, BezierCurve2.Track_List[i + 1].position, Color.cyan);
            else
                Debug.DrawLine(BezierCurve2.Track_List[i].position, BezierCurve2.Track_List[0].position, Color.cyan);
        }
    }

    public void AddNewPoints(Vector3 position, int ID,int type)
    {
        //BezierCurve2.addPoint_shiftID(ID);
        BezierCurve2.IncreaseSize(1);
        startSize += 1;

        GameObject myPoint = Instantiate(Point, position, Quaternion.identity);
        MainPoints newPoint = myPoint.GetComponent(typeof(MainPoints)) as MainPoints;
        newPoint.ID = ID;
        newPoint.type = type;
        myPoint.tag = "Point";
        myPoint.transform.parent = WayPointList.transform;

        BezierCurve2.updateCurvePoints();
        BezierCurve2.updateTrack = true;
        BezierCurve2.Go = false;
    }

}

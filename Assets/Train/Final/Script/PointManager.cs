using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PointManager : MonoBehaviour {

    public GameObject Point;

    private bool once;

    public int curveSteps;

    public int startSize;

    void Start () {
        once = true;
       
    }
	
	void Update () {
        BezierCurve2.CruveSteps = curveSteps;
        if (once)
        {
            BezierCurve2.ClearAllData();
            BezierCurve2.IncreaseSize(startSize);
            BezierCurve2.updateCurvePoints();

            BezierCurve2.updateTrack = true;
            BezierCurve2.Go = false;
            once = false;
        }


        if(Input.GetKeyDown("1"))
        {
            BezierCurve2.updateTrack = true;
            BezierCurve2.Go = false;
        }


	}

    public void AddNewPoints(Vector3 position, int ID,int type)
    {
        BezierCurve2.addPoint_shiftID(ID);
        BezierCurve2.IncreaseSize(1);

        GameObject myPoint = Instantiate(Point, position, Quaternion.identity);
        MainPoints newPoint = myPoint.GetComponent(typeof(MainPoints)) as MainPoints;
        newPoint.ID = ID;
        newPoint.type = type;
        myPoint.tag = "Point";
        myPoint.transform.parent = transform;

        BezierCurve2.updateCurvePoints();
        BezierCurve2.updateTrack = true;
        BezierCurve2.Go = false;
    }
}

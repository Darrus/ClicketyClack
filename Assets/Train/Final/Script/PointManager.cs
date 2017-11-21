/** 
*  @file    PointManager.cs
*  @author  Yin Shuyu (150713R) 
*  
*  @brief Contain class PointManager
*  
*/
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/**
*  @brief Class manage all the Mainpoint data
*/
#if UNITY_EDITOR
[ExecuteInEditMode()]
#endif
public class PointManager : MonoBehaviour {

    public GameObject Point; ///< GameObject prefab of a empty waypoint
    public GameObject WayPointList; ///< GameObject of all WayPoint's Parent 

    public int curveSteps;  ///< Default number of curveSteps

    public int startSize;  ///< size of the wayPoint List

    public int CurrentLevel; ///< current scene Level

    private Transform WayPointList_parent; ///< Transform of all WayPoint's Parent's Parent

    /**
    *  @brief Caculate All Track Point data once, and take WayPointList out from unactive parent to get data from all MainPoint once
    */
    void Start () {

        BezierCurve2.Go = false;
        BezierCurve2.CruveSteps = curveSteps;

        WayPointList_parent = WayPointList.transform.parent;
        WayPointList.transform.parent = null;
        BezierCurve2.ClearAllData();
        BezierCurve2.IncreaseSize(startSize);
        BezierCurve2.updateCurvePoints();
        BezierCurve2.CalcAllTrackPointData();
        WayPointList.transform.parent = WayPointList_parent;
        OrderExecution.Instance.Done = true;
    }

    void Update () {
      
    }

    /**
    *   @brief A function to Draw the Track in Line
    *  
    *   @return nothing
    */
#if UNITY_EDITOR
    public void DrawLine_Edtior()
    {
        for (int i = 0; i < BezierCurve2.TrackData_List.Length; i++)
        {
            if (i != BezierCurve2.TrackData_List.Length - 1)
                Debug.DrawLine(BezierCurve2.TrackData_List[i].position, BezierCurve2.TrackData_List[i + 1].position, Color.cyan);
            else
                Debug.DrawLine(BezierCurve2.TrackData_List[i].position, BezierCurve2.TrackData_List[0].position, Color.cyan);
        }
    }
#endif

    /**
    *   @brief A function to Add a new waypoint into the the WayPointList
    *  
    *   @param Vector3 position, position of the new waypoint
    *   
    *   @param  int ID, ID of the new waypoint
    *   
    *   @param MainPoints.pointType type, type of the new waypoint
    *   
    *   @return nothing
    */
    public void AddNewPoints(Vector3 position, int ID,MainPoints.pointType type)
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

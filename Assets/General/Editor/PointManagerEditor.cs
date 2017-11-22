/** 
*  @file    PointManagerEditor.cs
*  @author  Yin Shuyu (150713R) 
*  
*  @brief Contain Editor class PointManagerEditor
*  
*/
using UnityEngine;
using System.Collections;

using UnityEditor;

/**
*  @brief Editor Class for PointManager to Save and Load Data, Create new wayPoints and Track Line
*/
[CustomEditor(typeof(PointManager))]
public class PointManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PointManager myScript = (PointManager)target;
        
        /**
        *  @brief Create new wayPoint
        */
        if (GUILayout.Button("Create point"))
        {
            Vector3 Temp = Vector3.zero;
            if (BezierCurve2.points.Length > 1)
            {
                Temp = BezierCurve2.points[BezierCurve2.points.Length - 1] - (BezierCurve2.points[BezierCurve2.points.Length - 1] - BezierCurve2.points[0]) * 0.5f;
                Temp = Temp * 0.1f;
            } 

            myScript.AddNewPoints(Temp, BezierCurve2.points.Length, MainPoints.pointType.FixedPoint);
        }

        /**
        *  @brief Create Track Line
        */
        if (GUILayout.Button("Create line"))
        {
            BezierCurve2.CruveSteps = myScript.curveSteps;
            BezierCurve2.ClearAllData();
            BezierCurve2.IncreaseSize(myScript.startSize);
            BezierCurve2.updateCurvePoints();
            BezierCurve2.CalcAllTrackPointData();
            myScript.DrawLine_Edtior();
        }

        /**
        *  @brief Save Track Data
        */
        if (GUILayout.Button("Save Track Point Data"))
        {
            BezierCurve2.SaveTrackPointData(myScript.CurrentLevel);
        }

        /**
        *  @brief Load Track Data and Create Track Line
        */
        if (GUILayout.Button("Load Track Point Data && Create Line"))
        {
            BezierCurve2.LoadTrackPointData(myScript.CurrentLevel);
            myScript.DrawLine_Edtior();
        }

    }
}
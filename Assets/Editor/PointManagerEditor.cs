using UnityEngine;
using System.Collections;

#if !UNITY_WSA && UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(PointManager))]
public class PointManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PointManager myScript = (PointManager)target;
        if (GUILayout.Button("Create point"))
        {
            Vector3 Temp = Vector3.zero;
            if (BezierCurve2.points.Length > 1)
            {
                Temp = BezierCurve2.points[BezierCurve2.points.Length - 1] - (BezierCurve2.points[BezierCurve2.points.Length - 1] - BezierCurve2.points[0]) * 0.5f;
                Temp = Temp * 0.1f;
            } 

            myScript.AddNewPoints(Temp, BezierCurve2.points.Length, 1);
        }
    }
}
#endif
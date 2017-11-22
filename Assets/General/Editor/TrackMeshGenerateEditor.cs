/** 
*  @file    TrackMeshGenerateEditor.cs
*  @author  Yin Shuyu (150713R) 
*  
*  @brief Contain Editor class TrackMeshGenerateEditor
*  
*/
using UnityEngine;
using System.Collections;

using UnityEditor;


/**
*  @brief Editor Class for TrackMeshGenerate to Generate Track Mesh
*/
[CustomEditor(typeof(TrackMeshGenerate))]
public class TrackMeshGenerateEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TrackMeshGenerate myScript = (TrackMeshGenerate)target;

        /**
        *  @brief Generate Track Mesh individual parts
        */
        if (GUILayout.Button("Generate Track Mesh (many Parts)"))
        {
            BezierCurve2.LoadTrackPointData(myScript.Current_Level);
            myScript.GenerateMesh2();
        }

        /**
        *  @brief Generate Track Mesh in whole
        */
        if (GUILayout.Button("Generate Track Mesh(one P)"))
        {
            BezierCurve2.LoadTrackPointData(myScript.Current_Level);
            myScript.GenerateMesh();
        }
    }
}
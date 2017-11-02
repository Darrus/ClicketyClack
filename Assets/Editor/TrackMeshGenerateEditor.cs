using UnityEngine;
using System.Collections;

using UnityEditor;

[CustomEditor(typeof(TrackMeshGenerate))]
public class TrackMeshGenerateEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TrackMeshGenerate myScript = (TrackMeshGenerate)target;

        if (GUILayout.Button("Generate Track Mesh (many Parts)"))
        {
            BezierCurve2.LoadTrackPointData(myScript.Level);
            myScript.GenerateMesh2();
        }

        if (GUILayout.Button("Generate Track Mesh(one P)"))
        {
            BezierCurve2.LoadTrackPointData(myScript.Level);
            myScript.GenerateMesh();
        }
    }
}
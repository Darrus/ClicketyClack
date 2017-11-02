using UnityEngine;
using System.Collections;

#if !UNITY_WSA && UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(TrackMeshGenerate))]
public class TrackMeshGenerateEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TrackMeshGenerate myScript = (TrackMeshGenerate)target;
 
        if (GUILayout.Button("Generate Track Mesh"))
        {
            BezierCurve2.LoadTrackPointData(myScript.Level);
            myScript.GenerateMesh();
        }
    }
}
#endif
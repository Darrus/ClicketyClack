﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainPoints : MonoBehaviour {

    public enum pointType
    {
        None = 0,
        FixedPoint = 1,
        EventPoint = 2,
        NormalPoint = 3
    };

    public GameObject NormalPoint;
    //public GameObject FixedPoint;
    public GameObject EventPoint;


    [HideInInspector]
    public Vector3 ChildPoint_position;
    [HideInInspector]
    public Vector3 Normalized_For_Child; // tangent form parent and friend, using this to get child position
    [HideInInspector]
    public Vector3 Friend_ChildPoint_position;
    [HideInInspector]
    public Vector3 FriendPoint_position;
    
    public int type;
    public int ID;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // MESH VAR //
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public Material RoadMaterial;

    public Vector3[] CurveNodes
    {
        get { return curveNodes; }
        set { }
    }

    Vector3[] curveNodes;
    Vector3[] tangents;
    Vector3[] vertices;

    [HideInInspector]
    public bool UpdateMesh;

    [HideInInspector]
    public bool UnrenderMesh;

    // Use this for initialization
    void Start () {
        UpdateMesh = false;
        UnrenderMesh = true;

        if (type == (int)pointType.NormalPoint)
        {
            CreatePoints(NormalPoint);
            gameObject.name = ID.ToString() + "_Normal"; 
        }

        if (type == (int)pointType.FixedPoint)
        {
            gameObject.name = ID.ToString() + "_Fixed";
        }

        if (type == (int)pointType.EventPoint)
        {
            CreatePoints(EventPoint);
            gameObject.name = ID.ToString() + "_Event";
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (UpdateMesh)
        {
            curveNodes = new Vector3[BezierCurve2.CruveSteps + 1];
            tangents = new Vector3[BezierCurve2.CruveSteps + 1];
            vertices = new Vector3[(BezierCurve2.CruveSteps + 1) * 2];

            for (int i = 0; i <= BezierCurve2.CruveSteps; ++i)
            {

                if (type == (int)pointType.NormalPoint || type == (int)pointType.FixedPoint)
                {
                    Vector3 point = (BezierCurve2.GetPoint(transform.position * BezierCurve2.Distance_scaleFacter, ChildPoint_position * BezierCurve2.Distance_scaleFacter, Friend_ChildPoint_position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, i / (float)BezierCurve2.CruveSteps)) - transform.position * BezierCurve2.Distance_scaleFacter;
                    curveNodes[i] = point;

                    tangents[i] = BezierCurve2.GetFirstDerivative(transform.position * BezierCurve2.Distance_scaleFacter, ChildPoint_position * BezierCurve2.Distance_scaleFacter, Friend_ChildPoint_position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, i / (float)BezierCurve2.CruveSteps);
                }

                if (type == (int)pointType.EventPoint)
                {
                    Vector3 point = (BezierCurve2.GetPoint(transform.position * BezierCurve2.Distance_scaleFacter, transform.position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, i / (float)BezierCurve2.CruveSteps)) - transform.position * BezierCurve2.Distance_scaleFacter;
                    curveNodes[i] = point;

                    tangents[i] = BezierCurve2.GetFirstDerivative(transform.position * BezierCurve2.Distance_scaleFacter, transform.position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, i / (float)BezierCurve2.CruveSteps);
                }
            }

            BuildRoadMesh();

            UpdateMesh = false;

            if(UnrenderMesh)
            {
                GetComponent<Renderer>().enabled = false;
                UnrenderMesh = false;
            }
        }

        if(BezierCurve2.EnableTrackCollision)
            GetComponent<MeshCollider>().enabled = true;
        else
            GetComponent<MeshCollider>().enabled = false;

    }

    public void CreatePoints(GameObject temp)
    {
        GameObject myPoint = Instantiate(temp, transform.position, Quaternion.identity);
        myPoint.transform.parent = transform;
    }

    public void BuildRoadMesh()
    {

        // Get vertices for road edges
        MeshBuilder2 meshBuilder = new MeshBuilder2();

        for (int i = 0; i < curveNodes.Length; i++)
        {
            Vector3 cross = Vector3.Cross(Vector3.up, tangents[i]);
            cross = cross.normalized * 0.5f;
            vertices[i * 2] = curveNodes[i] + cross;
            vertices[i * 2 + 1] = curveNodes[i] - cross;

            Vector2 uv2 = new Vector2(0, (float)i / (float)BezierCurve2.CruveSteps * 5);
            Vector2 uv1 = new Vector2(1, (float)i / (float)BezierCurve2.CruveSteps * 5);

            meshBuilder.UVs.Add(uv1);
            meshBuilder.UVs.Add(uv2);
        }
        meshBuilder.Vertices.AddRange(vertices);

        // Build triangle list
        for (int i = 0; i < BezierCurve2.CruveSteps; i++)
        {
            int baseIndex = i * 2;
            meshBuilder.AddTriangle(baseIndex, baseIndex + 1, baseIndex + 2);

            meshBuilder.AddTriangle(baseIndex + 1, baseIndex + 3, baseIndex + 2);
        }
        Mesh mesh = meshBuilder.CreateMesh();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        mesh.name = "Test";


        MeshFilter filter = gameObject.GetComponent<MeshFilter>();

        filter.sharedMesh = mesh;

        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = filter.sharedMesh;

        //GetComponent<MeshCollider>().enabled = true;
        //GetComponent<MeshCollider>().convex = true;
        //GetComponent<MeshCollider>().isTrigger = true;

        GetComponent<Renderer>().material = RoadMaterial;
        GetComponent<Renderer>().enabled = true;
    }
}
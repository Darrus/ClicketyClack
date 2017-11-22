/** 
*  @file    MainPoints.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief Contain class MainPoints
*  
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*  @brief Class contain Track WayPoint data 
*/
#if UNITY_EDITOR
[ExecuteInEditMode()]
#endif
public class MainPoints : MonoBehaviour {

    /**
    *  @brief enum PointType, all types of Track WayPoint
    */
    public enum pointType
    {
        None = 0,
        FixedPoint = 1, 
        EventPoint = 2,
        NormalPoint = 3,
        TrafficLight = 4
    };

    //public GameObject NormalPoint; ///< GameObject prefab for NormalPoint type 
    //public GameObject FixedPoint; ///< GameObject prefab for FixedPoint type 
    //public GameObject EventPoint; ///< GameObject prefab for EventPoint type 
    public GameObject TrafficLight; ///< GameObject prefab for TrafficLight type 

    [HideInInspector]
    public Vector3 ChildPoint_position; ///< Vector3 Position of child (child determine the curve of this point)
    [HideInInspector]
    public Vector3 Normalized_For_Child; ///< Vector3 tangent of child position, using this to get child position
    [HideInInspector]
    public Vector3 Friend_ChildPoint_position; ///< Vector3 Position of Friend's child (next point's child determine the curve of next point)
    [HideInInspector]
    public Vector3 FriendPoint_position; ///< Vector3 Position of Friend (the next point, needed to determine the curve)

    public pointType type; ///< pointType of the waypoint
    public int ID; ///< ID of this waypoint
    public bool done; ///< bool a once run for pointType.TrafficLight

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // MESH VARs //
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public Material Track2DMaterial; ///< Material for the 2D Track

    Vector3[] curveNodes; ///< Array of Vector3 curve nodes for Generating the 2d track mesh
    Vector3[] tangents; ///< Array of Vector3 tangents for Generating the 2d track mesh
    Vector3[] vertices; ///< Array of Vector3 vertices points for Generating the 2d track mesh

    [HideInInspector]
    public bool UpdateMesh; ///< bool trigger for updating the mesh

    [HideInInspector]
    public bool UnrenderMesh; ///< bool trigger for whether to render the 2d track mesh

    /**
    *  @brief Set some bools, and change name and Create PointType Object
    */
    void Start()
    {
        if (Application.isPlaying)
        {
            done = false;
            UpdateMesh = true;
            UnrenderMesh = true;

            if (type == pointType.NormalPoint)
            {
                //CreatePoints(NormalPoint);
                gameObject.name = ID.ToString() + "_Normal";
            }

            if (type == pointType.FixedPoint)
            {
                gameObject.name = ID.ToString() + "_Fixed";
            }

            if (type == pointType.EventPoint)
            {
                gameObject.name = ID.ToString() + "_Event";
            }

            if (type == pointType.TrafficLight)
            {
                CreatePointTypeObject(TrafficLight);
                gameObject.name = ID.ToString() + "_TrafficLight";
               
            }
        }
    }

    /**
    *  @brief Update the 2D Track Mesh if UpdateMesh == true, and trigger MeshCollider
    */
    void Update () {

        if (Application.isPlaying)
        {
            if (UpdateMesh)
            {
                if (AppManager.Instance.gameState == AppManager.GameScene.Customization)
                {
                    curveNodes = new Vector3[BezierCurve2.CruveSteps + 1];
                    tangents = new Vector3[BezierCurve2.CruveSteps + 1];
                    vertices = new Vector3[(BezierCurve2.CruveSteps + 1) * 2];

                    for (int i = 0; i <= BezierCurve2.CruveSteps; ++i)
                    {

                        if (type == pointType.NormalPoint || type == pointType.FixedPoint || type == pointType.TrafficLight)
                        {
                            Vector3 point = (BezierCurve2.GetPoint(transform.position * BezierCurve2.Distance_scaleFacter, ChildPoint_position * BezierCurve2.Distance_scaleFacter, Friend_ChildPoint_position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, i / (float)BezierCurve2.CruveSteps)) - transform.position * BezierCurve2.Distance_scaleFacter;
                            curveNodes[i] = point;

                            tangents[i] = BezierCurve2.GetFirstDerivative(transform.position * BezierCurve2.Distance_scaleFacter, ChildPoint_position * BezierCurve2.Distance_scaleFacter, Friend_ChildPoint_position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, i / (float)BezierCurve2.CruveSteps);
                        }

                        if (type == pointType.EventPoint)
                        {
                            Vector3 point = (BezierCurve2.GetPoint(transform.position * BezierCurve2.Distance_scaleFacter, transform.position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, i / (float)BezierCurve2.CruveSteps)) - transform.position * BezierCurve2.Distance_scaleFacter;
                            curveNodes[i] = point;

                            tangents[i] = BezierCurve2.GetFirstDerivative(transform.position * BezierCurve2.Distance_scaleFacter, transform.position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, i / (float)BezierCurve2.CruveSteps);
                        }
                    }

                    BuildTrack2DMesh();
                }

                UpdateMesh = false;

                if (UnrenderMesh)
                {
                    GetComponent<Renderer>().enabled = false;
                    UnrenderMesh = false;
                }

                if (type == pointType.TrafficLight && !done)
                {
                    UpdateTrafficLightPoints();
                }

            }

            GetComponent<MeshCollider>().enabled = BezierCurve2.EnableTrackCollision;
        }
    }


    /**
    *   @brief A function to Create PointType gameobject prefab
    *  
    *   @param GameObject temp, which prefab to Instantiate
    *   
    *   @return nothing
    */
    public void CreatePointTypeObject(GameObject temp)
    {
        GameObject myPoint = Instantiate(temp, transform.position, Quaternion.identity);
        myPoint.transform.parent = transform;
    }

    /**
    *   @brief A function for TrafficLight prefab to update its position
    *  
    *   @return nothing
    */
    public void UpdateTrafficLightPoints()
    {
        GameObject Temp = gameObject.transform.GetChild(0).gameObject;
      
        Vector3 normal = Vector3.Cross(BezierCurve2.GetFirstDerivative(transform.position * BezierCurve2.Distance_scaleFacter, ChildPoint_position * BezierCurve2.Distance_scaleFacter, Friend_ChildPoint_position * BezierCurve2.Distance_scaleFacter, FriendPoint_position * BezierCurve2.Distance_scaleFacter, 0), Vector3.up).normalized;

        Temp.transform.position = Temp.transform.position - new Vector3(normal.x * 0.2f, 0, normal.z * 0.2f);  // hard coded

        Temp.transform.LookAt(transform.position);

        Temp.transform.position += new Vector3(0, 0.08f, 0); // hard coded

        FadeInTracks tempFadeSc = GameObject.FindObjectOfType<FadeInTracks>().GetComponent(typeof(FadeInTracks)) as FadeInTracks;
        tempFadeSc.GetPointObject(Temp, ID);

        done = true;
    }

    /**
    *   @brief A function to Generate 2D Track Mesh
    *  
    *   @return nothing
    */
    public void BuildTrack2DMesh()
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

        GetComponent<Renderer>().material = Track2DMaterial;
        GetComponent<Renderer>().enabled = true;
    }

    /**
    *   @brief to Draw Wire Sphere at waypoint
    */
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            if (type == pointType.TrafficLight)
                Gizmos.color = Color.red;

            if (type == pointType.FixedPoint || type == pointType.EventPoint)
                Gizmos.color = Color.black;


            Gizmos.DrawWireSphere(transform.position, 0.05f);

            Gizmos.DrawWireSphere(ChildPoint_position, 0.01f);
        }
    }
#endif

}

/** 
*  @file    TrackMeshGenerate.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief Contain class TrackMeshGenerate
*  
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
*  @brief Class that Gernerate Tracks Mesh From BezierCurze's TrackData 
*/
#if UNITY_EDITOR
using UnityEditor;

[ExecuteInEditMode()]
#endif
[RequireComponent(typeof(MeshFilter))]
public class TrackMeshGenerate : MonoBehaviour
{
    public float SteelRail_Gap; ///< Distance of inner Steel Rail from outter Steel Rail
    public float SteelRail_Width; ///< Width of Steel Rail
    public float SteelRail_Height; ///< Height of Steel Rail

    public float RailPlate_Gap; ///< Distance between Rail Plates
    public float RailPlate_Height; ///< Height of Rail Plate
    public float RailPlate_Width; ///< Width of Rail Plate
    public float RailPlate_Length; ///< Length of Rail Plate

    private int TotalWayPoints; ///< Number of WayPoints in TrackData
    private List<int> RailPlate_List; ///< List of Rail Plate's position TrackData 

    public Material SteelRail_Material; ///< Material of Steel Rail
    public Material RailPlate_Material; ///< Material of Rail Plate

    public int Current_Level; ///< int of current level

    public GameObject TrackFade_Parent; ///< Parent of TrackFade( Secondary track for visual effect )

    /**
    *  @brief update the 3D Track Mesh if all condition met
    */
    void Update()
    {
#if UNITY_EDITOR || UNITY_WSA

        if (BezierCurve2.updateTrack && BezierCurve2.points.Length > 2 && AppManager.Instance.gameState == AppManager.GameScene.Customization)
        {
            BezierCurve2.CalcAllTrackPointData();

            GenerateMesh();

            BezierCurve2.unRenderMesh();

            BezierCurve2.updateTrack = false;
            BezierCurve2.Go = true;
        }
#endif
    }

    /**
    *   @brief Generate Track Mesh  
    *   
    *   @return nothing 
    */
    public void GenerateMesh()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        TotalWayPoints = BezierCurve2.TrackData_List.Length;

        DrawTrack_Part1("inner", true);
        DrawTrack_Part1("outter", false);

        DrawTrack_Part2(true, gameObject);

    }


    /**
    *   @brief Generate the Mesh of All the Steel Rails of the Track
    *  
    *   @param string Name, the name of this part of track
    *   @param bool Inner, outter part of the track or inner part of the track
    *   
    *   @return nothing 
    */
    private void DrawTrack_Part1(string Name, bool Inner)
    {
        Vector3[] allPoints = new Vector3[TotalWayPoints];

        Vector3[] vertices = new Vector3[TotalWayPoints * 2];

        List<GameObject> objects = new List<GameObject>();

        for (int n = 0; n < 4; n++)
        {

            MeshBuilder2 meshBuilder = new MeshBuilder2();

            for (int i = 0; i < allPoints.Length; i++)
            {
                GameObject Temp = new GameObject();
                Temp.transform.position = BezierCurve2.TrackData_List[i].position;
                Temp.transform.LookAt(Temp.transform.position + BezierCurve2.TrackData_List[i].tangent);
                Vector3 TempR = Temp.transform.right;
                DestroyImmediate(Temp);

                Vector3 Offset = TempR * SteelRail_Gap;
                if (Inner)
                    Offset = -Offset;

                Vector3 cross = new Vector3();
                if (n == 0)
                {
                    allPoints[i] = BezierCurve2.TrackData_List[i].position + Vector3.up * SteelRail_Height + Offset;
                    cross = Vector3.Cross(Vector3.up, BezierCurve2.TrackData_List[i].tangent);

                    cross = cross.normalized;
                    vertices[i * 2] = allPoints[i] + cross * SteelRail_Width;
                    vertices[i * 2 + 1] = allPoints[i] - cross * SteelRail_Width;
                }
                if (n == 1)
                {
                    allPoints[i] = BezierCurve2.TrackData_List[i].position - Vector3.up * SteelRail_Height + Offset;
                    cross = Vector3.Cross(-Vector3.up, BezierCurve2.TrackData_List[i].tangent);

                    cross = cross.normalized;
                    vertices[i * 2] = allPoints[i] + cross * SteelRail_Width;
                    vertices[i * 2 + 1] = allPoints[i] - cross * SteelRail_Width;
                }

                if (n == 2)
                {
                    allPoints[i] = BezierCurve2.TrackData_List[i].position - TempR * SteelRail_Width + Offset;
                    cross = Vector3.Cross(-TempR, BezierCurve2.TrackData_List[i].tangent);

                    cross = cross.normalized;
                    vertices[i * 2] = allPoints[i] + cross * SteelRail_Height;
                    vertices[i * 2 + 1] = allPoints[i] - cross * SteelRail_Height;
                }
                if (n == 3)
                {
                    allPoints[i] = BezierCurve2.TrackData_List[i].position + TempR * SteelRail_Width + Offset;
                    cross = Vector3.Cross(TempR, BezierCurve2.TrackData_List[i].tangent);

                    cross = cross.normalized;
                    vertices[i * 2] = allPoints[i] + cross * SteelRail_Height;
                    vertices[i * 2 + 1] = allPoints[i] - cross * SteelRail_Height;
                }



                Vector2 uv2 = new Vector2(0, 1);
                Vector2 uv1 = new Vector2(1, 1);

                meshBuilder.UVs.Add(uv1);
                meshBuilder.UVs.Add(uv2);
            }

            meshBuilder.Vertices.AddRange(vertices);

            // Build triangle list
            for (int i = 0; i < allPoints.Length - 1; i++)
            {
                int baseIndex = i * 2;
                meshBuilder.AddTriangle(baseIndex, baseIndex + 1, baseIndex + 2);

                meshBuilder.AddTriangle(baseIndex + 1, baseIndex + 3, baseIndex + 2);
            }

            Mesh mesh = meshBuilder.CreateMesh();

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            GameObject piece = new GameObject();

            piece.AddComponent<MeshFilter>().sharedMesh = mesh;

            objects.Add(piece);
        }

        Mesh combinedMesh = CombineMeshes.Combine(objects);

        foreach (GameObject obj in objects)
        {
            DestroyImmediate(obj);
        }

        GameObject Final = new GameObject(Name);

        Final.transform.SetParent(transform);
        Final.transform.position = transform.position;

        Final.AddComponent<MeshFilter>().sharedMesh = combinedMesh;
        MeshRenderer rend = Final.AddComponent<MeshRenderer>();

        //#if UNITY_EDITOR
        //        combinedMesh.name = Name + "_" + Level.ToString();

        //        AssetDatabase.CreateAsset(combinedMesh, FileUtil.GetProjectRelativePath(Application.streamingAssetsPath + "/" + Level.ToString() + "/" + combinedMesh.name + ".asset"));
        //        AssetDatabase.SaveAssets();
        //#endif

        rend.sharedMaterial = SteelRail_Material;
    }

    /**
   *   @brief Generate the Mesh of All the Rail Plates of the Track
   *  
   *   @param bool One_P, to Generate all the Rail Plate In one Mesh or Get All the individual Mesh into a list of Rail Plates
   *   @param GameObject parent, A parent object to save the Mesh
   *   
   *   @return nothing if One_P is true
   *   @return List of GameObject if One_P is false
   */
    private List<GameObject> DrawTrack_Part2(bool One_P, GameObject parent)
    {
        GameObject Final = new GameObject("Rail");
        if (RailPlate_List == null)
            RailPlate_List = new List<int>();
        else
            RailPlate_List.Clear();

        Final.transform.SetParent(parent.transform);
        Final.transform.position = parent.transform.position;

        float MaxTrackLength = BezierCurve2.TrackData_List[TotalWayPoints - 1].distance;
        float currDistance = 0f;
        int currID = 0;


        //Debug.Log(MaxTrackLength);
        List<GameObject> objects = new List<GameObject>();

        bool run = true;

        while (run)
        {
            bool SecondRun = true;

            while (SecondRun)
            {

                if (currDistance >= BezierCurve2.TrackData_List[currID].distance)
                {
                    currID++;
                }

                if (currID >= TotalWayPoints - 1)
                {
                    SecondRun = false;
                }

                if (SecondRun)
                    if (currDistance < BezierCurve2.TrackData_List[currID + 1].distance)
                    {
                        DrawTrack_Part3(currID, One_P, Final, objects);
                        SecondRun = false;
                    }

            }

            if (currID >= TotalWayPoints - 1)
            {
                run = false;
            }
            currDistance += RailPlate_Gap;
        }

        if (One_P)
        {
            Mesh combinedMesh = CombineMeshes.Combine(objects);

            foreach (GameObject obj in objects)
            {
                DestroyImmediate(obj);
            }

            Final.AddComponent<MeshFilter>().sharedMesh = combinedMesh;

            MeshRenderer rend = Final.AddComponent<MeshRenderer>();

            //#if UNITY_EDITOR
            //            combinedMesh.name = Final.name + "_" + Level.ToString();
            //            AssetDatabase.CreateAsset(combinedMesh, FileUtil.GetProjectRelativePath(Application.streamingAssetsPath + "/" + Level.ToString() + "/" + combinedMesh.name + ".asset"));
            //            AssetDatabase.SaveAssets();
            //#endif

            rend.sharedMaterial = RailPlate_Material;

            return null;
        }
        else
        {
            return objects;
        }
    }

    /**
    *   @brief Generate the Indivi Mesh of the Rail Plate of the Track at a specific point
    *  
    *   @param int id, id of the point in Track Data
    *   @param bool One_P, if true Render is enable, if false Render is disable
    *   @param GameObject currParent, A parent object to Set its position and parent 
    *   @param List of GameObject ListObject, Stores all the Mesh of the Rail Plates
    *   
    *   @return nothing
    */
    private void DrawTrack_Part3(int id, bool One_P, GameObject currParent, List<GameObject> ListObjects)
    {
        GameObject Temp = new GameObject();
        Temp.transform.position = BezierCurve2.TrackData_List[id].position;
        Temp.transform.LookAt(Temp.transform.position + BezierCurve2.TrackData_List[id].tangent);
        Vector3 TempR = Temp.transform.right;
        DestroyImmediate(Temp);

        List<GameObject> objects = new List<GameObject>();

        for (int n = 0; n < 6; n++)
        {
            MeshBuilder2 meshBuilder = new MeshBuilder2();

            Vector3[] vertices = new Vector3[4];

            for (int i = 0; i < 2; i++)
            {
                Vector3 currPoint = BezierCurve2.TrackData_List[id].position;
                Vector3 currTangent = BezierCurve2.TrackData_List[id].tangent;

                if (i == 1)
                {
                    currPoint = BezierCurve2.TrackData_List[id].position + BezierCurve2.TrackData_List[id].tangent * RailPlate_Width;
                }


                Vector3 cross = new Vector3();

                if (n == 0)
                {
                    currPoint = currPoint + Vector3.up * RailPlate_Height;
                    cross = Vector3.Cross(Vector3.up, currTangent);

                    cross = cross.normalized;
                    vertices[i * 2] = currPoint + cross * RailPlate_Length;
                    vertices[i * 2 + 1] = currPoint - cross * RailPlate_Length;
                }
                if (n == 1)
                {
                    currPoint = currPoint - Vector3.up * RailPlate_Height;
                    cross = Vector3.Cross(-Vector3.up, currTangent);

                    cross = cross.normalized;
                    vertices[i * 2] = currPoint + cross * RailPlate_Length;
                    vertices[i * 2 + 1] = currPoint - cross * RailPlate_Length;
                }
                if (n == 2)
                {
                    currPoint = currPoint - TempR * RailPlate_Length;
                    cross = Vector3.Cross(-TempR, currTangent);

                    cross = cross.normalized;
                    vertices[i * 2] = currPoint + cross * RailPlate_Height;
                    vertices[i * 2 + 1] = currPoint - cross * RailPlate_Height;
                }
                if (n == 3)
                {
                    currPoint = currPoint + TempR * RailPlate_Length;
                    cross = Vector3.Cross(TempR, currTangent);

                    cross = cross.normalized;
                    vertices[i * 2] = currPoint + cross * RailPlate_Height;
                    vertices[i * 2 + 1] = currPoint - cross * RailPlate_Height;
                }
                if (n == 4)
                {
                    currPoint = BezierCurve2.TrackData_List[id].position;

                    if (i == 0)
                    {
                        currPoint = currPoint + Vector3.up * RailPlate_Height;
                        cross = Vector3.Cross(Vector3.up, -currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * RailPlate_Length;
                        vertices[i * 2 + 1] = currPoint - cross * RailPlate_Length;
                    }

                    if (i == 1)
                    {
                        currPoint = currPoint - Vector3.up * RailPlate_Height;
                        cross = Vector3.Cross(Vector3.up, -currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * RailPlate_Length;
                        vertices[i * 2 + 1] = currPoint - cross * RailPlate_Length;
                    }
                }
                if (n == 5)
                {
                    currPoint = BezierCurve2.TrackData_List[id].position + BezierCurve2.TrackData_List[id].tangent * RailPlate_Width;

                    if (i == 0)
                    {
                        currPoint = currPoint + Vector3.up * RailPlate_Height;
                        cross = Vector3.Cross(Vector3.up, currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * RailPlate_Length;
                        vertices[i * 2 + 1] = currPoint - cross * RailPlate_Length;
                    }

                    if (i == 1)
                    {
                        currPoint = currPoint - Vector3.up * RailPlate_Height;
                        cross = Vector3.Cross(Vector3.up, currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * RailPlate_Length;
                        vertices[i * 2 + 1] = currPoint - cross * RailPlate_Length;
                    }
                }




                Vector2 uv2 = new Vector2(0, 1);
                Vector2 uv1 = new Vector2(1, 1);

                meshBuilder.UVs.Add(uv1);
                meshBuilder.UVs.Add(uv2);
            }

            meshBuilder.Vertices.AddRange(vertices);

            // Build triangle list

            meshBuilder.AddTriangle(0, 1, 2);

            meshBuilder.AddTriangle(1, 3, 2);

            Mesh mesh = meshBuilder.CreateMesh();

            mesh.RecalculateNormals();

            GameObject piece = new GameObject();

            piece.AddComponent<MeshFilter>().sharedMesh = mesh;

            objects.Add(piece);
        }

        Mesh combinedMesh = CombineMeshes.Combine(objects);

        foreach (GameObject obj in objects)
        {
            DestroyImmediate(obj);
        }

        GameObject Final = new GameObject(id.ToString());
        RailPlate_List.Add(id);

        Final.transform.SetParent(currParent.transform);
        Final.transform.position = currParent.transform.position;

        Final.AddComponent<MeshFilter>().sharedMesh = combinedMesh;

        MeshRenderer rend = Final.AddComponent<MeshRenderer>();

        if (!One_P)
            rend.enabled = false;

        rend.sharedMaterial = RailPlate_Material;

        ListObjects.Add(Final);

    }

    /**
    *   @brief Generate the FadeTrack(A Secondary Track) for Visual Effect, which the track mesh is not all join together
    * 
    *   @return nothing
    */
    public void GenerateMesh2()
    {
        for (int i = TrackFade_Parent.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(TrackFade_Parent.transform.GetChild(i).gameObject);
        }

        TotalWayPoints = BezierCurve2.TrackData_List.Length;

        FadeInTracks FadeSc = TrackFade_Parent.GetComponent(typeof(FadeInTracks)) as FadeInTracks;

        FadeSc.Inner = DrawTrack_Part4("inner", true);
        FadeSc.Outter = DrawTrack_Part4("outter", false);

        FadeSc.Rail = DrawTrack_Part2(false, TrackFade_Parent);
        FadeSc.RailList = RailPlate_List;
    }

    /**
    *   @brief Generate A List of All the individual Mesh of the Steel Rail on the Track
    *  
    *   @param string Name, the name of this part of track
    *   @param bool Inner, outter part of the track or inner part of the track
    *   
    *   @return List of GameObject 
    */
    private List<GameObject> DrawTrack_Part4(string Name, bool Inner)
    {
        GameObject parent = new GameObject(Name);
        parent.transform.SetParent(TrackFade_Parent.transform);
        parent.transform.position = TrackFade_Parent.transform.position;

        List<GameObject> objectsList = new List<GameObject>();

        for (int id = 0; id < TotalWayPoints; id++)
        {
            GameObject Temp = new GameObject();
            Temp.transform.position = BezierCurve2.TrackData_List[id].position;
            Temp.transform.LookAt(Temp.transform.position + BezierCurve2.TrackData_List[id].tangent);
            Vector3 TempR = Temp.transform.right;
            DestroyImmediate(Temp);

            Vector3 Offset = TempR * SteelRail_Gap;
            if (Inner)
                Offset = -Offset;

            List<GameObject> objects = new List<GameObject>();

            for (int n = 0; n < 4; n++)
            {
                MeshBuilder2 meshBuilder = new MeshBuilder2();

                Vector3[] vertices = new Vector3[4];

                for (int i = 0; i < 2; i++)
                {
                    Vector3 currPoint = BezierCurve2.TrackData_List[id].position;
                    Vector3 currTangent = BezierCurve2.TrackData_List[id].tangent;

                    if (i == 1)
                    {
                        if (id + 1 != TotalWayPoints)
                        {
                            currPoint = BezierCurve2.TrackData_List[id + 1].position;
                            currTangent = BezierCurve2.TrackData_List[id + 1].tangent;
                        }
                        else
                        {
                            currPoint = BezierCurve2.TrackData_List[0].position;
                            currTangent = BezierCurve2.TrackData_List[0].tangent;
                        }
                    }

                    Vector3 cross = new Vector3();

                    if (n == 0)
                    {
                        currPoint = currPoint + Vector3.up * SteelRail_Height + Offset;
                        cross = Vector3.Cross(Vector3.up, currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * SteelRail_Width;
                        vertices[i * 2 + 1] = currPoint - cross * SteelRail_Width;
                    }
                    if (n == 1)
                    {
                        currPoint = currPoint - Vector3.up * SteelRail_Height + Offset;
                        cross = Vector3.Cross(-Vector3.up, currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * SteelRail_Width;
                        vertices[i * 2 + 1] = currPoint - cross * SteelRail_Width;
                    }

                    if (n == 2)
                    {
                        currPoint = currPoint - TempR * SteelRail_Width + Offset;
                        cross = Vector3.Cross(-TempR, currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * SteelRail_Height;
                        vertices[i * 2 + 1] = currPoint - cross * SteelRail_Height;
                    }
                    if (n == 3)
                    {
                        currPoint = currPoint + TempR * SteelRail_Width + Offset;
                        cross = Vector3.Cross(TempR, currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * SteelRail_Height;
                        vertices[i * 2 + 1] = currPoint - cross * SteelRail_Height;
                    }

                    Vector2 uv2 = new Vector2(0, 1);
                    Vector2 uv1 = new Vector2(1, 1);

                    meshBuilder.UVs.Add(uv1);
                    meshBuilder.UVs.Add(uv2);
                }

                meshBuilder.Vertices.AddRange(vertices);

                // Build triangle list

                meshBuilder.AddTriangle(0, 1, 2);

                meshBuilder.AddTriangle(1, 3, 2);

                Mesh mesh = meshBuilder.CreateMesh();

                mesh.RecalculateNormals();

                GameObject piece = new GameObject();

                piece.AddComponent<MeshFilter>().sharedMesh = mesh;

                objects.Add(piece);
            }

            Mesh combinedMesh = CombineMeshes.Combine(objects);

            foreach (GameObject obj in objects)
            {
                DestroyImmediate(obj);
            }

            GameObject Final = new GameObject(id.ToString());

            Final.transform.SetParent(parent.transform);
            Final.transform.position = parent.transform.position;

            Final.AddComponent<MeshFilter>().sharedMesh = combinedMesh;

            MeshRenderer rend = Final.AddComponent<MeshRenderer>();

            //#if UNITY_EDITOR
            //            combinedMesh.name = Final.name + "_" + Level.ToString();

            //            AssetDatabase.CreateAsset(combinedMesh, FileUtil.GetProjectRelativePath(Application.streamingAssetsPath + "/" + Level.ToString() + "/" + combinedMesh.name + ".asset"));
            //            AssetDatabase.SaveAssets();
            //#endif

            rend.enabled = false;

            rend.sharedMaterial = SteelRail_Material;

            objectsList.Add(Final);
        }

        return objectsList;
    }

}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
[ExecuteInEditMode()]
#endif
[RequireComponent(typeof(MeshFilter))]
public class TrackMeshGenerate : MonoBehaviour
{
    public float Gap;
    public float Width;
    public float Height;

    public float Sub_Gap;
    public float Sub_Height;
    public float Sub_Width;
    public float Sub_Length;

    private int totalPoints;
    private List<int> RailList;

    public Material Track_Metal;
    public Material Track_Wood;

    public int Level;

    public GameObject FadingParent;

    void Update()
    {
#if UNITY_EDITOR || UNITY_WSA

        if (BezierCurve2.updateTrack && BezierCurve2.points.Length > 2 && AppManager.curScene == 6)
        {
            BezierCurve2.CalcAllTrackPointData();

            GenerateMesh();

            BezierCurve2.unRenderMesh();

            BezierCurve2.updateTrack = false;
            BezierCurve2.Go = true;
        }
#endif
    }

    private void Start()
    {
        Sub_Height = Sub_Height * 0.5f;
    }

    public void GenerateMesh()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        totalPoints = BezierCurve2.TrackData_List.Length;

        DrawTrack_Part1("inner", true);
        DrawTrack_Part1("outter", false);

        DrawTrack_Part2(true, gameObject);

    }

    private void DrawTrack_Part1(string Name, bool Inner)
    {
        Vector3[] allPoints = new Vector3[totalPoints];

        Vector3[] vertices = new Vector3[totalPoints * 2];

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

                Vector3 Offset = TempR * Gap;
                if (Inner)
                    Offset = -Offset;

                Vector3 cross = new Vector3();
                if (n == 0)
                {
                    allPoints[i] = BezierCurve2.TrackData_List[i].position + Vector3.up * Height + Offset;
                    cross = Vector3.Cross(Vector3.up, BezierCurve2.TrackData_List[i].tangent);

                    cross = cross.normalized;
                    vertices[i * 2] = allPoints[i] + cross * Width;
                    vertices[i * 2 + 1] = allPoints[i] - cross * Width;
                }
                if (n == 1)
                {
                    allPoints[i] = BezierCurve2.TrackData_List[i].position - Vector3.up * Height + Offset;
                    cross = Vector3.Cross(-Vector3.up, BezierCurve2.TrackData_List[i].tangent);

                    cross = cross.normalized;
                    vertices[i * 2] = allPoints[i] + cross * Width;
                    vertices[i * 2 + 1] = allPoints[i] - cross * Width;
                }

                if (n == 2)
                {
                    allPoints[i] = BezierCurve2.TrackData_List[i].position - TempR * Width + Offset;
                    cross = Vector3.Cross(-TempR, BezierCurve2.TrackData_List[i].tangent);

                    cross = cross.normalized;
                    vertices[i * 2] = allPoints[i] + cross * Height;
                    vertices[i * 2 + 1] = allPoints[i] - cross * Height;
                }
                if (n == 3)
                {
                    allPoints[i] = BezierCurve2.TrackData_List[i].position + TempR * Width + Offset;
                    cross = Vector3.Cross(TempR, BezierCurve2.TrackData_List[i].tangent);

                    cross = cross.normalized;
                    vertices[i * 2] = allPoints[i] + cross * Height;
                    vertices[i * 2 + 1] = allPoints[i] - cross * Height;
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

        rend.sharedMaterial = Track_Metal;
    }

    private List<GameObject> DrawTrack_Part2(bool One_P, GameObject parent)
    {
        GameObject Final = new GameObject("Rail");
        if(RailList == null)
            RailList = new List<int>();
        else
            RailList.Clear();

        Final.transform.SetParent(parent.transform);
        Final.transform.position = parent.transform.position;

        float MaxTrackLength = BezierCurve2.TrackData_List[totalPoints - 1].distance;
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

                if (currID >= totalPoints - 1)
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

            if (currID >= totalPoints - 1)
            {
                run = false;
            }
            currDistance += Sub_Gap;
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

            rend.sharedMaterial = Track_Wood;

            return null;
        }
        else
        {
            return objects;
        }
    }

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

                if(i == 1)
                {
                    currPoint = BezierCurve2.TrackData_List[id].position + BezierCurve2.TrackData_List[id].tangent * Sub_Width;
                }


                Vector3 cross = new Vector3();

                if (n == 0)
                {
                    currPoint = currPoint + Vector3.up * Sub_Height;
                    cross = Vector3.Cross(Vector3.up, currTangent);

                    cross = cross.normalized;
                    vertices[i * 2] = currPoint + cross * Sub_Length;
                    vertices[i * 2 + 1] = currPoint - cross * Sub_Length;
                }
                if (n == 1)
                {
                    currPoint = currPoint - Vector3.up * Sub_Height;
                    cross = Vector3.Cross(-Vector3.up, currTangent);

                    cross = cross.normalized;
                    vertices[i * 2] = currPoint + cross * Sub_Length;
                    vertices[i * 2 + 1] = currPoint - cross * Sub_Length;
                }
                if (n == 2)
                {
                    currPoint = currPoint - TempR * Sub_Length;
                    cross = Vector3.Cross(-TempR, currTangent);

                    cross = cross.normalized;
                    vertices[i * 2] = currPoint + cross * Sub_Height;
                    vertices[i * 2 + 1] = currPoint - cross * Sub_Height;
                }
                if (n == 3)
                {
                    currPoint = currPoint + TempR * Sub_Length;
                    cross = Vector3.Cross(TempR, currTangent);

                    cross = cross.normalized;
                    vertices[i * 2] = currPoint + cross * Sub_Height;
                    vertices[i * 2 + 1] = currPoint - cross * Sub_Height;
                }
                if (n == 4)
                {
                    currPoint = BezierCurve2.TrackData_List[id].position;

                    if (i == 0)
                    {
                        currPoint = currPoint + Vector3.up * Sub_Height;
                        cross = Vector3.Cross(Vector3.up, -currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * Sub_Length;
                        vertices[i * 2 + 1] = currPoint - cross * Sub_Length;
                    }

                    if (i == 1)
                    {
                        currPoint = currPoint - Vector3.up * Sub_Height;
                        cross = Vector3.Cross(Vector3.up, -currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * Sub_Length;
                        vertices[i * 2 + 1] = currPoint - cross * Sub_Length;
                    }
                }
                if (n == 5)
                {
                    currPoint = BezierCurve2.TrackData_List[id].position + BezierCurve2.TrackData_List[id].tangent * Sub_Width;

                    if (i == 0)
                    {
                        currPoint = currPoint + Vector3.up * Sub_Height;
                        cross = Vector3.Cross(Vector3.up, currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * Sub_Length;
                        vertices[i * 2 + 1] = currPoint - cross * Sub_Length;
                    }

                    if (i == 1)
                    {
                        currPoint = currPoint - Vector3.up * Sub_Height;
                        cross = Vector3.Cross(Vector3.up, currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * Sub_Length;
                        vertices[i * 2 + 1] = currPoint - cross * Sub_Length;
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
        RailList.Add(id);

        Final.transform.SetParent(currParent.transform);
        Final.transform.position = currParent.transform.position;

        Final.AddComponent<MeshFilter>().sharedMesh = combinedMesh;

        MeshRenderer rend = Final.AddComponent<MeshRenderer>();

        if (!One_P)
            rend.enabled = false;

        rend.sharedMaterial = Track_Wood;

        ListObjects.Add(Final);

    }

    
    public void GenerateMesh2()
    {
        for (int i = FadingParent.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(FadingParent.transform.GetChild(i).gameObject);
        }

        totalPoints = BezierCurve2.TrackData_List.Length;

        FadeInTracks FadeSc = FadingParent.GetComponent(typeof(FadeInTracks))as FadeInTracks;

        FadeSc.Inner = DrawTrack_Part4("inner", true);
        FadeSc.Outter = DrawTrack_Part4("outter", false);

        FadeSc.Rail = DrawTrack_Part2(false, FadingParent);
        FadeSc.RailList = RailList;
    }

    private List<GameObject> DrawTrack_Part4(string Name, bool Inner)
    {
        GameObject parent = new GameObject(Name);
        parent.transform.SetParent(FadingParent.transform);
        parent.transform.position = FadingParent.transform.position;

        List<GameObject> objectsList = new List<GameObject>();

        for (int id = 0; id < totalPoints; id++)
        {
            GameObject Temp = new GameObject();
            Temp.transform.position = BezierCurve2.TrackData_List[id].position;
            Temp.transform.LookAt(Temp.transform.position + BezierCurve2.TrackData_List[id].tangent);
            Vector3 TempR = Temp.transform.right;
            DestroyImmediate(Temp);

            Vector3 Offset = TempR * Gap;
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
                        if (id + 1 != totalPoints)
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
                        currPoint = currPoint + Vector3.up * Height + Offset;
                        cross = Vector3.Cross(Vector3.up, currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * Width;
                        vertices[i * 2 + 1] = currPoint - cross * Width;
                    }
                    if (n == 1)
                    {
                        currPoint = currPoint - Vector3.up * Height + Offset;
                        cross = Vector3.Cross(-Vector3.up, currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * Width;
                        vertices[i * 2 + 1] = currPoint - cross * Width;
                    }

                    if (n == 2)
                    {
                        currPoint = currPoint - TempR * Width + Offset;
                        cross = Vector3.Cross(-TempR, currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * Height;
                        vertices[i * 2 + 1] = currPoint - cross * Height;
                    }
                    if (n == 3)
                    {
                        currPoint = currPoint + TempR * Width + Offset;
                        cross = Vector3.Cross(TempR, currTangent);

                        cross = cross.normalized;
                        vertices[i * 2] = currPoint + cross * Height;
                        vertices[i * 2 + 1] = currPoint - cross * Height;
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

            rend.enabled = false;

            rend.sharedMaterial = Track_Metal;

            objectsList.Add(Final);
        }

        return objectsList;
    }

}


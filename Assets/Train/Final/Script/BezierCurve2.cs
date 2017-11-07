using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class BezierCurve2 {

    public static Vector3[] points = new Vector3[] { };

    public static GameObject[] GO_Points = new GameObject[] { };

    public static int CruveSteps = 10; // number of points in one curve

    public static float Distance_scaleFacter = 10f; // becasue position also need scale down when size scale down

    public static bool updateTrack = false;

    public static bool Go = false;

    public static bool EnableTrackCollision = false;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Store Point's num of CruveSteps  //
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [System.Serializable]
    public struct PointData
    {
        public int id;
        public float distance; //distance from this point to next point
        public int pointCruveSteps;
    }

    public static PointData[] PointData_List;

    public static int getTotalCruveStepsTo(int ID)
    {
        int Result = 0;

        for (int i = 0; i < ID; i++)
        {
            for (int n = 0; n < PointData_List.Length; n++)
            {
                if (PointData_List[n].id == i)
                {
                    Result += PointData_List[n].pointCruveSteps;
                    break;
                }
            }
        }

        return Result;
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Calc SPLINE&TRACK Length //
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [System.Serializable]
    public struct TrackData
    {
        public Vector3 position;
        public Vector3 tangent;
        public float distance;
        public int id;
    }

    public static TrackData[] TrackData_List;

    public static void CalcAllTrackPointData()
    {
        CalcAllTrackLength(true);
        CalcAllTrackLength(false);
        //Debug.Log("HI");
    }

    public static void CalcAllTrackLength(bool T)
    {
        if (points.Length > 2)
        {
            int currentPoint = 0;
            int totalPoint = points.Length;

            float totalDistance = 0f;

            

            if (T)
            {
                Array.Resize(ref TrackData_List, (points.Length * CruveSteps) + 1);
                Array.Resize(ref PointData_List, (points.Length));
            }
            else
            {
                Array.Resize(ref TrackData_List, 0);
                
                int numOfSubPoint = 0;
                for(int i = 0;i< PointData_List.Length;i++)
                {
                    numOfSubPoint += PointData_List[i].pointCruveSteps;
                }

                Array.Resize(ref TrackData_List, numOfSubPoint + 1);
                //Debug.Log(numOfSubPoint + 1);
            }

            for (int n = 0; n < totalPoint; n++)
            {
                MainPoints tempPoint = GO_Points[0].GetComponent(typeof(MainPoints)) as MainPoints;

                for (int x = 0; x < GO_Points.Length; x++)
                {
                    MainPoints temp = GO_Points[x].GetComponent(typeof(MainPoints)) as MainPoints;
                    if (temp.ID == currentPoint)
                    {
                        tempPoint = temp;
                        break;
                    }
                }

                if (tempPoint.ID == 0)
                {
                    TrackData_List[0].id = 0;
                    TrackData_List[0].distance = 0f;
                    TrackData_List[0].position = tempPoint.transform.position;
                    TrackData_List[0].tangent = GetFirstDerivative(tempPoint.transform.position, tempPoint.ChildPoint_position, tempPoint.Friend_ChildPoint_position, tempPoint.FriendPoint_position, 0).normalized;
                }

                 
                float TempTotalDistance = CalcCurveLength(tempPoint, totalDistance,T);

                if (T)
                {
                    PointData_List[n].id = n;
                    PointData_List[n].distance = TempTotalDistance - totalDistance;
                    PointData_List[n].pointCruveSteps = (int)(PointData_List[n].distance * 100); // every 1 unit space, 100 sub point
                    //Debug.Log(n + " : "+ PointData_List[n].pointCruveSteps);
                }

                totalDistance = TempTotalDistance;

                currentPoint++;
            }
            Go = true;
        }
    }

    private static float CalcCurveLength(MainPoints currPoint, float currTotalDistance, bool T)
    {
        Vector3 prevPoint = currPoint.transform.position;
        Vector3 pt = new Vector3();
        Vector3 tangent = new Vector3();

        int currID = 1;
        float step = 0.0f;
        int MaxCurrID = 0;

        if (T)
        {
            currID += (currPoint.ID * CruveSteps);

            MaxCurrID = currID + CruveSteps;

            step = 1f / (float)CruveSteps;
        }
        else
        {
            for (int i = 0; i < currPoint.ID; i++)
            {
                currID += PointData_List[i].pointCruveSteps;
            }

            step = 1f / (float)PointData_List[currPoint.ID].pointCruveSteps;

            MaxCurrID = currID + PointData_List[currPoint.ID].pointCruveSteps;
        }

        for (float f = step; currID < MaxCurrID; f += step) // this is so dumb
        {
            
            if (currPoint.type == (int)MainPoints.pointType.NormalPoint || currPoint.type == (int)MainPoints.pointType.FixedPoint || currPoint.type == (int)MainPoints.pointType.TrafficLight)
            {
                pt = GetPoint(currPoint.transform.position, currPoint.ChildPoint_position, currPoint.Friend_ChildPoint_position, currPoint.FriendPoint_position, f);
                currTotalDistance += (pt - prevPoint).magnitude;

                tangent = GetFirstDerivative(currPoint.transform.position, currPoint.ChildPoint_position, currPoint.Friend_ChildPoint_position, currPoint.FriendPoint_position, f);
            }

            if(currPoint.type == (int)MainPoints.pointType.EventPoint)
            {
                pt = GetPoint(currPoint.transform.position, currPoint.transform.position, currPoint.FriendPoint_position, currPoint.FriendPoint_position, f);
                currTotalDistance += (pt - prevPoint).magnitude;

                tangent = (currPoint.FriendPoint_position - currPoint.transform.position).normalized;
            }

            TrackData_List[currID].id = currPoint.ID;
            TrackData_List[currID].distance = currTotalDistance;
            TrackData_List[currID].position = pt;
            TrackData_List[currID].tangent = tangent.normalized;

            prevPoint = pt;
            currID++;
        }

        return currTotalDistance;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // TRACK MESH //
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public static Vector3 Interpolate(int i, float t, out Vector3 tangent)
    {

        tangent = GetDirection(i, t);

        return GetPoint_Track(i, t);
    }

    public static Vector3 GetDirection(int i, float t)
    {
        return GetVelocity(i, t).normalized;
    }

    public static Vector3 GetVelocity(int i, float t)
    {
        int x = i;
        int y = i + 1;

        if (i + 1 == points.Length)
        {
            y = 0;
        }

        Vector3 p0 = Vector3.zero;
        Vector3 p1 = Vector3.zero;
        Vector3 p2 = Vector3.zero;
        Vector3 p3 = Vector3.zero;

        for (int n = 0; n < GO_Points.Length; n++)
        {
            MainPoints FristPoint = GO_Points[n].GetComponent(typeof(MainPoints)) as MainPoints;

            if (FristPoint.ID == x)
            {
                p0 = FristPoint.transform.position;
                p1 = FristPoint.ChildPoint_position;
            }

            if (FristPoint.ID == y)
            {
                p2 = FristPoint.transform.position;
                p3 = FristPoint.transform.position + (FristPoint.transform.position - FristPoint.ChildPoint_position);
            }
        }

        return GetFirstDerivative(p0, p1, p3, p2, t);
    }

    public static Vector3 GetPoint_Track(int i, float t)
    {
         int x = i;
        int y = i + 1;

        if (i + 1 == points.Length)
        {
            y = 0;
        }

        Vector3 p0 = Vector3.zero;
        Vector3 p1 = Vector3.zero;
        Vector3 p2 = Vector3.zero;
        Vector3 p3 = Vector3.zero;

        for (int n = 0; n < GO_Points.Length; n++)
        {
            MainPoints FristPoint = GO_Points[n].GetComponent(typeof(MainPoints)) as MainPoints;

            if (FristPoint.ID == x)
            {
                p0 = FristPoint.transform.position;
                p1 = FristPoint.ChildPoint_position;
            }

            if (FristPoint.ID == y)
            {
                p2 = FristPoint.transform.position;
                p3 = FristPoint.transform.position + (FristPoint.transform.position - FristPoint.ChildPoint_position);
            }
        }

        return GetPoint(p0, p1, p3, p2, t);

    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // UPDATE CURVE POINTS //
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public static void updateCurvePoints()
    {
        if (points.Length > 2)
        {
            updatePoints_Position();

            updateChild_Position();

            updateFriendPoints();

            TriggerUpdate();

        }
    }

    public static void unRenderMesh()
    {
        for (int i = 0; i < GO_Points.Length; i++)
        {
            MainPoints newPoint = GO_Points[i].GetComponent(typeof(MainPoints)) as MainPoints;

            newPoint.UnrenderMesh = true;
        }
    }
    private static void updatePoints_Position()
    {
        GO_Points = GameObject.FindGameObjectsWithTag("Point");

        for (int x = 0; x < points.Length; x++)
        {
            for (int i = 0; i < GO_Points.Length; i++)
            {
                MainPoints newPoint = GO_Points[i].GetComponent(typeof(MainPoints)) as MainPoints;
                if (newPoint.ID == x)
                {
                    points[x] = GO_Points[i].transform.position * Distance_scaleFacter;
                }
            }
        }
    }

    private static void updateChild_Position()
    {
        if (points.Length >= 3)
        {
            for (int x = 0; x < points.Length; x++)
            {
                int x_y = x + 1;
                int y = x + 2;


                if (x == points.Length - 1)
                {
                    x_y = 0;
                    y = 1;
                }
                else if (x == points.Length - 2)
                {
                    x_y = points.Length - 1;
                    y = 0;
                }
                MainPoints SecondPoint = GO_Points[0].GetComponent(typeof(MainPoints)) as MainPoints;
                GameObject FristPointObject = GO_Points[0];
                GameObject ThirdPointObject = GO_Points[0];

                for (int i = 0; i < GO_Points.Length; i++)
                {
                    MainPoints Temp = GO_Points[i].GetComponent(typeof(MainPoints)) as MainPoints;
                    if (Temp.ID == x)
                    {
                        FristPointObject = GO_Points[i];
                    }
                    if (Temp.ID == y)
                    {
                        ThirdPointObject = GO_Points[i];
                    }
                }

                for (int i = 0; i < GO_Points.Length; i++)
                {
                    SecondPoint = GO_Points[i].GetComponent(typeof(MainPoints)) as MainPoints;
                    if (SecondPoint.ID == x_y)
                    {
                        Vector3 normal = (ThirdPointObject.transform.position - FristPointObject.transform.position).normalized;


                        SecondPoint.Normalized_For_Child = normal;

                        // make a scale for child distance
                        float distance_AC = (ThirdPointObject.transform.position - FristPointObject.transform.position).magnitude * 0.5f;

                        float distance_AB = (SecondPoint.transform.position - FristPointObject.transform.position).magnitude;
                        float distance_BC = (ThirdPointObject.transform.position - SecondPoint.transform.position).magnitude;

                        if (distance_AC > distance_AB || distance_AC > distance_BC)
                        {



                            if (distance_AB > distance_BC)
                            {
                                distance_BC *= 0.3f;
                                SecondPoint.ChildPoint_position = SecondPoint.transform.position + (SecondPoint.Normalized_For_Child * distance_BC);
                                break;
                            }
                            else
                            {
                                distance_AB *= 0.3f;
                                SecondPoint.ChildPoint_position = SecondPoint.transform.position + (SecondPoint.Normalized_For_Child * distance_AB);
                                break;
                            }
                        }
                        else if (distance_AB > distance_AC && distance_AC < distance_BC)
                        {
                            while (distance_AC * 2 < distance_AB || distance_AC * 2 < distance_BC)
                            {
                                distance_AC *= 2.0f;
                            }

                            distance_AC *= 0.3f;
                            SecondPoint.ChildPoint_position = SecondPoint.transform.position + (SecondPoint.Normalized_For_Child * distance_AC);
                            break;
                        }

                        break;
                    }
                }
            }
        }
    }

    private static void updateFriendPoints()
    {
        for (int x = 0; x < points.Length; x++)
        {
            int y = x + 1;

            if (x == points.Length - 1)
            {
                y = 0;
            }

            MainPoints FristPoint = GO_Points[0].GetComponent(typeof(MainPoints)) as MainPoints;

            for (int i = 0; i < GO_Points.Length; i++)
            {
                FristPoint = GO_Points[i].GetComponent(typeof(MainPoints)) as MainPoints;
                if (FristPoint.ID == x)
                {
                    break;
                }
            }

            for (int i = 0; i < GO_Points.Length; i++)
            {
                MainPoints SecondPoint = GO_Points[i].GetComponent(typeof(MainPoints)) as MainPoints;

                if (SecondPoint.ID == y)
                {
                    FristPoint.FriendPoint_position = SecondPoint.transform.position;

                    Vector3 Temp = SecondPoint.transform.position + (SecondPoint.transform.position - SecondPoint.ChildPoint_position);
                    FristPoint.Friend_ChildPoint_position = Temp;
                    break;
                }
            }
        }
    }

    private static void TriggerUpdate()
    {
        for (int i = 0; i < GO_Points.Length; i++)
        {
            MainPoints newPoint = GO_Points[i].GetComponent(typeof(MainPoints)) as MainPoints;

            newPoint.UpdateMesh = true;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // BEZIER //
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * p0 +
            2f * oneMinusT * t * p1 +
            t * t * p2;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return
            2f * (1f - t) * (p1 - p0) +
            2f * t * (p2 - p1);
    }

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * p0 +
            3f * oneMinusT * oneMinusT * t * p1 +
            3f * oneMinusT * t * t * p2 +
            t * t * t * p3;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p1 - p0) +
            6f * oneMinusT * t * (p2 - p1) +
            3f * t * t * (p3 - p2);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // DEFAULT //
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public static void ClearAllData()
    {

        for (int n = 0; n < points.Length; n++)
        {
            points[0] = new Vector3();
        }
        Array.Resize(ref points, 0);


        for (int i = 0; i < GO_Points.Length; i++)
        {
            GO_Points[i] = null;
        }

        Array.Resize(ref GO_Points, 0);

        Array.Resize(ref TrackData_List, 0);
        Array.Resize(ref PointData_List, 0);
    }


    public static void IncreaseSize(Vector3[] newPoints, int i)
    {
        Array.Resize(ref points, points.Length + i);
        for (int n = 0; n < i; n++)
        {
            points[points.Length - (n + 1)] = newPoints[n];
        }
    }

    public static void IncreaseSize(int i)
    {
        Array.Resize(ref points, points.Length + i);
    }

    public static void addPoint_shiftID(int ID)
    {
        int lastID = points.Length-1;

        for (int x = 0; x < points.Length; x++)
        {
            if(ID > lastID - x)
            {
                break;
            }

            for (int i = 0; i < GO_Points.Length; i++)
            {
                MainPoints newPoint = GO_Points[i].GetComponent(typeof(MainPoints)) as MainPoints;
                if (newPoint.ID == lastID - x)
                {
                    newPoint.ID = lastID - x + 1;
                }
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // SAVE AND LOAD //
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [System.Serializable]
    public struct TrackPointData
    {
        public PointData[] pointData_List;
        public TrackData[] trackData_List;
    }

    public static void SaveTrackPointData(int Level)
    {
        TrackPointData DataList = new TrackPointData();
        DataList.pointData_List = PointData_List;
        DataList.trackData_List = TrackData_List;

        //Debug.Log(DataList.pointData_List[1].id);

        String saveDataString = JsonUtility.ToJson(DataList);
        String path = "";

        switch(Level)
        {
            case 0:
                {
                    path = Save_Load_Data.Tutorial;
                    break;
                }
            case 1:
                {
                    path = Save_Load_Data.Level_one_TrackData;
                    break;
                }
            case 2:
                {
                    path = Save_Load_Data.Level_two_TrackData;
                    break;
                }
            case 3:
                {
                    path = Save_Load_Data.Level_three_TrackData;
                    break;
                }
            case 4:
                {
                    path = Save_Load_Data.Level_four_TrackData;
                    break;
                }
        }

        Save_Load_Data.Save(path,saveDataString);
    }

    public static void LoadTrackPointData(int Level)
    {
        TrackPointData DataList = new TrackPointData();

        String path = "";

        switch (Level)
        {
            case 0:
                {
                    path = Save_Load_Data.Tutorial;
                    break;
                }
            case 1:
                {
                    path = Save_Load_Data.Level_one_TrackData;
                    break;
                }
            case 2:
                {
                    path = Save_Load_Data.Level_two_TrackData;
                    break;
                }
            case 3:
                {
                    path = Save_Load_Data.Level_three_TrackData;
                    break;
                }
            case 4:
                {
                    path = Save_Load_Data.Level_four_TrackData;
                    break;
                }
        }

        if (Save_Load_Data.Check_SaveFile(path))
        {
            DataList = JsonUtility.FromJson<TrackPointData>(Save_Load_Data.load(path));

            Array.Resize(ref TrackData_List, 0);
            Array.Resize(ref PointData_List, 0);
            TrackData_List = DataList.trackData_List;
            PointData_List = DataList.pointData_List;
        }
    }
}

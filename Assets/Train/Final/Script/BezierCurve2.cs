/** 
*  @file    BezierCurve2.cs
*  @author  Yin Shuyu (150713R) 
*  
*  @brief   Contain static class BezierCurve2
*
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
*  @brief A Static class for calculation for track points using bezier curve formula
*/
public static class BezierCurve2 {

    public static Vector3[] points = new Vector3[] { };  ///< Array of Vector3 storing the position of the track waypoint

    public static GameObject[] GO_Points = new GameObject[] { }; ///< Array of GameObject storing the gameobject of the track waypoints

    public static int CruveSteps = 10;  ///< Number of how many sub-point between two wayPoints

    public static float Distance_scaleFacter = 10f; ///< Scale down the position distance of the waypoint as we Scale down the actual scene (hardcoded)

    public static bool updateTrack = false; ///< Bool trigger for func for other scripts

    public static bool Go = false; ///< Bool trigger for starting the Train 

    public static bool EnableTrackCollision = false; ///< Bool trigger mesh collision for Rail Track

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Store Point's num of CruveSteps  //
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /**
    *  @brief Struct save all the track's WayPoint Data
    */
    [System.Serializable]
    public struct PointData
    {
        public int id; ///< ID of the wayPoint
        public float distance; ///< Distance from the current waypoint to the next waypoint
        public int pointCruveSteps; ///< Number of sub-point between the current waypoint and the next waypoint
    }

    public static PointData[] PointData_List; ///< Array of PointData struct Stores all the Track's WayPoint

    /**
   *   @brief calculate the total number of sub-points from start to a specific WayPoint
   *  
   *   @param int ID, id of the WayPoint we want to sreach to
   *   
   *   @return int Result, the total number of sub-points
   */
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

    /**
    *  @brief Struct save all the track's Sub-Point Data
    */
    [System.Serializable]
    public struct TrackData
    {
        public Vector3 position; ///< Vector3 position of the sub-point
        public Vector3 tangent; ///< Vector3 tangent of the sub-point
        public float distance; ///< Distance of the sub-point from start
        public int id; ///< id of the sub-point
    }

    public static TrackData[] TrackData_List; ///< Array of TrackData struct Stores all the track's Sub-Point

    /**
    *   @brief A function to run CalcAllTrackLength func two time in different way
    *   
    *   @return nothing 
    */
    public static void CalcAllTrackPointData()
    {
        CalcAllTrackLength(true);
        CalcAllTrackLength(false);
    }

    /**
    *   @brief A function to calculation All Track's Data, also reassign the pointCruveSteps for each WayPoint to optimistic the polygon count for track Mesh
    *  
    *   @param bool T, whether run for the first time
    *   
    *   @return nothing 
    */
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

    /**
    *   @brief A function to calculation the distance for the track
    *  
    *   @param MainPoints currPoint, Get current point Data
    *   
    *   @param float currTotalDistance, total distance of track till this waypoint
    *   
    *   @param bool T, pass down from CalcAllTrackLength function
    *   
    *   @return float currTotalDistance, total distance of track till this waypoint
    */
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
            
            if (currPoint.type == MainPoints.pointType.NormalPoint || currPoint.type == MainPoints.pointType.FixedPoint || currPoint.type == MainPoints.pointType.TrafficLight)
            {
                pt = GetPoint(currPoint.transform.position, currPoint.ChildPoint_position, currPoint.Friend_ChildPoint_position, currPoint.FriendPoint_position, f);
                currTotalDistance += (pt - prevPoint).magnitude;

                tangent = GetFirstDerivative(currPoint.transform.position, currPoint.ChildPoint_position, currPoint.Friend_ChildPoint_position, currPoint.FriendPoint_position, f);
            }

            if(currPoint.type == MainPoints.pointType.EventPoint)
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

    /**
    *   @brief A function to get the interpolate point of the waypoint
    *  
    *   @param int i, the id of the waypoint
    *   
    *   @param float t, interpolate percentage of the waypoint
    *   
    *   @param out Vector3 tangent, to get the tangent of the interpolate point 
    *   
    *   @return Vector3, position of the interpolate point 
    */
    public static Vector3 Interpolate(int i, float t, out Vector3 tangent)
    {

        tangent = GetDirection(i, t);

        return GetPoint_Track(i, t);
    }

    /**
    *   @brief A function to get the direction it is facing at the interpolate point of the waypoint
    *  
    *   @param int i, the id of the waypoint
    *   
    *   @param float t, interpolate percentage of the waypoint
    *   
    *   @return Vector3, direction of the interpolate point 
    */
    public static Vector3 GetDirection(int i, float t)
    {
        return GetVelocity(i, t).normalized;
    }

    /**
    *   @brief A function to get the Velocity at the interpolate point of the waypoint
    *  
    *   @param int i, the id of the waypoint
    *   
    *   @param float t, interpolate percentage of the waypoint
    *   
    *   @return Vector3, Velocity of the interpolate point 
    */
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

    /**
    *   @brief A function to get the Position of the interpolate point of the waypoint
    *  
    *   @param int i, the id of the waypoint
    *   
    *   @param float t, interpolate percentage of the waypoint
    *   
    *   @return Vector3, Position of the interpolate point 
    */
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

    /**
    *   @brief A function to Update the Curve of the of the Track 
    *   
    *   @return nothing 
    */
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

    /**
    *   @brief A function to unRender the 2D mesh of Track
    *  
    *   @return nothing 
    */
    public static void unRenderMesh()
    {
        for (int i = 0; i < GO_Points.Length; i++)
        {
            MainPoints newPoint = GO_Points[i].GetComponent(typeof(MainPoints)) as MainPoints;

            newPoint.UnrenderMesh = true;
        }
    }

    /**
    *   @brief A function to Update the position data of the waypoint with Distance_scaleFacter
    *  
    *   @return nothing 
    */
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

    /**
    *   @brief A function to Update the position data of the waypoint's Child-point using waypoints data with another waypoint data
    *   
    *   @return nothing 
    */
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

    /**
    *   @brief A function to Update the position data of the waypoint's friend(the next point)
    *   
    *   @return nothing 
    */
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

    /**
    *   @brief A function to trigger the MainPoint script to update
    *   
    *   @return nothing 
    */
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

    /**
    *   @brief A function to get the position of the interpolate point out of the Three Vector3 using bezier curve formula
    *  
    *   @param Vector3 p0, start point of curve 
    *   
    *   @param Vector3 p1, the point determine curve of the curve
    *   
    *   @param Vector3 p2, end point of curve 
    *   
    *   @param float t,interpolate percentage of the curve
    *   
    *   @return Vector3, position of the interpolate point
    */
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * p0 +
            2f * oneMinusT * t * p1 +
            t * t * p2;
    }

    /**
   *   @brief A function to get the Velocity of the interpolate point out of the Three Vector3 using bezier curve formula
   *  
   *   @param Vector3 p0, start point of curve 
   *   
   *   @param Vector3 p1, the point determine curve of the curve
   *   
   *   @param Vector3 p2, end point of curve 
   *   
   *   @param float t,interpolate percentage of the curve
   *   
   *   @return Vector3, Velocity of the interpolate point
   */
    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return
            2f * (1f - t) * (p1 - p0) +
            2f * t * (p2 - p1);
    }

    /**
     *   @brief A function to get the position of the interpolate point out of the four Vector3 using bezier curve formula
     *  
     *   @param Vector3 p0, start point of curve 
     *   
     *   @param Vector3 p1, the point determine curve of p0
     *   
     *   @param Vector3 p2, end point of curve 
     *   
     *   @param Vector3 p3, the point determine curve of p2
     * 
     *   @param float t,interpolate percentage of the curve
     *   
     *   @return Vector3, position of the interpolate point
     */
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

    /**
     *   @brief A function to get the Velocity of the interpolate point out of the four Vector3 using bezier curve formula
     *  
     *   @param Vector3 p0, start point of curve 
     *   
     *   @param Vector3 p1, the point determine curve of p0
     *   
     *   @param Vector3 p2, end point of curve 
     *   
     *   @param Vector3 p3, the point determine curve of p2
     * 
     *   @param float t,interpolate percentage of the curve
     *   
     *   @return Vector3, Velocity of the interpolate point
     */
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

    /**
    *   @brief A function to Clear all the Array Data to size 0
    *   
    *   @return nothing 
    */
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

    /**
    *   @brief A function to Increase the size of the WayPoint Array (old Codes)
    *  
    *   @param Array of Vector3 newPoints, Array of Previous WayPoints
    *   
    *   @param int i, size increase
    *   
    *   @return nothing 
    */
    public static void IncreaseSize(Vector3[] newPoints, int i)
    {
        Array.Resize(ref points, points.Length + i);
        for (int n = 0; n < i; n++)
        {
            points[points.Length - (n + 1)] = newPoints[n];
        }
    }

    /**
     *   @brief A function to Increase the size of the WayPoint data and object Arrays
     *   
     *   @param int i, size increase
     *   
     *   @return nothing 
     */
    public static void IncreaseSize(int i)
    {
        Array.Resize(ref points, points.Length + i);
        Array.Resize(ref GO_Points, GO_Points.Length + i);
    }

    /**
     *   @brief A function to Add new WayPoint in bewteen waypoints and update all the waypoint's id accordingly 
     *   
     *   @param int ID, the new WayPoint's ID
     *   
     *   @return nothing 
     */
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

    /**
    *  @brief Struct Contain both Track Data and Point Data
    */
    [System.Serializable]
    public struct TrackPointData
    {
        public PointData[] pointData_List;
        public TrackData[] trackData_List;
    }

    /**
    *   @brief A function to Call the Save_Load_Data.cs to save TrackPointData of the level
    *  
    *   @param int level, level of the TrackPointData
    *   
    *   @return nothing 
    */
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
                    path = Save_Load_Data.Tutorial_path;
                    break;
                }
            case 1:
                {
                    path = Save_Load_Data.Level_one_TrackData_path;
                    break;
                }
            case 2:
                {
                    path = Save_Load_Data.Level_two_TrackData_path;
                    break;
                }
            case 3:
                {
                    path = Save_Load_Data.Level_three_TrackData_path;
                    break;
                }
            case 4:
                {
                    path = Save_Load_Data.Level_four_TrackData_path;
                    break;
                }
        }

        Save_Load_Data.Save(path,saveDataString);
    }

    /**
    *   @brief A function to Call the Save_Load_Data.cs to Load TrackPointData of the level
    *  
    *   @param int level, level of the TrackPointData
    *   
    *   @return nothing 
    */
    public static void LoadTrackPointData(int Level)
    {
        TrackPointData DataList = new TrackPointData();

        String path = "";

        switch (Level)
        {
            case 0:
                {
                    path = Save_Load_Data.Tutorial_path;
                    break;
                }
            case 1:
                {
                    path = Save_Load_Data.Level_one_TrackData_path;
                    break;
                }
            case 2:
                {
                    path = Save_Load_Data.Level_two_TrackData_path;
                    break;
                }
            case 3:
                {
                    path = Save_Load_Data.Level_three_TrackData_path;
                    break;
                }
            case 4:
                {
                    path = Save_Load_Data.Level_four_TrackData_path;
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

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour {

    public TrainMovementManager Manager;

    public int ID;
    public float distanceTravel;
    public int Point_ID;
    private float distanceGap;
    public bool once;
	
	// Update is called once per frame
	void Update () {

        if (ID == 0)
            TrainRenderUpdate();

        if (ID == 666)
            TrainUnRenderUpdate();    

        if (ID != 0 && ID != 666)
            TrainMovementUpdate();
    }

    private void TrainRenderUpdate()
    {
        if (!once)
        {
            Point_ID = 0;
            CheckPosition(true);
            once = true;
            OrderExecution.Done = true;
        }

        if (AppManager.RenderingTrack)
        {
            distanceTravel += Time.deltaTime * Manager.RenderSpeed;
            CheckPosition(true);
        }
    }

    private void TrainUnRenderUpdate()
    {

       
        if (!once && Manager.once)
        {
            Point_ID = BezierCurve2.TrackData_List.Length;
            Point_ID -= 1;
            distanceTravel = Manager.TotalTrackDistance;
            CheckPosition(false);
            once = true;
            OrderExecution.Done = true;
        }

        if (AppManager.UnRenderingTrack)
        {
            distanceTravel -= Time.deltaTime * Manager.UnRenderSpeed;
            CheckPosition(false);
        }
    }

    private void TrainMovementUpdate()
    {
        if (!once && Manager.once)
        {
            for (int i = 0; i < Manager.TheTrain.Length; i++)
            {
                if (ID == Manager.TheTrain[i].ID)
                {
                    distanceGap = Manager.TheTrain[i].distanceGap;
                }

            }

            distanceTravel = 0 - distanceGap;

            if (distanceTravel < 0)
            {
                distanceTravel = Manager.TotalTrackDistance - distanceGap;
            }


            Point_ID = 0;
            CheckPosition(true);
            once = true;
        }

        if (LevelManager.TrianConnected && LevelManager.MoveOut && !LevelManager.ReachStation && BezierCurve2.Go && LevelManager.Play)
        {
            distanceTravel += Time.deltaTime * Manager.MainSpeed;
            CheckPosition(true);
        }
    }

    private void CheckPosition(bool T)
    {
        bool run = true;
        int Temp_Id = Point_ID;

        while (run)
        {
            if (T)
            {
                if (Temp_Id + 1 == BezierCurve2.TrackData_List.Length)
                {
                    Temp_Id = 0;
                    distanceTravel = 0;

                    if (ID == 0)
                    {
                        Point_ID = BezierCurve2.TrackData_List.Length;
                        gameObject.SetActive(false);
                        break;
                    }

                    if (ID == 1)
                    {
                        LevelManager.ReachStation = true;
                        gameObject.SetActive(false);
                        break;
                    }

                }
                if (distanceTravel >= BezierCurve2.TrackData_List[Temp_Id].distance)
                {
                    transform.position = BezierCurve2.TrackData_List[Temp_Id].position;
                    transform.LookAt(transform.position + BezierCurve2.TrackData_List[Temp_Id].tangent);
                }

                if (distanceTravel < BezierCurve2.TrackData_List[Temp_Id + 1].distance)
                {
                    Point_ID = Temp_Id;
                    run = false;
                }

                Temp_Id++;
            }
            else
            {
                if (Temp_Id == 0)
                {
                    Temp_Id = BezierCurve2.TrackData_List.Length;
                    Temp_Id -= 1;
                    distanceTravel = Manager.TotalTrackDistance;

                    if (ID == 666)
                    {
                        Point_ID = 0;
                        gameObject.SetActive(false);
                        break;
                    }
                }
                if (distanceTravel <= BezierCurve2.TrackData_List[Temp_Id].distance)
                {
                    transform.position = BezierCurve2.TrackData_List[Temp_Id].position;
                    transform.LookAt(transform.position - BezierCurve2.TrackData_List[Temp_Id].tangent);
                }

                if (distanceTravel > BezierCurve2.TrackData_List[Temp_Id - 1].distance)
                {
                    Point_ID = Temp_Id;
                    run = false;
                }

                Temp_Id--;
            }

            
        }
    }
    
}

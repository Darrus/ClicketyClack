using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour {

    public TrainMovementManager Manager;

    public int ID;
    public float distanceTravel;
    private int Point_ID;
    private float distanceGap;
    public bool once;
	
	// Update is called once per frame
	void Update () {

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

            if(distanceTravel < 0)
            {
                distanceTravel = Manager.TotalTrackDistance - distanceGap;
            }


            Point_ID = 0;
            CheckPosition();
            once = true;
        }

        if (LevelManager.TrianConnected && LevelManager.MoveOut && !LevelManager.ReachStation && BezierCurve2.Go)
        {
            distanceTravel += Time.deltaTime * Manager.MainSpeed;
            CheckPosition();
        }
    }

    private void CheckPosition()
    {
        bool run = true;
        int Temp_Id = Point_ID;

        while (run)
        {
            if (Temp_Id + 1 == BezierCurve2.Track_List.Length)
            {
                Temp_Id = 0;
                distanceTravel = 0;

                if(ID == 1)
                    LevelManager.ReachStation = true;
            }
            if (distanceTravel >= BezierCurve2.Track_List[Temp_Id].distance)
            {
                transform.position = BezierCurve2.Track_List[Temp_Id].position;
                transform.LookAt(transform.position + BezierCurve2.Track_List[Temp_Id].tangent);
            }

            if(distanceTravel < BezierCurve2.Track_List[Temp_Id + 1].distance)
            {
                Point_ID = Temp_Id;
                run = false;
            }

            Temp_Id++;
        }
    }
    
}

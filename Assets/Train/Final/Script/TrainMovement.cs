using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour {

    public float speed;

    private float distacneTravel;
    public int Point_ID;

	// Use this for initialization
	void Start () {
        distacneTravel = 0f;
        Point_ID = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if(LevelManager.TrianConnected && LevelManager.MoveOut && !LevelManager.ReachStation && BezierCurve2.Go)
        {
            distacneTravel += Time.deltaTime * speed;
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
                distacneTravel = 0;

                LevelManager.ReachStation = true;
            }

            if (distacneTravel >= BezierCurve2.Track_List[Temp_Id].distance)
            {
                transform.position = BezierCurve2.Track_List[Point_ID].position;
                transform.LookAt(transform.position + BezierCurve2.Track_List[Point_ID].tangent);
            }

            if(distacneTravel < BezierCurve2.Track_List[Temp_Id + 1].distance)
            {
                Point_ID = Temp_Id;
                run = false;
            }

            Temp_Id++;
        }
    }
    
}

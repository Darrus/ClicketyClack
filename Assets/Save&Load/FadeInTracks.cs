using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInTracks : MonoBehaviour {


    [System.Serializable]
    public struct Event_Data
    {
        public GameObject Object;
        public int Point_ID;
    }

    public GameObject TheRealTrack;

    public TrainMovement movingPoint;

    public List<GameObject> Inner;
    public List<GameObject> Outter;
    public List<GameObject> Rail;
    public List<int> RailList;
    public List<Event_Data> Event_Data_List;

    private int prePoint_ID;

    private int preRail_List_ID;
    private int currRail_List_ID;

    private int preEvent_List_ID;
    private int currEvent_List_ID;


    private void Awake()
    {
        OrderExecution.Done = true;
    }

    // Use this for initialization
    void Start () {
        prePoint_ID = 0;

        preRail_List_ID = 0;
        currRail_List_ID = -1;

        preEvent_List_ID = 0;
        currEvent_List_ID = -1;

        TheRealTrack.SetActive(false);

        for (int i = 0; i < Event_Data_List.Count; i++)
        {
            Event_Data_List[i].Object.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {

        int temp = 0;

        if(prePoint_ID != Inner.Count)
        {
            if (movingPoint.Point_ID < prePoint_ID)
                temp = Inner.Count;
            else
                temp = movingPoint.Point_ID;

            RenderTrack_Part_1(temp);

            if(preRail_List_ID != RailList.Count)
                RenderTrack_Part_2();

            if (preEvent_List_ID != Event_Data_List.Count)
                RenderEvent();
        }
        else
        {
            TheRealTrack.SetActive(true);
            movingPoint.transform.gameObject.SetActive(false);
            LevelManager.MoveOut = true;
            gameObject.SetActive(false);
        }
    }

    void RenderTrack_Part_1(int temp)
    {
        for (int i = prePoint_ID; i < temp; i++)
        {
            Renderer inner_RD = Inner[i].GetComponent<Renderer>();
            Renderer outter_RD = Outter[i].GetComponent<Renderer>();

            inner_RD.enabled = true;
            outter_RD.enabled = true;
        }

        prePoint_ID = temp;
    }

    void RenderTrack_Part_2()
    {
        for (int i = preRail_List_ID; i < RailList.Count; i++)
        {
            if (RailList[i] < prePoint_ID)
            {
                currRail_List_ID = i;
                break;
            }
        }

        if (currRail_List_ID >= 0)
        {
            for (int i = preRail_List_ID; i <= currRail_List_ID; i++)
            {
                Renderer Rail_RD = Rail[i].GetComponent<Renderer>();
                Rail_RD.enabled = true;
            }

            preRail_List_ID = currRail_List_ID+1;
        }
    }

    void RenderEvent()
    {
        for (int i = preEvent_List_ID; i < Event_Data_List.Count; i++)
        {
            if (Event_Data_List[i].Point_ID < prePoint_ID)
            {
                currEvent_List_ID = i;
                break;
            }
        }

        if(currEvent_List_ID >= 0)
        {
            for (int i = preEvent_List_ID; i <= currEvent_List_ID; i++)
                Event_Data_List[i].Object.SetActive(true);

            preEvent_List_ID = currEvent_List_ID+1;
        }
    }

    public void GetPointObject(GameObject T, int ID)
    {
        Event_Data temp = new Event_Data();
        temp.Object = T;
        temp.Point_ID = BezierCurve2.getTotalCruveStepsTo(ID);

        Event_Data_List.Add(temp);

        for (int i = 0; i < Event_Data_List.Count; i++)
        {
            if(Event_Data_List[i].Point_ID > temp.Point_ID)
            {
                //insert here

                for(int n = Event_Data_List.Count-1; n > i; n--)
                {
                    Event_Data_List[n] = Event_Data_List[n - 1];
                }

                Event_Data_List[i] = temp;
                break;
            }
        }

        temp.Object.SetActive(false);
    }
}


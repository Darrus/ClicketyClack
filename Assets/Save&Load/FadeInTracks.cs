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

    public TrainMovement RenderPoint;
    public TrainMovement UnRenderPoint;

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

        if (AppManager.Instance.RenderingTrack && !AppManager.Instance.UnRenderingTrack)
        {
            RenderAll();
        }

        if (AppManager.Instance.UnRenderingTrack && !AppManager.Instance.RenderingTrack)
        {
            if (TheRealTrack.activeSelf)
                TheRealTrack.SetActive(false);

            UnRenderAll();
        }
    }

    void RenderAll()
    {
        int temp = 0;
        if (prePoint_ID != BezierCurve2.TrackData_List.Length)
        {
            if (RenderPoint.Point_ID < prePoint_ID)
                temp = Inner.Count;
            else
                temp = RenderPoint.Point_ID;

            RenderTrack_Part_1(temp);

            if (preRail_List_ID != RailList.Count)
                RenderTrack_Part_2();

            if (preEvent_List_ID != Event_Data_List.Count)
                RenderEvent();
        }
        else
        {
            AppManager.Instance.RenderingTrack = false;
            TheRealTrack.SetActive(true);
        }
    }

    void UnRenderAll()
    {
        int temp = 0;

        if (prePoint_ID != 0)
        {
            if(prePoint_ID == Inner.Count)
            {
                prePoint_ID -= 1;
            }

            if (UnRenderPoint.Point_ID > prePoint_ID)
                temp = 0;
            else
                temp = UnRenderPoint.Point_ID;

            UnRenderTrack_Part_1(temp);

            if (preRail_List_ID != -1)
                UnRenderTrack_Part_2();

            if (preEvent_List_ID != -1)
                UnRenderEvent();
        } 
        else
        {
            AppManager.Instance.UnRenderingTrack = false;
            AppManager.Instance.ChangeScene();
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

    void UnRenderTrack_Part_1(int temp)
    {
        for (int i = prePoint_ID; i > temp; i--)
        {
            Renderer inner_RD = Inner[i].GetComponent<Renderer>();
            Renderer outter_RD = Outter[i].GetComponent<Renderer>();

            inner_RD.enabled = false;
            outter_RD.enabled = false;
        }

        prePoint_ID = temp;
    }

    void UnRenderTrack_Part_2()
    {

        if (RailList.Count == preRail_List_ID)
        {
            preRail_List_ID -= 1;
            currRail_List_ID = preRail_List_ID;
        }

        for (int i = preRail_List_ID; i >= 0; i--)
        {
            if (RailList[i] < prePoint_ID)
            {
                currRail_List_ID = i;
                break;
            }

            if (currRail_List_ID == 0)
            {
                currRail_List_ID = -1;
                break;
            }
        }


        for (int i = preRail_List_ID; i > currRail_List_ID; i--)
        {
            Renderer Rail_RD = Rail[i].GetComponent<Renderer>();
            Rail_RD.enabled = false;
        }

      

        preRail_List_ID = currRail_List_ID;
    }

    void UnRenderEvent()
    {
        if(Event_Data_List.Count == preEvent_List_ID)
        {
            preEvent_List_ID -= 1;
            currEvent_List_ID = preEvent_List_ID;
        }

        for (int i = preEvent_List_ID; i >= 0; i--)
        {
            if (Event_Data_List[i].Point_ID < prePoint_ID)
            {
                currEvent_List_ID = i;
                break;
            }

            if(currEvent_List_ID == 0)
            {
                currEvent_List_ID = -1;
                break;
            }
        }


        for (int i = preEvent_List_ID; i > currEvent_List_ID; i--)
        {
            if(Event_Data_List[i].Object != null)
                Event_Data_List[i].Object.SetActive(false);
        }

        preEvent_List_ID = currEvent_List_ID;
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


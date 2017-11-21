/** 
*  @file    FadeInTracks.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief Contain class FadeInTracks
*  
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*  @brief A class that manager Rendering/UnRendering the track
*/
public class FadeInTracks : MonoBehaviour {

    /**
    *  @brief Struct of Game Event Data
    */
    [System.Serializable]
    public struct Event_Data
    {
        public GameObject Object; ///< GameObject of the Event
        public int Point_ID; ///< ID of the Event on the Sub-Point of the Track
    }

    public GameObject TheRealTrack; ///< GameObject of the Actual Whole Track Mesh

    public TrainMovement RenderPoint; ///< TrainMovement of the rendering point
    public TrainMovement UnRenderPoint; ///< TrainMovement of the unrendering point

    public List<GameObject> Inner; ///< List of GameObject of the individual parts of the Track Mesh of the inner Steel rail
    public List<GameObject> Outter; ///< List of GameObject of the individual parts of the Track Mesh of the outter Steel rail
    public List<GameObject> Rail; ///< List of GameObject of the individual parts of the Track Mesh of the Rail Plate
    public List<int> RailList;  ///< List of ID of the sub-point for the individual parts of the Track Mesh of the Rail Plate
    public List<Event_Data> Event_Data_List; ///< list of Struct Event_Data

    private int prePoint_ID; ///< previous sub-Point ID

    private int preRail_List_ID; ///< previous Rail Plate List ID
    private int currRail_List_ID; ///< Current Rail Plate List ID

    private int preEvent_List_ID; ///< previous Event Data List ID
    private int currEvent_List_ID; ///< Current Event Data List ID

    /**
    *  @brief InActive all gameobject in Event_Data_List
    */
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

    /**
    *  @brief Render All Individual Track Mesh and Event Gameobject as it follow along TrainMovement RenderPoint
    *  
    *  @return null
    */
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

    /**
    *  @brief unRender All Individual Track Mesh and Event Gameobject as it follow along TrainMovement UnRenderPoint
    *  
    *  @return null
    */
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

    /**
    *  @brief Render Individual Steel Rail Track Mesh till a specific sub-Point
    *  
    *  @param int temp, ID of sub-Point to render to
    *  
    *  @return null
    */
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

    /**
    *  @brief Render Individual Rail Plate Track Mesh till to prePoint_ID of the sub-point
    *  
    *  @return null
    */
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

    /**
    *  @brief Render Individual Event Gameobject till to prePoint_ID of the sub-point
    *  
    *  @return null
    */
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

    /**
    *  @brief unRender Individual Steel Rail Track Mesh till a specific sub-Point
    *  
    *  @param int temp, ID of sub-Point to unrender to
    *  
    *  @return null
    */
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

    /**
    *  @brief unRender Individual Rail Plate Track Mesh till to prePoint_ID of the sub-point
    *  
    *  @return null
    */
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

    /**
    *  @brief unRender Individual Event Gameobject till to prePoint_ID of the sub-point
    *  
    *  @return null
    */
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


    /**
    *  @brief pushing the traffic Light Gameobject into the Event_Data_List (traffic Light only create in runtime)
    *  
    *  @param GameObject T, the traffic Light 
    *  
    *  @param int ID, id of the wayPoint of which the traffic Light belong to
    * 
    *  @return null
    */
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


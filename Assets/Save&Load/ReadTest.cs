using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct SaveData
{
    public Track_Point[] points;
    public Track_Event[] events;
}

[System.Serializable]
public struct Track_Point
{
    public Vector3 position;
    public int ID;
    public int Type;
}

[System.Serializable]
public struct Track_Event
{
    public Vector3 position;
    public Quaternion rotation;
    public int Type;
}

public class ReadTest : MonoBehaviour
{

    public SaveData SaveData;

    public PointManager pointManager;
    public EventManager eventManager;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.rotation

        if (Input.GetKeyDown("2"))
        {
            Get_All_Points();
            Get_All_Events();

            String saveDataString = JsonUtility.ToJson(SaveData);
            Save_Load_Data.Save(saveDataString);
        }

        if (Input.GetKeyDown("3"))
        {
            if (Save_Load_Data.Check_SaveFile())
            {
                BezierCurve2.ClearAllData(); // remove in the furture

                SaveData = JsonUtility.FromJson<SaveData>(Save_Load_Data.load());
                Create_All_Points();
                Create_All_Events();
            }
        }

    }


    void Get_All_Points()
    {
        Array.Resize(ref SaveData.points, BezierCurve2.GO_Points.Length);

        for (int i = 0; i < BezierCurve2.GO_Points.Length; i++)
        {
            MainPoints tempPoint = BezierCurve2.GO_Points[i].GetComponent(typeof(MainPoints)) as MainPoints;

            SaveData.points[i].position = tempPoint.transform.position;
            SaveData.points[i].ID = tempPoint.ID;
            SaveData.points[i].Type = tempPoint.type;
        }

    }

    void Create_All_Points()
    {
        for (int i = 0; i < SaveData.points.Length; i++)
        {
            pointManager.AddNewPoints(SaveData.points[i].position, SaveData.points[i].ID, SaveData.points[i].Type);
        }
    }

    void Get_All_Events()
    {
        GameObject[] EventList = GameObject.FindGameObjectsWithTag("Event");

        Array.Resize(ref SaveData.events, EventList.Length);

        for (int i = 0; i < EventList.Length; i++)
        {
            EventSC tempEvent = EventList[i].GetComponent(typeof(EventSC)) as EventSC;

            SaveData.events[i].position = tempEvent.transform.position;
            SaveData.events[i].rotation = tempEvent.transform.rotation;
            SaveData.events[i].Type = tempEvent.type;
        }

    }

    void Create_All_Events()
    {
        for (int i = 0; i < SaveData.events.Length; i++)
        {
            eventManager.CreateEvents(SaveData.events[i].position, SaveData.events[i].rotation, SaveData.events[i].Type);
        }
    }

}
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

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.rotation

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Get_All_Points();

            String saveDataString = JsonUtility.ToJson(SaveData);
            Save_Load_Data.Save(saveDataString);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Save_Load_Data.Check_SaveFile())
            {
                BezierCurve2.ClearAllData(); // remove in the furture

                SaveData = JsonUtility.FromJson<SaveData>(Save_Load_Data.load());
                Create_All_Points();
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
}
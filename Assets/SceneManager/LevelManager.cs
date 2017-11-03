using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static bool TrianConnected;
    public static bool TrianOnGround;

    public static bool ReachStation;
    public static bool MoveOut;

    public static bool CargoOn;

    public static bool Play;

    public GameObject Room_Items;

    public GameObject AppPrefab;
    public PointManager pointManager;
    public bool Tutorial;

    public static LevelManager Singleton = null;

    public static LevelManager Instance
    {
        get { return Singleton; }
    }

    private float TimeToRollOut;

    void Awake()
    {
        Debug.Log("Level: Starting.");

        if (Singleton != null)
        {
            Debug.LogError("Multiple Level Singletons exist!");
            return;
        }
        Singleton = this;

        // Is the controlling App already in existence?
        if (GameObject.Find("App") == null)
        {
            Instantiate(AppPrefab);

            Debug.Log("Creating temporary App");
        }

        Play = true;
        Room_Items.SetActive(true);
    }

    public static void Add_Child_ToRoom(LevelManager Temp)
    {
        GameObject Room = GameObject.FindGameObjectWithTag("TheRoom");
        Debug.Log(Room.name + " : " + Room.tag);
        Temp.Room_Items.transform.SetParent(Room.transform);
        Debug.Log("Room Child Added");
    }

    void Start()
    {
        TrianConnected = true;
        TrianOnGround = false;
        ReachStation = true;
        MoveOut = false;
        CargoOn = false;
        TimeToRollOut = 10f;

        if(!Tutorial)
        {
            CargoOn = true;
        }

    }

    void Update()
    {

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveOut = true;
            ReachStation = false;
            CargoOn = true;
        }
#endif
        if (!MoveOut && CargoOn && ReachStation)
        {
            if (!Play)
            {
                TimeToRollOut = 10f;
            }
            else
            {

                if (TimeToRollOut >= 0)
                    TimeToRollOut -= Time.deltaTime;
                else
                {
                    pointManager.UpdatePoints();
                    Debug.Log("Points Updated");

                    MoveOut = true;
                    ReachStation = false;
                    Debug.Log("GO out");
                }
            }
        }

        Check_Win_Lose_Condition();
    }

    void Check_Win_Lose_Condition()
    {
        if (!TrianConnected && TrianOnGround)
        {
            ResetLevel();
        }

        if (ReachStation && MoveOut)
        {
            AppManager.NextLevel(AppManager.Singleton);

            // CLEAR
            TextControll.textNum = 4;
        }

    }

    void ResetLevel()
    {
        AppManager.Detach_RoomChild(AppManager.Singleton);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

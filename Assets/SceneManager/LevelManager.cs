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

    public GameObject Room_Items;

    public GameObject AppPrefab;


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
        //GameObject Room = GameObject.Find("TheRoom");
        //Room_Items.transform.SetParent(Room.transform);
    }

    void Start()
    {
        TrianConnected = true;
        TrianOnGround = false;
        ReachStation = false;
        MoveOut = false;
        CargoOn = false;
        TimeToRollOut = 5f;
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
        if (!MoveOut && CargoOn)
        {
            if (TimeToRollOut >= 0)
                TimeToRollOut -= Time.deltaTime;
            else
            {
                MoveOut = true;
                ReachStation = false;
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
        }

    }

    void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AppManager.Detach_RoomChild(AppManager.Singleton);
    }

}

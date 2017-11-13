﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [System.Serializable]
    public struct Train
    {
        public bool head;
        public bool Carriage;
        public bool Cargo;
        public int Life;

        public Train(bool T)
        {
            head = T;
            Carriage = T;
            Cargo = T;
            Life = 3;
        }

        public void killHead()
        {
            head = false;
            Carriage = false;
            Cargo = false;
            Life = 0;
        }

        public void KillCarriage()
        {
            Carriage = false;
            Cargo = false;
            Life = 1;
        }
        public void KillCargo()
        {
            Cargo = false;
            Life = 2;
        }
    };
    public static bool TrianOnGround;

    public static bool ReachStation;
    public static bool MoveOut;

    public static bool CargoOn;

    public static bool Play;

    public GameObject Room_Items;

    public GameObject AppPrefab;
    public PointManager pointManager;
    public bool Tutorial;

    public static Train TheTrainLife = new Train(true);

    public static LevelManager Singleton = null;

    public static LevelManager Instance
    {
        get { return Singleton; }
    }

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
        OrderExecution.Done = true;
    }

    public static void Add_Child_ToRoom(LevelManager singleton)
    {
        GameObject Room = GameObject.FindGameObjectWithTag("TheRoom");
        Debug.Log(Room.name + " : " + Room.tag);
        singleton.Room_Items.transform.SetParent(Room.transform);
        Debug.Log("Room Child Added");
    }

    void Start()
    {
        TrianOnGround = false;
        ReachStation = true;
        MoveOut = false;
        CargoOn = false;

        if (!Tutorial)
        {
            CargoOn = true;
        }

    }

    void Update()
    {
        if (!MoveOut && CargoOn && ReachStation && OrderExecution.Singleton != null)
        {
            if(OrderExecution.LifeGoalReached)
            {
                AppManager.RenderingTrack = true;
                BezierCurve2.Go = true;
                ReachStation = false;
                OrderExecution.SelfDestory(OrderExecution.Singleton);
            }
        }

        Check_Win_Lose_Condition();



    }

    void Check_Win_Lose_Condition()
    {
        if (TheTrainLife.Life == 0 && TrianOnGround && !AppManager.ReStartLevel)
        {
            AppManager.ReStartLevel = true;
        }

        if (ReachStation && MoveOut)
        {
            AppManager.NextLevel(AppManager.Singleton);

            TextControll.textNum = 4;
        }

    }

    public static void SelfDestory(LevelManager singleton)
    {
        LevelManager.Destroy(singleton.gameObject);
    }


}

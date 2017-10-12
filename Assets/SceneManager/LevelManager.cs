﻿using UnityEngine;
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

    public GameObject AppPrefab;

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
    }

    void Start()
    {
        TrianConnected = true;
        TrianOnGround = false;
        ReachStation = false;
        MoveOut = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveOut = true;
            ReachStation = false;
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
    }

}
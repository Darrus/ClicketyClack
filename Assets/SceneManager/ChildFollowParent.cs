﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildFollowParent : MonoBehaviour
{

    private bool once;

    void Awake()
    {
        once = true;
        Debug.Log("Room Childposition start");
        if (AppManager.curScene == (int)AppManager.GameScene.mainmenu)
        {
            MainMenuManager.Add_Child_ToRoom(MainMenuManager.Singleton);
        }
        else
        {
            Debug.Log(LevelManager.Singleton);
            LevelManager.Add_Child_ToRoom(LevelManager.Singleton);
            Debug.Log("Room Child Test");
        }

        OrderExecution.Done = true;
    }
    // Use this for initialization
    void Start()
    {
        if (once && transform.parent != null)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.identity;

            Debug.Log("Room Child loaded");

            Debug.Log("Room Child's P :" + transform.position);

            once = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (once && transform.parent != null)
        {
            Debug.Log("Room Child Order Wrong");
        }
    }
}

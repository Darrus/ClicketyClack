using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [HideInInspector]
    public bool ReachStation;
    [HideInInspector]
    public bool MoveOut;
    [HideInInspector]
    public bool CargoOn;
    [HideInInspector]
    public bool Play;

    public GameObject Room_Items;

    public GameObject AppPrefab;

    public PointManager pointManager;

    public bool Tutorial;

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

        Play = false;
        BezierCurve2.Go = false;
        
    }

    public void Add_Child_ToRoom()
    {
        GameObject Room = GameObject.FindGameObjectWithTag("TheRoom");
        Debug.Log(Room.name + " : " + Room.tag);
        Room_Items.transform.SetParent(Room.transform);
        Debug.Log("Room Child Added");
    }

    void Start()
    {
        ReachStation = false;
        MoveOut = false;
        CargoOn = false;

        if (!Tutorial)
        {
            CargoOn = true;
        }
        OrderExecution.Instance.Done = true;
    }

    void Update()
    {

        if (Tutorial && !CargoOn)
        {
            AppManager.Instance.RenderingTrack = true;
            BezierCurve2.Go = true;
        }

        if (Tutorial && CargoOn && !AppManager.Instance.RenderingTrack)
        {
            Button_Play();
        }

        if (!MoveOut && CargoOn && !Tutorial)
        {
            AppManager.Instance.RenderingTrack = true;
            MoveOut = true;
            BezierCurve2.Go = true;
        }
    }

    public void Button_Menu()
    {
        AppManager.Instance.RenderingTrack = false;
        AppManager.Instance.curScene = AppManager.GameScene.mainmenu;
        AppManager.Instance.LoadScene();
    }

    public void Button_Play()
    {
        Play = true;
    }

    public void Button_Pause()
    {
        Play = false;
    }

    public void SelfDestory()
    {
        Singleton = null;
        if(pointManager != null)
            pointManager.WayPointList.SetActive(false);
        GameObject.Destroy(gameObject);
    }


}

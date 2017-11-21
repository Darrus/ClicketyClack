/** 
*  @file    LevelManager.cs
*  @author  Yin Shuyu (150713R) 
*  
*  @brief Contain Singleton class LevelManager
*  
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.SceneManagement;

/**
*  @brief Singleton Class for Level Management for setting Some Game State purpose 
*/
public class LevelManager : MonoBehaviour {

    [HideInInspector]
    public bool ReachStation; ///< bool trigger when Train Reach Station after one round
    [HideInInspector]
    public bool MoveOut; ///< bool Trigger when all Track preparation is done
    [HideInInspector]
    public bool CargoOn; ///< bool check for Cargo Connect to the Train
    [HideInInspector]
    public bool Play; ///< Bool Trigger for Starting the Train

    public GameObject Room_Items; ///< GameObject of the RoomItem needed be align with the World Anchor

    public PointManager pointManager; ///< PointManager point Manager of current level

    public bool Tutorial; ///< bool Check Whether level is Tutorial, tutorial run codes differently

    public static LevelManager Singleton = null; ///< Static Singleton of the LevelManager

    public static LevelManager Instance ///< Static Instance function to get all the Data of LevelManager
    {
        get { return Singleton; }
    }

    /**
    *  @brief At the Awake of the gameobject, need to set the Singleton 
    */
    void Awake()
    {
        Debug.Log("Level: Starting.");

        if (Singleton != null)
        {
            Debug.LogError("Multiple Level Singletons exist!");
            return;
        }
        Singleton = this;

        Play = false;
        BezierCurve2.Go = false;
        
    }

    /**
	*   @brief Function to Add the Room_Items to the World Anchor's GameObject
	*  
	*   @return null
	*/
    public void Add_Child_ToRoom()
    {
        GameObject Room = GameObject.FindGameObjectWithTag("TheRoom");
        Debug.Log(Room.name + " : " + Room.tag);
        Room_Items.transform.SetParent(Room.transform);
        Debug.Log("Room Child Added");
    }

    /**
    *  @brief Setting some Default values
    */
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

    /**
    *  @brief Update Game State when condition met
    */
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

    /**
	*   @brief Change Scene to MainMenu Scene
	*  
	*   @return null
	*/
    public void Button_Menu()
    {
        AppManager.Instance.RenderingTrack = false;
        AppManager.Instance.gameState = AppManager.GameScene.mainmenu;
        AppManager.Instance.LoadScene();
    }

    /**
	*   @brief Allow the train to Move
	*  
	*   @return null
	*/
    public void Button_Play()
    {
        Play = true;
    }

    /**
	*   @brief Dont Allow the train to Move
	*  
	*   @return null
	*/
    public void Button_Pause()
    {
        Play = false;
    }

    /**
	*   @brief to Destroy LevelManager's Singleton and gameObject
	*  
	*   @return null
	*/
    public void SelfDestory()
    {
        Singleton = null;
        if(pointManager != null)
            pointManager.WayPointList.SetActive(false);
        GameObject.Destroy(gameObject);
    }


}

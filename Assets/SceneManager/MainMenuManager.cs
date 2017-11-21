/** 
*  @file    MainMenuManager.cs
*  @author  Yin Shuyu (150713R) 
*  
*  @brief Contain Singleton class MainMenuManager
*  
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

/**
*  @brief Singleton Class for Main Menu Management for Selecting Level
*/
public class MainMenuManager : MonoBehaviour
{

    public GameObject Room_Items; ///< GameObject of the RoomItem needed be align with the World Anchor

    public static MainMenuManager Singleton = null; ///< Static Singleton of the MainMenuManager

    public static MainMenuManager Instance ///< Static Instance function to get all the Data of MainMenuManager
    {
        get { return Singleton; }
    }

	/**
    *  @brief At the Awake of the gameobject, need to set the Singleton 
    */
    void Awake()
    {
        Debug.Log("MainMenu: Starting.");

        if (Singleton != null)
        {
            Debug.LogError("Multiple MainMenu Singletons exist!");
            return;
        }
        Singleton = this;

        OrderExecution.Instance.Done = true;
    }

	/**
	*   @brief Function to Add the Room_Items to the World Anchor's GameObject
	*  
	*   @return null
	*/
    public void Add_Child_ToRoom()
    {
        GameObject Room = GameObject.FindGameObjectWithTag("TheRoom");
        Room_Items.transform.SetParent(Room.transform);
    }

    void Start()
    {

    }

    void Update()
    {
    }

	/**
	*   @brief Change Scene to Tutorial Scene
	*  
	*   @return null
	*/
    public void Button_Tutorial()
    {
        AppManager.Instance.gameState = AppManager.GameScene.Tutorial;
        AppManager.Instance.LoadScene();
    }

	/**
	*   @brief Set gameState to Level 1 Scene
	*  
	*   @return null
	*/
    public void Button_Level_1()
    {
        AppManager.Instance.gameState = AppManager.GameScene.level_1;
    }

	/**
	*   @brief Set gameState to Level 2 Scene
	*  
	*   @return null
	*/
    public void Button_Level_2()
    {
        AppManager.Instance.gameState = AppManager.GameScene.level_2;
    }

	/**
	*   @brief Set gameState to Level 3 Scene
	*  
	*   @return null
	*/
    public void Button_Level_3()
    {
        AppManager.Instance.gameState = AppManager.GameScene.level_3;
    }

	/**
	*   @brief Set gameState to Level 4 Scene
	*  
	*   @return null
	*/
    public void Button_Level_4()
    {
        AppManager.Instance.gameState = AppManager.GameScene.level_4;
    }

	/**
	*   @brief Change Scene acording to gameState
	*  
	*   @return null
	*/
    public void Button_Load_Level()
    {
        AppManager.Instance.LoadScene();
    }

	/**
	*   @brief To quit Application
	*  
	*   @return null
	*/
    public void Button_Quit()
    {
        AppManager.Instance.Quit();
    }

	/**
	*   @brief to Destroy MainMenuManager's Singleton and gameObject
	*  
	*   @return null
	*/
    public void SelfDestory()
    {
        Singleton = null;
        GameObject.Destroy(gameObject);
    }

}
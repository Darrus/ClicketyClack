/** 
*  @file    AppManager.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief Contain Singleton class AppManager
*  
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

/**
*  @brief A Singleton Class for all Scene Management and as well as game state
*/
public class AppManager : MonoBehaviour {

    public String GameTitle = "ClicketyClack"; ///< String GameTitle

    public String MainMenu_Scene; ///< String Mainmenu scene name
    public String Level_1_Scene; ///< String Level 1 scene name
    public String Level_2_Scene; ///< String Level 2 scene name
    public String Level_3_Scene; ///< String Level 3 scene name
    public String Level_4_Scene; ///< String Level 4 scene name
    public String Tutorial_Scene; ///< String Tutorial scene name
    public String TempChange_Scene; ///< String Temp Restart Change Scene name (currently hardcore, cant save level in prefab bacasue of track mesh, furture can save level data, and restart level data but not the envriment and the track)

    [HideInInspector]
    public String NextScene_Name; ///< String Scene name of scene needed to changed to  
    [HideInInspector]
    public String CurrScene_Name; ///< String current Active Scene name

    /**
    *  @brief Enum of All Game Scenes
    */
    [System.Serializable]
    public enum GameScene
    {
        mainmenu = 0,
        level_1 = 1,
        level_2 = 2,
        level_3 = 3,
        level_4 = 4,
        Tutorial = 5,
        Customization = 6,

        TotalScene = 7
    };

    [HideInInspector]
    public GameScene gameState; ///< Scene of the game state should be
    [HideInInspector]
    public bool ReStartLevel; ///< bool trigger of restart the same scene (currently hardcored)
    [HideInInspector]
    public bool RenderingTrack; ///< bool trigger for rendering the track 
    [HideInInspector]
    public bool UnRenderingTrack; ///< bool trigger for unrendering the track 

    public GameScene TestScene; ///< Scene of the application to change to at the start application run (UNITY_EDITOR) 

    public GameObject TheRoom; ///< GamObject that will be apply the world anchor to, also the all the visualable gameobjects should be

    public static AppManager Singleton = null; ///< Static Singleton of the AppManager

    public static AppManager Instance ///< Static Instance function to get all the Data of the AppManager
    {
        get { return Singleton; }
    }

    /**
    *  @brief At the Awake of the gameobject, need to set the Singleton and all Scene Data to current "Application" scene
    */
    void Awake()
    {
        Debug.Log("App Awake!");

        if (Singleton != null)
        {
            Debug.LogError("Multiple App Singletons exist!");
            return;
        }
        Singleton = this;

        NextScene_Name = "Application";
        CurrScene_Name = "Application";

        RenderingTrack = false;
        UnRenderingTrack = false;
        ReStartLevel = false;
    }

    /**
    *  @brief Set GameObject DontDestroyOnLoad, (UNITY_EDITOR) Change scene into TestScene
    */
    void Start()
    {
        // Are we in the Current Scene?
        if (SceneManager.GetActiveScene().name == "Application")
        {
            // Make sure this object persists between scene loads.
            DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
            gameState = TestScene;
            LoadScene();
#endif
        }
    }

    /**
    *  @brief If Condition met, UnRender Track or ChangeScene
    */
    void Update()
    {
        if (SceneManager.GetSceneByName(NextScene_Name).isLoaded && CurrScene_Name != NextScene_Name && !UnRenderingTrack)
        {
                if (CurrScene_Name != MainMenu_Scene && CurrScene_Name != "Application" && CurrScene_Name != TempChange_Scene)
                    UnRenderingTrack = true;

                if (OrderExecution.Singleton != null && OrderExecution.Instance.AllDone && !UnRenderingTrack)
                    ChangeScene();
        }

    }

   /**
   *   @brief Function call when World Anchor done loading, change the Game Scene to main menu
   *  
   *   @return null
   */
    public void StartGame()
    {
        gameState = GameScene.mainmenu;
        SceneManager.LoadScene(MainMenu_Scene);
    }

   /**
   *   @brief Remove Room Child objects then Change Current active scene, and unload the old scene
   *  
   *   @return null
   */
    public void ChangeScene()
    {
        Detach_RoomChild();

        OrderExecution.Instance.LifeGoalReached = true;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(NextScene_Name));

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(CurrScene_Name).buildIndex);

        CurrScene_Name = NextScene_Name;

        if (ReStartLevel && CurrScene_Name == TempChange_Scene)
        {
            ReStartLevel = false;
            LoadScene();
        }
    }

    /**
   *   @brief Quit Application Function //TODO
   *  
   *   @return null
   */
    public void Quit()
    {
        Debug.Log("App: Terminating.");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_WSA
        //Application.Current.Exit();
        //Application.Quit();
#endif

    }

    /**
    *   @brief Async Load the next Scene to preload scene data
    *  
    *   @return null
    */
    public void LoadScene()
    {
        CheckAndDestoryManagers();

        switch (gameState)
        {
            case GameScene.mainmenu:
                {
                    NextScene_Name = MainMenu_Scene;
                    SceneManager.LoadSceneAsync(MainMenu_Scene, LoadSceneMode.Additive);
                    break;
                }
            case GameScene.level_1:
                {
                    NextScene_Name = Level_1_Scene;

                    if (ReStartLevel)
                    {
                        NextScene_Name = TempChange_Scene;
                        SceneManager.LoadSceneAsync(TempChange_Scene, LoadSceneMode.Additive);
                    }
                    else
                        SceneManager.LoadSceneAsync(Level_1_Scene, LoadSceneMode.Additive);

                    break;
                }
            case GameScene.level_2:
                {
                    NextScene_Name = Level_2_Scene;

                    if (ReStartLevel)
                    {
                        NextScene_Name = TempChange_Scene;
                        SceneManager.LoadSceneAsync(TempChange_Scene, LoadSceneMode.Additive);
                    }
                    else
                        SceneManager.LoadSceneAsync(Level_2_Scene, LoadSceneMode.Additive);

                    break;
                }
            case GameScene.level_3:
                {
                    NextScene_Name = Level_3_Scene;

                    if (ReStartLevel)
                    {
                        NextScene_Name = TempChange_Scene;
                        SceneManager.LoadSceneAsync(TempChange_Scene, LoadSceneMode.Additive);
                    }
                    else
                        SceneManager.LoadSceneAsync(Level_3_Scene, LoadSceneMode.Additive);

                    break;
                }
            case GameScene.level_4:
                {
                    NextScene_Name = Level_4_Scene;

                    if (ReStartLevel)
                    {
                        NextScene_Name = TempChange_Scene;
                        SceneManager.LoadSceneAsync(TempChange_Scene, LoadSceneMode.Additive);
                    }
                    else
                        SceneManager.LoadSceneAsync(Level_4_Scene, LoadSceneMode.Additive);

                    break;
                }
            case GameScene.Tutorial:
                {
                    NextScene_Name = Tutorial_Scene;

                    if (ReStartLevel)
                    {
                        NextScene_Name = TempChange_Scene;
                        SceneManager.LoadSceneAsync(TempChange_Scene, LoadSceneMode.Additive);
                    }
                    else
                        SceneManager.LoadSceneAsync(Tutorial_Scene, LoadSceneMode.Additive);

                    break;
                }
        }
    }

    /**
    *   @brief update the new GameState according the old GameState
    *  
    *   @return null
    */
    public void NextLevel()
    {
        switch (gameState)
        {
            case GameScene.level_1:
                {
                    gameState = GameScene.level_2;
                    break;
                }
            case GameScene.level_2:
                {
                    gameState = GameScene.level_3;
                    break;
                }
            case GameScene.level_3:
                {
                    gameState = GameScene.level_4;
                    break;
                }
            case GameScene.level_4:
                {
                    gameState = GameScene.mainmenu;
                    break;
                }
            case GameScene.Tutorial:
                {
                    gameState = GameScene.mainmenu;
                    break;
                }
        }
        LoadScene();
    }

    /**
    *   @brief Detory the Child GameObjects in TheRoom
    *  
    *   @return null
    */
    public void Detach_RoomChild()
    {
        Destroy(TheRoom.transform.GetChild(0).gameObject);
    }

    /**
    *   @brief Detory all the current Level,menu,game State Managers, prepare for the next scene, prevent crush with next scene's managers
    *  
    *   @return null
    */
    private void CheckAndDestoryManagers()
    {
        if(LevelManager.Singleton != null)
        {
            LevelManager.Instance.SelfDestory();
        }

        if(MainMenuManager.Singleton != null)
        {
            MainMenuManager.Instance.SelfDestory();
        }

        if(GameBoard.Singleton != null)
        {
            GameBoard.Instance.SelfDestory();
        }
    }
}

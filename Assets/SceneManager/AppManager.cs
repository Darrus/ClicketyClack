using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour {

    public String GameTitle = "ClicketyClack";

    public String MainMenu;
    public String Level_1;
    public String Level_2;
    public String Level_3;
    public String Level_4;
    public String Tutorial;

    [System.Serializable]
    public enum GameScene
    {
        mainmenu = 0,
        level_1 = 1,
        level_2 = 2,
        level_3 = 3,
        level_4 = 4,
        Tutorial = 5,
        TestLevelCreation = 6

    };

    private static bool once;

    public static int curScene;

    public static String curScene_Name;
    public static String preScene_Name;

    public static bool ReStartLevel;

    public static bool RenderingTrack;
    public static bool UnRenderingTrack;

    public GameScene TestScene;

    public GameObject TheRoom;

    public static AppManager Singleton = null;

    public static AppManager Instance
    {
        get { return Singleton; }
    }
   
    void Awake()
    {
        Debug.Log("App Awake!");

        if (Singleton != null)
        {
            Debug.LogError("Multiple App Singletons exist!");
            return;
        }
        Singleton = this;

        curScene_Name = "Application";
        preScene_Name = "Application";

        RenderingTrack = false;
        UnRenderingTrack = false;
        ReStartLevel = false;
    }
   
    void Start()
    {
        // Are we in the Current Scene?
        if (SceneManager.GetActiveScene().name == "Application")
        {
            // Make sure this object persists between scene loads.
            DontDestroyOnLoad(gameObject);
            once = true;

#if UNITY_EDITOR
            curScene = (int)TestScene;
            LoadScene(Singleton);
#endif
        }
    }


    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
#endif

//#if UNITY_WSA
//        if (once && Room.Instance.done)
//        {
//            curScene = (int)GameScene.mainmenu;
//            SceneManager.LoadScene(MainMenu);
//            once = false;
//        }
//#endif
        if (OrderExecution.Singleton != null && ReStartLevel)
        {
            OrderExecution.LifeGoalReached = true;
            ReStartLevel = false;
        }
        if (SceneManager.GetSceneByName(curScene_Name).isLoaded && preScene_Name != curScene_Name && !UnRenderingTrack)
        {
            if (preScene_Name != MainMenu && preScene_Name != "Application")
                UnRenderingTrack = true;


#if UNITY_EDITOR
            if (once)
            {
                ChangeScene();
                once = false;
            }
#endif
        }
    }

    public void StartGame()
    {
        curScene = (int)GameScene.mainmenu;
        SceneManager.LoadScene(MainMenu);
        once = false;
    }
    

    public static void ChangeScene()
    {
        Detach_RoomChild(Singleton);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(curScene_Name));

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(preScene_Name).buildIndex);

        if (OrderExecution.Singleton != null)
            OrderExecution.LifeGoalReached = true;

        preScene_Name = curScene_Name;
       
    }


    public static void Quit()
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

    public static void LoadScene(AppManager Temp)
    {
        Temp.CheckAndDestoryManagers();

        switch (curScene)
        {
            case (int)GameScene.mainmenu:
                {
                    curScene_Name = Temp.MainMenu;
                    SceneManager.LoadSceneAsync(Temp.MainMenu,LoadSceneMode.Additive);
                    break;
                }
            case (int)GameScene.level_1:
                {
                    curScene_Name = Temp.Level_1;
                    SceneManager.LoadSceneAsync(Temp.Level_1, LoadSceneMode.Additive);
                    break;
                }
            case (int)GameScene.level_2:
                {
                    curScene_Name = Temp.Level_2;
                    SceneManager.LoadSceneAsync(Temp.Level_2, LoadSceneMode.Additive);
                    break;
                }
            case (int)GameScene.level_3:
                {
                    curScene_Name = Temp.Level_3;
                    SceneManager.LoadSceneAsync(Temp.Level_3, LoadSceneMode.Additive);
                    break;
                }
            case (int)GameScene.level_4:
                {
                    curScene_Name = Temp.Level_4;
                    SceneManager.LoadSceneAsync(Temp.Level_4, LoadSceneMode.Additive);
                    break;
                }
            case (int)GameScene.Tutorial:
                {
                    curScene_Name = Temp.Tutorial;
                    SceneManager.LoadSceneAsync(Temp.Tutorial, LoadSceneMode.Additive);
                    break;
                }
            case (int)GameScene.TestLevelCreation:
                {
                    break;
                }
        }
    }

    public static void NextLevel(AppManager Temp)
    {
        switch (curScene)
        {
            case (int)GameScene.level_1:
                {
                    curScene = (int)GameScene.level_2;
                    break;
                }
            case (int)GameScene.level_2:
                {
                    curScene = (int)GameScene.level_3;
                    break;
                }
            case (int)GameScene.level_3:
                {
                    curScene = (int)GameScene.level_4;
                    break;
                }
            case (int)GameScene.level_4:
                {
                    curScene = (int)GameScene.mainmenu;
                    break;
                }
            case (int)GameScene.Tutorial:
                {
                    curScene = (int)GameScene.mainmenu;
                    break;
                }

        }

        LoadScene(Temp);
    }

    public static void Detach_RoomChild(AppManager Temp)
    {
        if(preScene_Name != "Application")
            Destroy(Temp.TheRoom.transform.GetChild(0).gameObject);
    }

    private void CheckAndDestoryManagers()
    {
        if(LevelManager.Singleton != null)
        {
            LevelManager.SelfDestory(LevelManager.Singleton);
        }

        if(MainMenuManager.Singleton != null)
        {
            MainMenuManager.SelfDestory(MainMenuManager.Singleton);
        }
    }
}

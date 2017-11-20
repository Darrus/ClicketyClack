using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour {

    public String GameTitle = "ClicketyClack";

    public String MainMenu_Scene;
    public String Level_1_Scene;
    public String Level_2_Scene;
    public String Level_3_Scene;
    public String Level_4_Scene;
    public String Tutorial_Scene;
    public String TempChange_Scene;

    [HideInInspector]
    public String curScene_Name;
    [HideInInspector]
    public String preScene_Name;

    [System.Serializable]
    public enum GameScene
    {
        mainmenu = 0,
        level_1 = 1,
        level_2 = 2,
        level_3 = 3,
        level_4 = 4,
        Tutorial = 5,
        Customization = 6
    };

    [HideInInspector]
    public GameScene curScene;
    [HideInInspector]
    public bool ReStartLevel;
    [HideInInspector]
    public bool RenderingTrack;
    [HideInInspector]
    public bool UnRenderingTrack;

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

#if UNITY_EDITOR
            curScene = TestScene;
            LoadScene();
#endif
        }
    }


    void Update()
    {
        if (SceneManager.GetSceneByName(curScene_Name).isLoaded && preScene_Name != curScene_Name && !UnRenderingTrack)
        {
                if (preScene_Name != MainMenu_Scene && preScene_Name != "Application" && preScene_Name != TempChange_Scene)
                    UnRenderingTrack = true;

                if (OrderExecution.Singleton != null && OrderExecution.Instance.AllDone && !UnRenderingTrack)
                    ChangeScene();
        }

    }

    public void StartGame()
    {
        curScene = GameScene.mainmenu;
        LoadScene();
    }

    public void ChangeScene()
    {
        Detach_RoomChild();

        OrderExecution.Instance.LifeGoalReached = true;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(curScene_Name));

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(preScene_Name).buildIndex);

        preScene_Name = curScene_Name;

        if (ReStartLevel && preScene_Name == TempChange_Scene)
        {
            ReStartLevel = false;
            LoadScene();
        }
    }


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

    public void LoadScene()
    {
        CheckAndDestoryManagers();

        switch (curScene)
        {
            case GameScene.mainmenu:
                {
                    curScene_Name = MainMenu_Scene;
                    SceneManager.LoadSceneAsync(MainMenu_Scene, LoadSceneMode.Additive);
                    break;
                }
            case GameScene.level_1:
                {
                    curScene_Name = Level_1_Scene;

                    if (ReStartLevel)
                    {
                        curScene_Name = TempChange_Scene;
                        SceneManager.LoadSceneAsync(TempChange_Scene, LoadSceneMode.Additive);
                    }
                    else
                        SceneManager.LoadSceneAsync(Level_1_Scene, LoadSceneMode.Additive);

                    break;
                }
            case GameScene.level_2:
                {
                    curScene_Name = Level_2_Scene;

                    if (ReStartLevel)
                    {
                        curScene_Name = TempChange_Scene;
                        SceneManager.LoadSceneAsync(TempChange_Scene, LoadSceneMode.Additive);
                    }
                    else
                        SceneManager.LoadSceneAsync(Level_2_Scene, LoadSceneMode.Additive);

                    break;
                }
            case GameScene.level_3:
                {
                    curScene_Name = Level_3_Scene;

                    if (ReStartLevel)
                    {
                        curScene_Name = TempChange_Scene;
                        SceneManager.LoadSceneAsync(TempChange_Scene, LoadSceneMode.Additive);
                    }
                    else
                        SceneManager.LoadSceneAsync(Level_3_Scene, LoadSceneMode.Additive);

                    break;
                }
            case GameScene.level_4:
                {
                    curScene_Name =Level_4_Scene;

                    if (ReStartLevel)
                    {
                        curScene_Name = TempChange_Scene;
                        SceneManager.LoadSceneAsync(TempChange_Scene, LoadSceneMode.Additive);
                    }
                    else
                        SceneManager.LoadSceneAsync(Level_4_Scene, LoadSceneMode.Additive);

                    break;
                }
            case GameScene.Tutorial:
                {
                    curScene_Name = Tutorial_Scene;

                    if (ReStartLevel)
                    {
                        curScene_Name = TempChange_Scene;
                        SceneManager.LoadSceneAsync(TempChange_Scene, LoadSceneMode.Additive);
                    }
                    else
                        SceneManager.LoadSceneAsync(Tutorial_Scene, LoadSceneMode.Additive);

                    break;
                }
        }
    }

    public void NextLevel()
    {
        switch (curScene)
        {
            case GameScene.level_1:
                {
                    curScene = GameScene.level_2;
                    break;
                }
            case GameScene.level_2:
                {
                    curScene = GameScene.level_3;
                    break;
                }
            case GameScene.level_3:
                {
                    curScene = GameScene.level_4;
                    break;
                }
            case GameScene.level_4:
                {
                    curScene = GameScene.mainmenu;
                    break;
                }
            case GameScene.Tutorial:
                {
                    curScene = GameScene.mainmenu;
                    break;
                }
        }
        LoadScene();
    }

    public void Detach_RoomChild()
    {
        Destroy(TheRoom.transform.GetChild(0).gameObject);
    }

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

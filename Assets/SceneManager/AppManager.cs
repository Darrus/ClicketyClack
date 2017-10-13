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
    public String ArtScene;

    public enum GameScene
    {
        mainmenu = 0,
        level_1 = 1,
        level_2 = 2,
        level_3 = 3,
        level_4 = 4,
        ArtScene = 5,
        TestLevelCreation = 6

    };

    private static bool once;

    public static int curScene;

    public static AppManager Singleton = null;

    public GameObject TheRoom;

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
            curScene = (int)GameScene.mainmenu;
            LoadScene(this);
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

#if UNITY_WSA
        if (once && Room.Instance.done)
        {
            curScene = (int)GameScene.mainmenu;
            LoadScene(this);
            once = false;
        }
#endif
    }

    public static void Quit()
    {
        Debug.Log("App: Terminating.");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public static void LoadScene(AppManager Temp)
    {
        switch(curScene)
        {
            case (int)GameScene.mainmenu:
                {
                    SceneManager.LoadScene(Temp.MainMenu);
                    break;
                }
            case (int)GameScene.level_1:
                {
                    SceneManager.LoadScene(Temp.Level_1);
                    break;
                }
            case (int)GameScene.level_2:
                {
                    SceneManager.LoadScene(Temp.Level_2);
                    break;
                }
            case (int)GameScene.level_3:
                {
                    SceneManager.LoadScene(Temp.Level_3);
                    break;
                }
            case (int)GameScene.level_4:
                {
                    SceneManager.LoadScene(Temp.Level_4);
                    break;
                }
            case (int)GameScene.ArtScene:
                {
                    SceneManager.LoadScene(Temp.ArtScene);
                    break;
                }
            case (int)GameScene.TestLevelCreation:
                {
                    break;
                }
        }
        Detach_RoomChild(Temp);
    }

    public static void NextLevel(AppManager Temp)
    {
        switch (curScene)
        {
            case (int)GameScene.level_1:
                {
                    curScene = (int)GameScene.level_2;
                    SceneManager.LoadScene(Temp.Level_2);
                    break;
                }
            case (int)GameScene.level_2:
                {
                    curScene = (int)GameScene.level_3;
                    SceneManager.LoadScene(Temp.Level_3);
                    break;
                }
            case (int)GameScene.level_3:
                {
                    curScene = (int)GameScene.level_4;
                    SceneManager.LoadScene(Temp.Level_4);
                    break;
                }
            case (int)GameScene.level_4:
                {
                    curScene = (int)GameScene.mainmenu;
                    SceneManager.LoadScene(Temp.MainMenu);
                    break;
                }
        }
        Detach_RoomChild(Temp);
    }

    public static void Detach_RoomChild(AppManager Temp)
    {
        Temp.TheRoom.transform.DetachChildren();
    }

}

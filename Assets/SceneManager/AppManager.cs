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

    public enum GameScene
    {
        mainmenu = 0,
        level_1 = 1,
        level_2 = 2
    };

    public static int curScene;

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

    }
   
    void Start()
    {
        // Are we in the Current Scene?
        if (SceneManager.GetActiveScene().name == "Application")
        {
            // Make sure this object persists between scene loads.
            DontDestroyOnLoad(gameObject);
            curScene = (int)GameScene.mainmenu;
            LoadScene(this);
        }
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public static void Quit()
    {
        Debug.Log("App: Terminating.");


        UnityEditor.EditorApplication.isPlaying = false;
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
        }
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
                    curScene = (int)GameScene.level_1;
                    SceneManager.LoadScene(Temp.Level_1);
                    break;
                }
        }
    }

}

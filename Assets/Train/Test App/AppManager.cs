using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour {

    public String GameTitle = "ClicketyClack";

    public SceneAsset A_MainMenu;
    public SceneAsset A_Level_1;


    enum GameScene
    {
        mainmenu,
        level_1,
        level_2
    };

    private static int GameState;

    static AppManager Singleton = null;

   
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
            GameState = (int)GameScene.mainmenu;
            ChangeScene();
        }
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameState = (int)GameScene.level_1;
            ChangeScene();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            GameState = (int)GameScene.mainmenu;
            ChangeScene();
        }
    }

    public void Quit()
    {
        Debug.Log("App: Terminating.");


        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void ChangeScene()
    {


        switch (GameState)
        {
            case (int)GameScene.mainmenu:
                {
                    SceneManager.LoadScene(A_MainMenu.name);
                    break;
                }
            case (int)GameScene.level_1:
                {
                    SceneManager.LoadScene(A_Level_1.name);
                    break;
                }
            case (int)GameScene.level_2:
                {

                    break;
                }
        }

    }
}

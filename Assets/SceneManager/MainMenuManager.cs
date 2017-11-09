using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public enum MenuPages
    {
        Base_Menu = 0,
        Level_Selection = 1,
        Customization = 2
    }

    public static int curPage;

    public GameObject MenuPage;
    public GameObject LevelPage;
    //public GameObject CustomizePage;

    public GameObject Room_Items;

    public GameObject AppPrefab;

    public bool TestScene;

    public static MainMenuManager Singleton = null;

    public static MainMenuManager Instance
    {
        get { return Singleton; }
    }

    void Awake()
    {
        Debug.Log("MainMenu: Starting.");

        if (Singleton != null)
        {
            Debug.LogError("Multiple MainMenu Singletons exist!");
            return;
        }
        Singleton = this;

        // Is the controlling App already in existence?
        if (GameObject.Find("App") == null)
        {
            Instantiate(AppPrefab);

            Debug.Log("Creating temporary App");
        }
        Room_Items.SetActive(true);
    }

    public static void Add_Child_ToRoom(MainMenuManager Temp)
    {
        GameObject Room = GameObject.FindGameObjectWithTag("TheRoom");
        //Temp.Room_Items.transform.SetParent(Room.transform);
        Debug.Log("Room Child Added");
    }

    void Start()
    {
        if (!TestScene)
        {
            curPage = (int)MenuPages.Base_Menu;
            MenuPage.SetActive(true);
            Debug.Log("Base Menu On");
        }
        
    }

    void Update()
    {
    }

    public static void UpdateMainMenu(MainMenuManager Temp)
    {
        Temp.MenuPage.SetActive(false);
        Temp.LevelPage.SetActive(false);
        //Temp.CustomizePage.SetActive(false);

        switch (curPage)
        {
            case (int)MenuPages.Base_Menu:
                {
                    Temp.MenuPage.SetActive(true);
                    break;
                }
            case (int)MenuPages.Level_Selection:
                {
                    Temp.LevelPage.SetActive(true);
                    break;
                }
            //case (int)MenuPages.Customization:
            //    {
            //        Temp.CustomizePage.SetActive(true);
            //        break;
            //    }
        }
    }

    public void Button_Start()
    {
        curPage = (int)MenuPages.Level_Selection;
        UpdateMainMenu(Singleton);
    }

    public void Button_Temp()
    {
        AppManager.curScene = (int)AppManager.GameScene.Tutorial;
        AppManager.LoadScene(AppManager.Singleton);

        //MainMenuManager.curPage = (int)MainMenuManager.MenuPages.Customization;
        //MainMenuManager.UpdateMainMenu(MainMenuManager.Singleton);
    }

    public void Button_Quit()
    {
        AppManager.Quit();
    }

    public void Button_Back()
    {
        curPage = (int)MenuPages.Base_Menu;
        UpdateMainMenu(Singleton);
    }

    public void Button_Level_1()
    {
        AppManager.curScene = (int)AppManager.GameScene.level_1;
    }

    public void Button_Level_2()
    {
        AppManager.curScene = (int)AppManager.GameScene.level_2;
    }

    public void Button_Level_3()
    {
        AppManager.curScene = (int)AppManager.GameScene.level_3;
    }

    public void Button_Level_4()
    {
        AppManager.curScene = (int)AppManager.GameScene.level_4;
    }

    public void Button_Load_Level()
    {
        AppManager.LoadScene(AppManager.Singleton);
    }

}
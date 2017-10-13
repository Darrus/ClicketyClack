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
    public GameObject CustomizePage;

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
        GameObject Room = GameObject.Find("TheRoom");
        Room_Items.transform.SetParent(Room.transform);
    }

    void Start()
    {
        if (!TestScene)
        {
            curPage = (int)MenuPages.Base_Menu;
            MenuPage.SetActive(true);
        }
        
    }

    void Update()
    {
    }

    public static void UpdateMainMenu(MainMenuManager Temp)
    {
        Temp.MenuPage.SetActive(false);
        Temp.LevelPage.SetActive(false);
        Temp.CustomizePage.SetActive(false);

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
            case (int)MenuPages.Customization:
                {
                    Temp.CustomizePage.SetActive(true);
                    break;
                }
        }
    }


}
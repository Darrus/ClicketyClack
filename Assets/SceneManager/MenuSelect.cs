using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;


public class MenuSelect : MonoBehaviour ,IInputClickHandler{

    enum UIList
    {
        Start = 1,
        Temp = 99,
        Quit = 0,

        Level_1 = 2,
        Level_2 = 3,
        Level_3 = 4,
        Level_4 = 5,

        New = 6,
        Load = 7,

        back = 8
    }

    public int UI_Type;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        switch(UI_Type)
        {
            case (int)UIList.Start:
                {
                    MainMenuManager.curPage = (int)MainMenuManager.MenuPages.Level_Selection;
                    MainMenuManager.UpdateMainMenu(MainMenuManager.Singleton);
                    break;
                }
            case (int)UIList.Temp:
                {
                    MainMenuManager.curPage = (int)MainMenuManager.MenuPages.Customization;
                    MainMenuManager.UpdateMainMenu(MainMenuManager.Singleton);
                    break;
                }
            case (int)UIList.Quit:
                {
                    AppManager.Quit();
                    break;
                }
            case (int)UIList.Level_1:
                {
                    AppManager.curScene = (int)AppManager.GameScene.level_1;
                    AppManager.LoadScene(AppManager.Singleton);
                    break;
                }
            case (int)UIList.Level_2:
                {
                    AppManager.curScene = (int)AppManager.GameScene.level_2;
                    AppManager.LoadScene(AppManager.Singleton);
                    break;
                }
            case (int)UIList.Level_3:
                {
                    AppManager.curScene = (int)AppManager.GameScene.level_3;
                    AppManager.LoadScene(AppManager.Singleton);
                    break;
                }
            case (int)UIList.Level_4:
                {
                    AppManager.curScene = (int)AppManager.GameScene.level_4;
                    AppManager.LoadScene(AppManager.Singleton);
                    break;
                }
            case (int)UIList.New:
                {

                    break;
                }
            case (int)UIList.Load:
                {
                    AppManager.curScene = (int)AppManager.GameScene.ArtScene;
                    AppManager.LoadScene(AppManager.Singleton);
                    break;
                }
            case (int)UIList.back:
                {
                    MainMenuManager.curPage = (int)MainMenuManager.MenuPages.Base_Menu;
                    MainMenuManager.UpdateMainMenu(MainMenuManager.Singleton);
                    break;
                }
        }
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

    }
}

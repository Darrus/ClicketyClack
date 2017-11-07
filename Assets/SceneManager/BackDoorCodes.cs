using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;


public class BackDoorCodes : MonoBehaviour ,IInputClickHandler{

    public bool Back_To_Menu;
    public bool Pause_Train;
    public bool Play_Tarin;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (Back_To_Menu)
        {
            AppManager.curScene = (int)AppManager.GameScene.mainmenu;
            AppManager.LoadScene(AppManager.Singleton);
        }

        if (Pause_Train)
        {
            LevelManager.Play = false;
        }

        if (Play_Tarin)
        {
            LevelManager.Play = true;
        }
    }
}
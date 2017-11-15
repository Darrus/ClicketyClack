using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;


public class BackMenu : MonoBehaviour ,IInputClickHandler{

    public void OnInputClicked(InputClickedEventData eventData)
    {
        AppManager.curScene = (int)AppManager.GameScene.mainmenu;
        AppManager.LoadScene(AppManager.Singleton);
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

    }
}
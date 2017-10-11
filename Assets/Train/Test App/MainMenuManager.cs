using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MainMenuManager : MonoBehaviour
{
   public GameObject AppPrefab;

    void Awake()
    {
        Debug.Log("MainMenu: Starting.");

        // Is the controlling App already in existence?
        if (GameObject.Find("App") == null)
        {
            Instantiate(AppPrefab);

            Debug.Log("Creating temporary App");
        }
    }

    void Start()
    {
    }

    void Update()
    {
       
    }

   

}
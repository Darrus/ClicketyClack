using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public GameObject Room_Items;

    public GameObject AppPrefab;

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

        OrderExecution.Instance.Done = true;
    }

    public void Add_Child_ToRoom()
    {
        GameObject Room = GameObject.FindGameObjectWithTag("TheRoom");
        Room_Items.transform.SetParent(Room.transform);
        Debug.Log("Room Child Added");
    }

    void Start()
    {

    }

    void Update()
    {
    }

    public void Button_Tutorial()
    {
        AppManager.Instance.curScene = AppManager.GameScene.Tutorial;
        AppManager.Instance.LoadScene();
    }

    public void Button_Level_1()
    {
        AppManager.Instance.curScene = AppManager.GameScene.level_1;
    }

    public void Button_Level_2()
    {
        AppManager.Instance.curScene = AppManager.GameScene.level_2;
    }

    public void Button_Level_3()
    {
        AppManager.Instance.curScene = AppManager.GameScene.level_3;
    }

    public void Button_Level_4()
    {
        AppManager.Instance.curScene = AppManager.GameScene.level_4;
    }

    public void Button_Load_Level()
    {
        AppManager.Instance.LoadScene();
    }

    public void Button_Quit()
    {
        AppManager.Instance.Quit();
    }

    public void SelfDestory()
    {
        Singleton = null;
        GameObject.Destroy(gameObject);
    }

}
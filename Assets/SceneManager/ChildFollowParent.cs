using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildFollowParent : MonoBehaviour
{
    private bool once;

    void Awake()
    {
        once = true;
        Debug.Log("Room Childposition start");
        if (AppManager.Instance.curScene == AppManager.GameScene.mainmenu)
        {
            MainMenuManager.Instance.Add_Child_ToRoom();
        }
        else
        {
            Debug.Log(LevelManager.Singleton);
            LevelManager.Instance.Add_Child_ToRoom();
            Debug.Log("Room Child Test");
        }

        OrderExecution.Instance.Done = true;
    }
    // Use this for initialization
    void Start()
    {
        if (once && transform.parent != null)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.identity;

            Debug.Log("Room Child loaded");

            Debug.Log("Room Child's P :" + transform.position);

            once = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

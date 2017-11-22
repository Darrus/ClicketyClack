/** 
*  @file    ChildFollowParent.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief Contain class ChildFollowParent
*  
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*  @brief A Simply Class for Scene's RoomItem to run once to be align with the World Anchor 
*/
public class ChildFollowParent : MonoBehaviour
{
    private bool once; ///< bool trigger for some codes run once only


    /**
    *  @brief Need to Put GameObject in the Room Parent before other script update they data useing their current transform in update or start
    */
    void Awake()
    {
        once = true;
        if (AppManager.Instance.gameState == AppManager.GameScene.mainmenu)
        {
            MainMenuManager.Instance.Add_Child_ToRoom();
        }
        else
        {
            Debug.Log(LevelManager.Singleton);
            LevelManager.Instance.Add_Child_ToRoom();
        }

        OrderExecution.Instance.Done = true;
    }

    /**
    *  @brief After being put into the Room Parent, this will align the GameObject with the World Anchor 
    */
    void Start()
    {
        if (once && transform.parent != null)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.identity;
            once = false;
        }
    }

}

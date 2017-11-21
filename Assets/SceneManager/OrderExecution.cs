/** 
*  @file    OrderExecution.cs
*  @author  Yin Shuyu (150713R) 
*  
*  @brief Contain Singleton class OrderExecution
*  
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*  @brief Singleton Class for Order Execution Management for controling some scripts run order 
*/
public class OrderExecution : MonoBehaviour {

    public List<GameObject> RunOrder; ///< List of GameObject to run ascending order

    [HideInInspector]
    public bool Done; ///< bool trigger for Scripts when reach a specific point in the script
    [HideInInspector]
    public bool AllDone; ///< bool trigger when all RunOrder's script run once

    private int currOrder; ///< id of script, current in the RunOrder
    [HideInInspector]
    public bool LifeGoalReached; ///< bool trigger when the purpose of OrderExecution is fulfill

    public static OrderExecution Singleton = null; ///< Static Singleton of the OrderExecution

    public static OrderExecution Instance ///< Static Instance function to get all the Data of OrderExecution
    {
        get { return Singleton; }
    }

    /**
   *  @brief At the Awake of the gameobject, need to set the Singleton And Set all Data to default values
   */
    void Awake()
    {
        Debug.Log("Order Execution: Starting.");

        if (Singleton != null)
        {
            Debug.LogError("Multiple Order Execution Singletons exist!");
            return;
        }
        Singleton = this;

        SetAllActive( false);

        AllDone = false;
        Done = true;
        LifeGoalReached = false;
        currOrder = 0;
    }

    /**
    *  @brief Running all RunOrder's script in ascending order once
    */
    void Update () {

        if (Done)
        {
            Done = false;

            if(currOrder != 0)
            {
                RunOrder[currOrder - 1].SetActive(false);
            }

            if (currOrder != RunOrder.Count)
            {
                RunOrder[currOrder].SetActive(true);
            }
            else
            {
                SetAllActive(false);
                AllDone = true;
            }
    
            currOrder++;
        }

        if(LifeGoalReached)
        {
            SetAllActive( true);
            SelfDestory();
        }
	}

    /**
	*   @brief Set all RunOrder's GameObject to Active/inActive
	*  
    *   @param bool T, whether Set All Active/inActive
    *  
	*   @return null
	*/
    public void SetAllActive(bool T)
    {
        for (int i = 0; i < RunOrder.Count; i++)
        {
            if(RunOrder[i].gameObject != null)
                RunOrder[i].SetActive(T);
        }
    }

    /**
	*   @brief to Destroy OrderExecution's Singleton and gameObject
	*  
	*   @return null
	*/
    public void SelfDestory()
    {
        Singleton = null;
        GameObject.Destroy(gameObject);
    }
}

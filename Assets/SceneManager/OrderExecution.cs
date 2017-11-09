using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderExecution : MonoBehaviour {

    public List<GameObject> RunOrder;

    public static bool Done;
    private int currOrder;

    public static bool LifeGoalReached;

    public static OrderExecution Singleton = null;

    public static OrderExecution Instance
    {
        get { return Singleton; }
    }

    // Use this for initialization
    void Awake()
    {
        Debug.Log("Order Execution: Starting.");

        if (Singleton != null)
        {
            Debug.LogError("Multiple Order Execution Singletons exist!");
            return;
        }
        Singleton = this;

        SetAllActive(Singleton, false);

        Done = true;
        LifeGoalReached = false;
        currOrder = 0;
    }
	// Update is called once per frame
	void Update () {

        if (Done)
        {
            Done = false;

            if(currOrder != 0)
            {
                RunOrder[currOrder - 1].SetActive(false);
            }

            if (currOrder != RunOrder.Count)
                RunOrder[currOrder].SetActive(true);
            else
            {
                SetAllActive(Singleton, false);

                if (AppManager.curScene == (int)AppManager.GameScene.mainmenu)
                {
                    LifeGoalReached = true;
                }
            }

            currOrder++;
        }

        if(LifeGoalReached)
        {
            SetAllActive(Singleton, true);
        }
	}

    public static void SetAllActive(OrderExecution singleton, bool T)
    {
        for (int i = 0; i < singleton.RunOrder.Count; i++)
        {
            singleton.RunOrder[i].SetActive(T);
        }
    }

    public static void SelfDestory(OrderExecution singleton)
    {
        GameObject Temp = singleton.gameObject;
        singleton = null;
        GameObject.Destroy(Temp);
    }
}

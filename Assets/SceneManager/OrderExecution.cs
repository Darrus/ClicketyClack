using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderExecution : MonoBehaviour {

    public List<GameObject> RunOrder;

    [HideInInspector]
    public bool Done;
    [HideInInspector]
    public bool AllDone;

    private int currOrder;
    [HideInInspector]
    public bool LifeGoalReached;

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

        SetAllActive( false);

        AllDone = false;
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

    public void SetAllActive(bool T)
    {
        for (int i = 0; i < RunOrder.Count; i++)
        {
            if(RunOrder[i].gameObject != null)
                RunOrder[i].SetActive(T);
        }
    }

    public void SelfDestory()
    {
        Singleton = null;
        GameObject.Destroy(gameObject);
    }
}

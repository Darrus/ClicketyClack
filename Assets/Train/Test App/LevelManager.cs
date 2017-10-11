using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject AppPrefab;

    void Awake()
    {
        Debug.Log("Level: Starting.");

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

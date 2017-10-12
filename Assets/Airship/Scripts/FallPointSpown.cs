﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPointSpown : MonoBehaviour
{
    public GameObject fallpointPrefab;
    public GameObject train;
    public float spownTime;
    private Vector3 FallPos;


    void Update ()
    {
        spownTime -= Time.deltaTime;

        if (spownTime < 0)
        {
            //FallPos = transform.root.position + RandomPosition();
            FallPos = train.transform.position + train.transform.forward;

            GameObject point =Instantiate((GameObject)fallpointPrefab, FallPos, Quaternion.identity);
            spownTime = SpownTime();
        }
    }

    public Vector3 RandomPosition()
    {
        float levelSize = 0.5f;
        return new Vector3(Random.Range(-levelSize, levelSize), train.transform.position.y, Random.Range(-levelSize, levelSize));
    }

    private float SpownTime()
    {
        return Random.Range(3.0f, 5.0f);
    }

}
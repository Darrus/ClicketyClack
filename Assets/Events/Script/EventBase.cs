﻿using UnityEngine;
using UnityEngine.Events;

public class EventBase : MonoBehaviour
{
    bool solved;

    public bool Solved
    {
        get
        {
            return solved;
        }
        set
        {
            solved = value;
            if(solved)
            {
                OnSolved();
            }
        }
    }

    protected virtual void OnSolved()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAreaManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Rocks = null;
    public static bool _isStop = false;
    public bool T = true;

    private void Update()
    {
        if (!T)
        {
            _isStop = false;
            LevelManager.Instance.MoveOut = true;
            ArrowControll.RockComp = true;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        for (int i = 0; i < 7; i++)
        {
            if (col.gameObject == Rocks[i])
            {
                T = true;
                break;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        for (int i = 0; i < 7; i++)
        {
            if (col.gameObject == Rocks[i])
            {
                T = false;
            }
        }
        
    }
}

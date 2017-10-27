using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool _isPause = false;

    public static void Pause()
    {
        if (_isPause)
        {
            Debug.Log("pause");

            Time.timeScale = 0;
        }
    }
    public static void OnStart()
    {
        if (!_isPause)
        {
            Debug.Log("Restart");

            Time.timeScale = 1;
        }
    }
}

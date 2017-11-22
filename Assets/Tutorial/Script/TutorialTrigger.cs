using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{

    private void Awake()
    {
        OrderExecution.Instance.Done = true;
    }

    //private bool _isTrig;
    //private void Start()
    //{
    //    _isTrig = false;
    //}

    //private void Update()
    //{
    //    // 電車が動いたら
    //    if(LevelManager.Singleton != null)
    //        if (LevelManager.Instance.CargoOn)
    //        {
    //            if (!_isTrig)
    //            {
    //            }
    //            _isTrig = true;
    //        }
    //}

}

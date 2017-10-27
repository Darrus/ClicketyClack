using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    //public GameObject HeadlinePrefab;

    private GameObject Train;
    private Vector3 startPos;
    private bool _isTrig;

    private void Start()
    {
        _isTrig = false;
        //Instantiate<GameObject>(HeadlinePrefab, HeadlinePrefab.transform.position, HeadlinePrefab.transform.rotation);
        Train = GameObject.FindGameObjectWithTag("Player");
        startPos = Train.transform.position;
    }

    private void Update()
    {
        // 電車が動いたら
        if (LevelManager.CargoOn)
        {
            if (!_isTrig)
            {
                TextControll.textNum = 2;
            }
            _isTrig = true;
        }
    }

}

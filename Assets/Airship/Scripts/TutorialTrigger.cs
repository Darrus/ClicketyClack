using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject HeadlinePrefab;

    private GameObject train;
    private Vector3 startPos;
    private bool _isTrig;

    private void Start()
    {
        _isTrig = false;
        train = GameObject.FindGameObjectWithTag("Player");
        startPos = train.transform.position;
    }

    private void Update()
    {
        // 電車が動いたら
        if (startPos != train.transform.position)
        {
            if (!_isTrig)
            {
                TextControll.textNum = 1;
            }

            _isTrig = true;
        }
    }

    // 洞窟を通過したら
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            TextControll.textNum = 2;
            Instantiate<GameObject>(HeadlinePrefab, HeadlinePrefab.transform.position, HeadlinePrefab.transform.rotation);
        }
    }
}

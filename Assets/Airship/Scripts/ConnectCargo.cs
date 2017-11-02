using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectCargo : MonoBehaviour
{
    public static bool _isConnect = false;
    public GameObject Connector;

    private Collider col;
    private Rigidbody rigid;
    private GameObject connector;

    private void Start()
    {
        connector = GameObject.FindGameObjectWithTag("Connector");

        col = GetComponent<BoxCollider>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isConnect)
        {
            Destroy(connector);

            LevelManager.CargoOn = true;
            this.GetComponent<TrainMovement2>().enabled = true;

        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Connector")
        {
            _isConnect = true;
            ArrowControll.ConnectComp = true;
        }
    }
    
}
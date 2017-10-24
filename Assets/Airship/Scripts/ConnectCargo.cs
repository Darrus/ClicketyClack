using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectCargo : MonoBehaviour
{
    public GameObject Connector;

    public static bool _isConnect = false;
    private Collider col;
    private Rigidbody rigid;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isConnect)
        {
            this.transform.position = Connector.transform.position;
            rigid.useGravity = false;  
            rigid.isKinematic = true;
            LevelManager.CargoOn = true;

            
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Connector")
        {
            _isConnect = true;
            Debug.Log("Success Connect");

            // 親オブジェクトを登録
            //transform.parent = GameObject.FindGameObjectWithTag("Train").transform;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class CargoCollision : MonoBehaviour
{
    public static bool _isArrow = false;
    public GameObject obj_coal_pile;

    private  bool _isCoal = false;
    private int coalCount;

    private void Start()
    {
        coalCount = 0;
       GetComponent<HandDraggable>().enabled = false;
    }

    private void Update()
    {

        if (_isCoal)
        {
            TextControll.textNum = 1;
            ArrowControll.coalComp = true;
            _isCoal = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "Coal")
        {
            CreateCount._isTri = true;
            Destroy(collision.gameObject);
            CoalInCargo();
        }

        if (collision.gameObject.tag == "DragPoint")
        {
            Debug.Log("Stop");
            Destroy(collision.gameObject);
            
            TextControll.textNum = 3;

            _isArrow = true;
            RockAreaManager._isStop = true;
            LevelManager.MoveOut = false;
        }
    }

    void CoalInCargo()
    {
        coalCount++;

        if (coalCount <= 4)
        {
            obj_coal_pile.transform.position += new Vector3(0, 0.005f, 0);
        }
        else
        {
            _isCoal = true;
            GetComponent<HandDraggable>().enabled = true;
        }
    }
}



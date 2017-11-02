using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControll : MonoBehaviour {


    public float height;

    public GameObject WayPoint_0;
    public GameObject Coal;
    public GameObject RockPos;

    public static bool coalComp = false;
    public static bool cargoComp = false;
    public static bool ConnectComp = false;
    public static bool RockComp = false;

   

    // Initialization
    void Start()
    {
        // CoalPos
        transform.position = new Vector3(Coal.transform.position.x, height, Coal.transform.position.z);
    }

    private void FixedUpdate()
    {

        if (coalComp)
        {
            // TrainPos
            transform.position = new Vector3(WayPoint_0.transform.position.x, height, WayPoint_0.transform.position.z);
            coalComp = false;
        }

        if (ConnectCargo._isConnect)
        {
            // TENKUU
            transform.position = new Vector3(0, 0, 10000);
            ConnectCargo._isConnect = false;
        }

        if (CargoCollision._isArrow)
        {
            // Mount
            transform.position = new Vector3(RockPos.transform.position.x, height, RockPos.transform.position.z);
            CargoCollision._isArrow = false;
        }
        if (RockComp)
        {
            Destroy(this.gameObject);
        }
    }
}

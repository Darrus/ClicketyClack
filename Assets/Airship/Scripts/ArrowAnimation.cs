using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    private float dy = 1.0f;


    [Range(0.1f,1f)]
    public float speed = 1.0f;
    public float distance = 1.0f;

    public GameObject WayPoint_0;
    public GameObject Coal;
    public Vector3 RockPos;

    public static bool coalComp = false;
    public static bool cargoComp = false;
    public static bool ConnectComp = false;
    public static bool RockComp = false;

    // Initialization
    void Start()
    {
        dy = Time.deltaTime * speed;

        // CoalPos
        transform.position = new Vector3(Coal.transform.position.x, -0.355f, Coal.transform.position.z);
    }

    private void FixedUpdate()
    {
        MoveAnimation();

        if (coalComp)
        {
            // TrainPos
            transform.position = new Vector3(WayPoint_0.transform.position.x, -0.3f, WayPoint_0.transform.position.z);
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
            transform.position = new Vector3(RockPos.x, 0.1f, RockPos.z);
            CargoCollision._isArrow = false;
        }
        if (RockComp)
        {
            Destroy(this.gameObject);
        }
    }

    private void ChangePosition(string name)
    {
        GameObject target = GameObject.FindGameObjectWithTag(name);

        transform.position = (target.transform.position - new Vector3(0, 1f, 0));

    }

    void MoveAnimation()
    {
        Vector3 v = transform.position;

        v.y += dy;

        if (v.y > -0.1f)
        {
            v.y = -0.1f;
            dy *= -1;
        }
        if (v.y < -0.33f)
        {
            v.y = -0.33f;
            dy *= -1;
        }

        transform.position = v;
    }

}

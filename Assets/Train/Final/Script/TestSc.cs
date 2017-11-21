/** 
*  @file    TestSc.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief Contain class TestSc (Currently project not in use)
*  
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

/**
*  @brief Class Tested of inserting a empty Waypoint into a Waypoint_List (works)
*/
public class TestSc : MonoBehaviour, IManipulationHandler
{

    private bool onTrack; ///< bool check for whether on track ( true if Collide with 2D track mesh)
    public int id; ///< id of this new empty Waypoint

    private PointManager Manager; ///< PointManager script to get acress to function

    /**
    *  @brief Find PointManager with tag, cant assign as empty Waypoint is a prefab
    */
    private void Start()
    {
        onTrack = false;
        Manager = GameObject.FindGameObjectWithTag("PointManager").GetComponent(typeof(PointManager))as PointManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Point")
        {
            onTrack = true;
            Debug.Log("In");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Point")
        {
            MainPoints Temp = other.gameObject.GetComponent(typeof(MainPoints)) as MainPoints;

            id = Temp.ID + 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Point")
        {
            onTrack = false;

            Debug.Log("Out");
        }
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
    }

    /**
    *  @brief Create new Waypoint if onTrack == true when OnManipulationCompleted
    */
    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        if(onTrack)
        {
            Manager.AddNewPoints(transform.position, id, MainPoints.pointType.NormalPoint);
        }

        BezierCurve2.EnableTrackCollision = false;

        gameObject.SetActive(false);

        gameObject.GetComponent<BoxCollider>().isTrigger = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        BezierCurve2.EnableTrackCollision = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
    }
}

/** 
*  @file    DragPoints.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief Contain class DragPoints (Currently project not in use)
*  
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

/**
*  @brief Class Tested of Draging a Waypoint around and update the 3D Track Mesh at end (works)
*/
public class DragPoints : MonoBehaviour, IManipulationHandler
{
    private float Timer; ///< Timer for Updating the 2D Track Mesh (dont update every frame will lag)
    private int ID; ///< Id of the waypoint
    private bool InZone; ///< bool checking are you in the zone that allow the waypoint to move in

    private Vector3 LastPosition; ///< VEctor3 Last position of the waypoint in zone (prevent going out of zone)

    /**
    *  @brief Get ID from MainPoints.cs which in the same gameobject
    */
    private void Start()
    {
        MainPoints Temp = gameObject.transform.parent.gameObject.GetComponent(typeof(MainPoints)) as MainPoints;
        ID = Temp.ID;
        LastPosition = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
       // string Temp = "Floor_" + ID.ToString();

        if(other.tag == "Floor")
        {
            InZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // string Temp = "Floor_" + ID.ToString();

        if (other.tag == "Floor")
        {
            InZone = false;
        }
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
    }

    /**
    *  @brief Update the 3D Track Mesh
    */
    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        transform.position = LastPosition;
        if (InZone)
        {
            gameObject.transform.parent.gameObject.transform.position = transform.position;
            transform.localPosition = new Vector3(0, 0, 0); 
            Timer = 0f;
            InZone = false;
        }

        BezierCurve2.updateCurvePoints();
        BezierCurve2.updateTrack = true;
        BezierCurve2.Go = false;
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        Timer = 0f;
    }

    /**
    *  @brief Update the 2D Track Mesh when Timer reach a specific time
    */
    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;


        Timer += Time.deltaTime;

        if (Timer >= 0.5f && InZone)
        {
            gameObject.transform.parent.gameObject.transform.position = transform.position;
            transform.localPosition = new Vector3(0, 0, 0);
            BezierCurve2.updateCurvePoints();
            Timer = 0f;
            LastPosition = transform.position;
            InZone = false;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class DragPoints : MonoBehaviour, IManipulationHandler
{
    private float Timer;
    private int ID;
    private bool InZone;

    private Vector3 LastPosition;


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

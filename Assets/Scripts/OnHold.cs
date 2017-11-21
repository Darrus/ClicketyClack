/** 
 *  @file    OnHold.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains the OnClick class
 */
using UnityEngine;
using UnityEngine.Events;
using HoloToolkit.Unity.InputModule;

/** 
 *  @brief   A hololen's class that on click will invoke Unity Events, Supports a debug button in Unity as well
 */
public class OnHold : MonoBehaviour, IManipulationHandler
{
    public UnityEvent eventOnHold;
    public UnityEvent eventOnRelease;

#if UNITY_EDITOR
    public KeyCode holdKey;
    public KeyCode releaseKey;

    public void Update()
    {
        if (Input.GetKeyDown(holdKey))
            eventOnHold.Invoke();
        if (Input.GetKeyDown(releaseKey))
            eventOnRelease.Invoke();
    }
#endif

    /** 
      *  @brief  On manipulation started, invoke event on hold
      *  @param  eventData, an hololen's ManipulationEvent Datafor this function to handle
      */
    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        eventOnHold.Invoke();
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
    }

    /** 
       *  @brief  On manipulation completed, invoke event on release
       *  @param  eventData, an hololen's ManipulationEvent Datafor this function to handle
       */
    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        eventOnRelease.Invoke();
    }

    /** 
      *  @brief  On manipulation canceled, invoke event on release
      *  @param  eventData, an hololen's ManipulationEvent Datafor this function to handle
      */
    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        eventOnRelease.Invoke();
    }
}

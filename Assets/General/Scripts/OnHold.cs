/** 
 *  @file    OnHold.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains the OnHold class
 */
using UnityEngine;
using HoloToolkit.Unity.InputModule;

/** 
 *  @brief   A hololen's class that on click will invoke Unity Events, Supports a debug button in Unity as well
 */
public class OnHold : MonoBehaviour, IManipulationHandler
{
    private float defaultSpeed;

    private void Start()
    {
        defaultSpeed = LevelManager.Instance.TrainManager.MainSpeed;
    }

    /** 
      *  @brief  On manipulation started, invoke event on hold
      *  @param  eventData, an hololen's ManipulationEvent Datafor this function to handle
      */
    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        if(LevelManager.Singleton != null)
        {
            LevelManager.Instance.TrainManager.MainSpeed *= 0.6f;
        }
        
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
        if (LevelManager.Singleton != null)
        {
            LevelManager.Instance.TrainManager.MainSpeed = defaultSpeed;
        }
    }

    /** 
      *  @brief  On manipulation canceled, invoke event on release
      *  @param  eventData, an hololen's ManipulationEvent Datafor this function to handle
      */
    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        if (LevelManager.Singleton != null)
        {
            LevelManager.Instance.TrainManager.MainSpeed = defaultSpeed;
        }
    }
}

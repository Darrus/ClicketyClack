/** 
 *  @file     SlowSoundOnHold.cs
 *  @author Darrus
 *  @date    21/11/2017  
 *  @brief   Contains the slow sound on hold class
 */
using UnityEngine;
using HoloToolkit.Unity.InputModule;

/** 
 *  @brief   Slow sound on hold, using holotool kit manipulation gesture
 */
public class SlowSoundOnHold : MonoBehaviour, IManipulationHandler {
    [Range(-3.0f, 3.0f)]
    public float slowedPitch = 1.0f;

    [SerializeField]
    AudioSource audioSource;

    private void Awake()
    {
        if(!audioSource)
            audioSource = GetComponent<AudioSource>();
    }

    /** 
      *  @brief   Slows done the pitch when the manipulation gesture started
      *  @param  eventData, ManipulationEventData class provided by holotool kit
      */
    public void OnManipulationStarted(ManipulationEventData eventData)
    {
#if DEBUG
        Debug.Log("Sound slowed.");
#endif
        audioSource.pitch = slowedPitch;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
    }

    /** 
      *  @brief   Returns the pitch back to 1.0f when manipulation is completed
      *  @param  eventData, ManipulationEventData class provided by holotool kit
      */
    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
#if DEBUG
        Debug.Log("Sound pitch resumed.");
#endif
        audioSource.pitch = 1.0f;
    }

    /** 
      *  @brief   Returns the pitch back to 1.0f when manipulation is cancelled
      *  @param  eventData, ManipulationEventData class provided by holotool kit
      */
    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
#if DEBUG
        Debug.Log("Sound pitch resumed.");
#endif
        audioSource.pitch = 1.0f;
    }
}

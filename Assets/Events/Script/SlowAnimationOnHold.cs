/** 
 *  @file    SlowAnimationOnHold.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains Slow animation on hold class
 */
using UnityEngine;
using HoloToolkit.Unity.InputModule;

/** 
 *  @brief   Slows the animator animation using Hololen's manipulation gesture
 */
[RequireComponent(typeof(Animator))]
public class SlowAnimationOnHold : MonoBehaviour, IManipulationHandler
{
    [Range(0.0f, 1.0f)]
    public float slowedSpeed = 0.5f;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /** 
     *  @brief   Slows the animator animation when manipulation starts
     */
    public void OnManipulationStarted(ManipulationEventData eventData)
    {
#if DEBUG
        Debug.Log("Animation slowed.");
#endif
        anim.speed = slowedSpeed;
    }
   
    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
    }

    /** 
     *  @brief   When the user has let go of the manipulation, returns the animator to it's original speed
     */
    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
#if DEBUG
        Debug.Log("Animation speed resumed.");
#endif
        anim.speed = 1.0f;
    }

    /** 
     *  @brief   When the user has let go of the manipulation, returns the animator to it's original speed
     */
    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
#if DEBUG
        Debug.Log("Animation speed resumed.");
#endif
        anim.speed = 1.0f;
    }
}

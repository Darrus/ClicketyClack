/** 
 *  @file    TriggerExit.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains the Trigger Exit class
 */
using UnityEngine;
using UnityEngine.Events;

/** 
 *  @brief   Upon trigger exit, invoke Unity Events
 */
[RequireComponent(typeof(Collider))]
public class TriggerExit : MonoBehaviour
{
    [TagSelector]
    public string triggerTag;

    public UnityEvent events;

    /** 
     *  @brief   On trigger exit with the trigger tag, invokes unity events
     *  @param   other, the collider class that this game object has collided with
     */
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(triggerTag))
            return;

        events.Invoke();
    }
}

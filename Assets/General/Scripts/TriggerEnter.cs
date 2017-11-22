/** 
 *  @file    TriggerEnter.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief  Calls UnityEvent upon trigger entering with a given trigger tag.
 */
using UnityEngine;
using UnityEngine.Events;

/** 
 *  @brief  Calls UnityEvent upon trigger entering with a given trigger tag, requires the Collider component.
 */
[RequireComponent(typeof(Collider))]
public class TriggerEnter : MonoBehaviour
{
    [TagSelector]
    public string triggerTag;

    public UnityEvent events;

    /**
     * @brief Invoke unity events upon trigger enter
     * @param other is a class type Collider, it's the othe collider that this object has collided with
     */
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(triggerTag))
            return;

        events.Invoke();
    }
}

/** 
 *  @file    CollisionEnter.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains the collision enter class
 */
using UnityEngine;
using UnityEngine.Events;

/** 
 *  @brief   Given a trigger tag, upon colliding will invoke the UnityEvents
 */
[RequireComponent(typeof(Collider))]
public class CollisionEnter : MonoBehaviour
{
    [TagSelector]
    public string triggerTag;

    public UnityEvent events;

    /** 
     *  @brief   Upon collision enter, invokes unity event
     *  @param   collision, the collision class that this object has collided with
     */
    private void OnCollisionEnter(Collision collision)
    {
        if ( !collision.gameObject.CompareTag(triggerTag))
            return;

        events.Invoke();
    }
}

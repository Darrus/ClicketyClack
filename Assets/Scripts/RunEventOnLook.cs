/** 
 *  @file    RunEventOnLook.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains the RunEventOnLook class
 */
using UnityEngine;
using UnityEngine;
using UnityEngine.Events;

/** 
 *  @brief   Runs Unity Event upon looking at the object using raycast from the main camera position
 */
[RequireComponent(typeof(Collider))]
public class RunEventOnLook : MonoBehaviour
{
    public float lookDistance;
    public UnityEvent onLookEvents;
    public UnityEvent onLookAwayEvents;

    Collider col;
    Camera mainCam;
    bool looked = false;

    private void Awake()
    {
        mainCam = Camera.main;
        col = GetComponent<Collider>();
    }

    /** 
     *  @brief   Checks if camera ray has hit this object at all
     */
    private void Update()
    {
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        RaycastHit hitInfo;
        bool hit = col.Raycast(ray, out hitInfo, lookDistance);

        if (hit && !looked)
        {
            looked = true;
            onLookEvents.Invoke();
        }
        else if(looked && !hit)
        {
            looked = false;
            onLookAwayEvents.Invoke();
        }
    }
}

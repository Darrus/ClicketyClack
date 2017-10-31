using UnityEngine;
using UnityEngine.Events;

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

    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit);

        if(!looked && hit.transform != null && hit.distance <= lookDistance && hit.collider == col)
        {
            looked = true;
            onLookEvents.Invoke();
        }
        else if(looked && (hit.transform == null || hit.collider != col))
        {
            looked = false;
            onLookAwayEvents.Invoke();
        }
    }
}

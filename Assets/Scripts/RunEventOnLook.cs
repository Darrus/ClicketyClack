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

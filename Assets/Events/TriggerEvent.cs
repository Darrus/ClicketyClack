using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerEvent : MonoBehaviour
{
    [TagSelector]
    public string triggerTag;

    public UnityEvent events;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(triggerTag))
            return;

        events.Invoke();
    }
}

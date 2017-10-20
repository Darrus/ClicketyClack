using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerExit : MonoBehaviour
{
    [TagSelector]
    public string triggerTag;

    public UnityEvent events;

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(triggerTag))
            return;

        events.Invoke();
    }
}

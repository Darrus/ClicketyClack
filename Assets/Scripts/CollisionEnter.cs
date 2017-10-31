using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionEnter : MonoBehaviour
{
    [TagSelector]
    public string triggerTag;

    public UnityEvent events;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(triggerTag))
            return;

        events.Invoke();
    }
}

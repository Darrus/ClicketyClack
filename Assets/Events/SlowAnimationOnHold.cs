using UnityEngine;
using HoloToolkit.Unity.InputModule;

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

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
#if DEBUG
        Debug.Log("Animation speed resumed.");
#endif
        anim.speed = 1.0f;
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
#if DEBUG
        Debug.Log("Animation speed resumed.");
#endif
        anim.speed = 1.0f;
    }
}

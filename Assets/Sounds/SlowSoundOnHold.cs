using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class SlowSoundOnHold : MonoBehaviour, IManipulationHandler {
    [Range(-3.0f, 3.0f)]
    public float slowedPitch = 1.0f;

    [SerializeField]
    AudioSource audioSource;

    private void Awake()
    {
        if(!audioSource)
            audioSource = GetComponent<AudioSource>();
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
#if DEBUG
        Debug.Log("Sound slowed.");
#endif
        audioSource.pitch = slowedPitch;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
#if DEBUG
        Debug.Log("Sound pitch resumed.");
#endif
        audioSource.pitch = 1.0f;
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
#if DEBUG
        Debug.Log("Sound pitch resumed.");
#endif
        audioSource.pitch = 1.0f;
    }
}

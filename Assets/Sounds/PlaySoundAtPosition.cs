using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundAtPosition : MonoBehaviour {
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAtPosition()
    {
        transform.SetParent(null);
        audioSource.Play();
    }
}

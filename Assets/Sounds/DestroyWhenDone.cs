using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyWhenDone : MonoBehaviour
{
    AudioSource audioSource;
    bool played;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        played = audioSource.isPlaying;
    }

    void Update ()
    {
        if(!played)
            played = audioSource.isPlaying;

        if (!audioSource.isPlaying && played)
            Destroy(this.gameObject);
	}
}

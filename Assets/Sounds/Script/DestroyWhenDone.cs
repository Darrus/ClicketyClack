/** 
 *  @file     DestroyWhenDone.cs
 *  @author Darrus
 *  @date    21/11/2017  
 *  @brief   Contains the destroy when done class
 */
using UnityEngine;

/** 
 *  @brief   Destroys the object when the sound is done playing
 */
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

    /** 
      *  @brief   Check if sound is done playing
      */
    void Update ()
    {
        if(!played)
            played = audioSource.isPlaying;

        if (!audioSource.isPlaying && played)
            Destroy(this.gameObject);
	}
}

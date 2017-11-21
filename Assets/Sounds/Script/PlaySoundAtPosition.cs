/** 
 *  @file     PlaySoundAtPosition.cs
 *  @author Darrus
 *  @date    21/11/2017  
 *  @brief   Contains the play sound at position class
 */
using UnityEngine;

/** 
 *  @brief   Detach itself from the parent and plays the sound at it's current position
 */
[RequireComponent(typeof(AudioSource))]
public class PlaySoundAtPosition : MonoBehaviour {
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /** 
      *  @brief   Detach from current parent and play sound
      */
    public void PlayAtPosition()
    {
        transform.SetParent(null);
        audioSource.Play();
    }
}

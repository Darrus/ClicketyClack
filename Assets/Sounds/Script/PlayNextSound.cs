/** 
 *  @file     PlayNextSound.cs
 *  @author Darrus
 *  @date    21/11/2017  
 *  @brief   Contains the play next sound class
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 *  @brief   Given a next sound to play, once the current audioSource is done playing, plays the next sound
 */
[RequireComponent(typeof(AudioSource))]
public class PlayNextSound : MonoBehaviour {
    public AudioSource nextSource;
    AudioSource currentSource;
    bool played = false;

    private void Start()
    {
        currentSource = GetComponent<AudioSource>();
        played = currentSource.isPlaying;
    }

    /** 
      *  @brief   check if current source is done playing, if so play the next source
      */
    void Update () {
		if(played && !currentSource.isPlaying && !nextSource.isPlaying)
        {
            nextSource.Play();
        }
	}
}

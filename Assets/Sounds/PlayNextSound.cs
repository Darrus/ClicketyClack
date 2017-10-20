using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	void Update () {
		if(played && !currentSource.isPlaying && !nextSource.isPlaying)
        {
            nextSource.Play();
        }
	}
}

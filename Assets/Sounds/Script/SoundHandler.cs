/** 
 *  @file     SoundHandler.cs
 *  @author Darrus
 *  @date    21/11/2017  
 *  @brief   Contains the sound handler class
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 *  @brief   Manager of all the audio source under the game object that this script is attach to
 */
public class SoundHandler : MonoBehaviour {
    public AudioSource[] sounds;

    [SerializeField]
    private float fadeInDuration;

    /** 
      *  @brief   Get all children components Audio Source
      */
    private void Awake()
    {
        sounds = GetComponentsInChildren<AudioSource>();
    }

    /** 
      *  @brief   Plays the audio source given the index
      *  @param  index, index of the audio source
      */
    public void Play(int index)
    {
        if (index > sounds.Length || index < 0)
            Debug.LogError("Incorrect index provided");

        if (sounds[index].isPlaying)
        {
            Debug.LogWarning("Sound is already playing.");
            return;
        }
        sounds[index].Play();
    }

    /** 
      *  @brief   Plays the audio source given the index
      *  @param  name, name of the audio source to play
      */
    public void Play(string name)
    {
        for(int i = 0; i < sounds.Length; ++i)
        {
            if(name == sounds[i].name)
            {
                if (sounds[i].isPlaying)
                {
                    Debug.LogWarning("Sound is already playing.");
                    return;
                }
                sounds[i].Play();
                return;
            }
        }

        Debug.LogWarning("Unable to find sound \"" + name + "\"");
    }

    /** 
      *  @brief   Play and Fades in the audio with the given index
      *  @param  index, index of the audio source
      */
    public void PlayFadeIn(int index)
    {
        if (index > sounds.Length || index < 0)
            Debug.LogError("Incorrect index provided");

        if (sounds[index].isPlaying)
        {
            Debug.LogWarning("Sound is already playing.");
            return;
        }
        StartCoroutine(FadeInSound(sounds[index]));
    }

    /** 
      *  @brief   Play and Fades in the audio with the given name
      *  @param  name, name of the audio source to play
      */
    public void PlayFadeIn(string name)
    {
        for (int i = 0; i < sounds.Length; ++i)
        {
            if (name == sounds[i].name)
            {
                if (sounds[i].isPlaying)
                {
                    Debug.LogWarning("Sound is already playing.");
                    return;
                }

                StartCoroutine(FadeInSound(sounds[i]));
                return;
            }
        }

        Debug.LogWarning("Unable to find sound \"" + name + "\"");
    }

    /** 
      *  @brief   Fades in the sound given a fade in duration
      *  @param  source, audio source to fade into
      */
    IEnumerator FadeInSound(AudioSource source)
    {
        float sourceVolume = source.volume;
        source.volume = 0.0f;
        source.Play();
        float incrementValue = sourceVolume / fadeInDuration;
        float time = fadeInDuration;
        while(time > 0.0f)
        {
            time -= Time.deltaTime;
            source.volume += incrementValue * Time.deltaTime;
            yield return null;
        }

        source.volume = sourceVolume;
        yield break;
    }

    /** 
      *  @brief   Stop the sound with the given index
      *  @param  index, index of the audio source
      */
    public void Stop(int index)
    {
        if (index > sounds.Length || index < 0)
            Debug.LogError("Incorrect index provided");

        sounds[index].Stop();
    }

    /** 
      *  @brief   Stop the sound with the given name
      *  @param  name, name of the audio source to play
      */
    public void Stop(string name)
    {
        for (int i = 0; i < sounds.Length; ++i)
        {
            if (name == sounds[i].name)
            {
                sounds[i].Stop();
                return;
            }
        }

        Debug.LogWarning("Unable to find sound \"" + name + "\"");
    }

    /** 
      *  @brief   Stop and Fades out the audio with the given index
      *  @param  index, index of the audio source
      */
    public void StopFadeOut(int index)
    {
        if (index > sounds.Length || index < 0)
            Debug.LogError("Incorrect index provided");

        StartCoroutine(FadeOutSound(sounds[index]));
    }

    /** 
      *  @brief   Stop and Fades out the audio with the given name
      *  @param  name, name of the audio source to play
      */
    public void StopFadeOut(string name)
    {
        for (int i = 0; i < sounds.Length; ++i)
        {
            if (name == sounds[i].name)
            {
                StartCoroutine(FadeOutSound(sounds[i]));
                return;
            }
        }

        Debug.LogWarning("Unable to find sound \"" + name + "\"");
    }

    /** 
      *  @brief   Coroutine to fades out the sound
      *  @param  source, audio source to fade into
      */
    IEnumerator FadeOutSound(AudioSource source)
    {
        float sourceVolume = source.volume;
        float decrementValue = sourceVolume / fadeInDuration;
        float time = fadeInDuration;
        while (time > 0.0f)
        {
            time -= Time.deltaTime;
            source.volume -= decrementValue * Time.deltaTime;
            yield return null;
        }

        source.Stop();
        source.volume = sourceVolume;
        yield break;
    }

    /** 
     *  @brief   Pause the sound with the given index
     *  @param  index, index of the audio source
     */
    public void Pause(int index)
    {
        if (index > sounds.Length || index < 0)
            Debug.LogError("Incorrect index provided");

        sounds[index].Pause();
    }

    /** 
    *  @brief   Pause the sound with the given name
    *  @param  name, name of the audio source to play
    */
    public void Pause(string name)
    {
        for (int i = 0; i < sounds.Length; ++i)
        {
            if (name == sounds[i].name)
            {
                sounds[i].Pause();
                return;
            }
        }

        Debug.LogWarning("Unable to find sound \"" + name + "\"");
    }
}

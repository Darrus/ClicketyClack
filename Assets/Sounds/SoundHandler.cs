using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour {
    public AudioSource[] sounds;

    [SerializeField]
    private float fadeInDuration;

    private void Awake()
    {
        sounds = GetComponentsInChildren<AudioSource>();
    }

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

    public void Stop(int index)
    {
        if (index > sounds.Length || index < 0)
            Debug.LogError("Incorrect index provided");

        sounds[index].Stop();
    }

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

    public void StopFadeOut(int index)
    {
        if (index > sounds.Length || index < 0)
            Debug.LogError("Incorrect index provided");

        StartCoroutine(FadeOutSound(sounds[index]));
    }

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

    public void Pause(int index)
    {
        if (index > sounds.Length || index < 0)
            Debug.LogError("Incorrect index provided");

        sounds[index].Pause();
    }

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

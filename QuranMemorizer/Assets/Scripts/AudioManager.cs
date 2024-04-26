using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        
        // foreach (Sound s in musicSounds) {
        //     s.clip = Resources.Load<AudioClip>("Music/" + s.name);
        // }

        // foreach (Sound s in sfxSounds) {
        //     s.clip = Resources.Load<AudioClip>("SFX/" + s.name);
        // }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        musicSource.clip = s.clip;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        sfxSource.PlayOneShot(s.clip);
    }

    public void VolumeDownMusic(bool isTrue)
    {
        if (isTrue) {
            musicSource.volume = 0.03f;
        } else {
            musicSource.volume = 0.7f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM;

    public Sound[] sounds;
    void Start()
    { 
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
 
    }
    public void PlaySound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.source.Play();
        }
    }

    public void ChangeBGM(AudioClip music)
    {
        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}

using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayAudio("Theme");
    }

    public void PlayAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if(s.source == null)
            SetAudioSource(s);
        s.source.Play();
    }

    public void PlayAudio(string name, GameObject audioSource)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (s.source == null)
            SetAudioSource(s, audioSource);
        s.source.Play();
    }

    private void SetAudioSource(Sound s)
    {
        var go = new GameObject(s.name + "_Sound");
        go.transform.SetParent(transform);
        var source = go.AddComponent<AudioSource>();

        if (source == null) return;

        s.source = source;
        SetSound(s);
    }

    private void SetAudioSource(Sound s, GameObject audioSource)
    {
        var source = audioSource.AddComponent<AudioSource>();

        if (source == null) return;

        s.source = source;
        SetSound(s);
    }

    private void SetSound(Sound s)
    {
        s.source.clip = s.ac;
        s.source.pitch = s.pitch;
        s.source.volume = s.volume;
        s.source.loop = s.loop;
    }
}

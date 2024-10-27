using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Audio[] bgms;
    [SerializeField] Audio[] sfxs;

    List<AudioVolumeNamePair> bgmPairs = new List<AudioVolumeNamePair>();
    List<AudioVolumeNamePair> sfxPairs = new List<AudioVolumeNamePair>();

    AudioVolumeNamePair[] bgmPairsArray;
    AudioVolumeNamePair[] sfxPairsArray;

    float masterLevel = 0.75f;
    float bgmLevel = 1f;
    float sfxLevel = 1f;
    float voicesLevel = 1f;

    bool isMuted = false;

    public bool isDestroyOnLoad = true;

    public static AudioManager instance;

    private struct AudioVolumeNamePair
    {
        public AudioSource source;
        public float originalVolume;
        public string name;

        public AudioVolumeNamePair(AudioSource source, float originalVolume, string name)
        {
            this.source = source;
            this.originalVolume = originalVolume;
            this.name = name;
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (!isDestroyOnLoad)
            DontDestroyOnLoad(gameObject);

        foreach (Audio s in bgms)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.originalVolume * bgmLevel * masterLevel;
            s.source.loop = s.loop;

            bgmPairs.Add(new AudioVolumeNamePair(s.source, s.originalVolume, s.name));
        }

        foreach (Audio s in sfxs)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.originalVolume * sfxLevel * masterLevel;
            s.source.loop = s.loop;

            sfxPairs.Add(new AudioVolumeNamePair(s.source, s.originalVolume, s.name));
        }

        bgmPairsArray = bgmPairs.ToArray();
        sfxPairsArray = sfxPairs.ToArray();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute();
        }
    }

    public void PlayBGM(string name)
    {
        StopAllBGM();
        UnmuteAllBGM();
        AudioSource source = Array.Find(bgmPairsArray, pair => pair.name == name).source;
        source.Play();
    }

    public void PlaySFX(string name)
    {
        AudioSource source = Array.Find(sfxPairsArray, pair => pair.name == name).source;
        //source.Play();
        source.PlayOneShot(source.clip);
    }

    public void PlaySFXWithVarPitch(string name, float pitchRange)
    {
        AudioSource source = Array.Find(sfxPairsArray, pair => pair.name == name).source;
        source.pitch = 1f - UnityEngine.Random.Range(-pitchRange, pitchRange);
        //source.Play();
        source.PlayOneShot(source.clip);

    }

    public void StopAllBGM()
    {
        foreach (AudioVolumeNamePair pair in bgmPairs)
        {
            pair.source.Stop();
        }
    }

    public void MuteAllBGM()
    {
        foreach (AudioVolumeNamePair pair in bgmPairs)
        {
            pair.source.mute = true;
        }
    }

    public void UnmuteAllBGM()
    {
        foreach (AudioVolumeNamePair pair in bgmPairs)
        {
            pair.source.mute = false;
        }
    }

    public void StopAllSFX()
    {
        foreach (AudioVolumeNamePair pair in sfxPairs)
        {
            pair.source.Stop();
        }
    }

    public void UpdateBGMVolume(float volume)
    {
        bgmLevel = volume;

        foreach (AudioVolumeNamePair pair in bgmPairs)
        {
            pair.source.volume = pair.originalVolume * bgmLevel * masterLevel;
        }
    }

    public void UpdateSFXVolume(float volume)
    {
        sfxLevel = volume;

        foreach (AudioVolumeNamePair pair in sfxPairs)
        {
            pair.source.volume = pair.originalVolume * sfxLevel * masterLevel;
        }
    }

    public void UpdateMasterVolume(float volume)
    {
        masterLevel = volume;

        UpdateBGMVolume(bgmLevel);
        UpdateSFXVolume(sfxLevel);
    }

    public float GetMasterVolume()
    {
        return masterLevel;
    }

    public float GetBGMVolume()
    {
        return bgmLevel;
    }

    public float GetSFXVolume()
    {
        return sfxLevel;
    }

    public float GetVoicesVolume()
    {
        return voicesLevel;
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (!isMuted)
        {
            UpdateMasterVolume(0.75f);
        }
        else
        {
            UpdateMasterVolume(0);
        }
    }

    public bool GetIsMuted()
    {
        return isMuted;
    }
}

using UnityEngine;

[System.Serializable]
public class Audio
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float originalVolume;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

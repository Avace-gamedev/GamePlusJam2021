using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem instance;
    public static AudioSource musicSource { get => instance._musicSource; }
    public static AudioSource sfxSource { get => instance._sfxSource; }

    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource _sfxSource;

    void Awake()
    {
        instance = this;
    }
}

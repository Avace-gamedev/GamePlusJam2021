using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class CustomClip
    {
        public enum State
        {
            INTRO, LOOP, OUTRO
        }

        [SerializeField] AudioClip[] intro;
        [SerializeField] AudioClip[] loop;
        [SerializeField] AudioClip[] outro;

        public State state { get; private set; } = State.INTRO;
        public bool over { get => state == State.OUTRO && currentIndex >= outro.Length; }
        int currentIndex = -1;

        public AudioClip GetNextClip()
        {
            if (over) return null;

            currentIndex++;
            while (!CheckCurrent() && !over) { }

            return GetCurrent();
        }

        bool CheckCurrent()
        {
            switch (state)
            {
                case State.INTRO:
                    if (currentIndex >= intro.Length)
                    {
                        currentIndex = 0;
                        state = State.LOOP;
                        return false;
                    }
                    break;
                case State.LOOP:
                    if (loop.Length == 0)
                    {
                        currentIndex = 0;
                        state = State.OUTRO;
                        return false;
                    }
                    else if (currentIndex >= loop.Length)
                    {
                        currentIndex = 0;
                        return true;
                    }
                    break;
                case State.OUTRO:
                    if (currentIndex >= outro.Length)
                        return false;
                    break;
            }

            return true;
        }

        AudioClip GetCurrent()
        {
            if (over) return null;
            switch (state)
            {
                case State.INTRO:
                    return intro[currentIndex];
                case State.LOOP:
                    return loop[currentIndex];
                case State.OUTRO:
                    return outro[currentIndex];
                default:
                    return null;
            }
        }
    }

    [Header("General Stuff")]
    [SerializeField] CustomClip music;

    [Header("Delays")]
    [SerializeField] float initialDelay = 1f;
    [SerializeField] float delayBetweenClips = 0f;

    [Header("Fade")]
    [SerializeField] bool fadeOnInit = false;
    [SerializeField] bool fadeBetweenClips = false;
    [SerializeField] float fadeDuration = 1;

    float volume = 0.8f;
    public bool over { get; private set; } = false;
    bool inBetweenClips = false;

    void Start()
    {
        volume = AudioSystem.musicSource.volume;
        AudioSystem.musicSource.loop = false;
        Play(music.GetNextClip(), fadeOnInit, initialDelay);
    }

    void Update()
    {
        if (!over && !AudioSystem.musicSource.isPlaying && !inBetweenClips)
        {
            if (music.over)
                over = true;
            else
            {
                AudioClip nextClip = music.GetNextClip();
                Play(nextClip, fadeBetweenClips, delayBetweenClips);
            }
        }
    }

    public void Play(AudioClip clip, bool fade = true, float delay = 0f)
    {
        AudioSystem.musicSource.clip = clip;
        AudioSystem.musicSource.volume = 0;
        AudioSystem.musicSource.Play();
        if (delay > 0)
            StartCoroutine(DelayedPlay(fade, delay));
        else
            DoPlay(fade);
    }

    void DoPlay(bool fade)
    {
        if (fade)
            StartCoroutine(SetVolume(volume, fadeDuration));
        else
            AudioSystem.musicSource.volume = volume;
    }

    IEnumerator DelayedPlay(bool fade, float delay)
    {
        inBetweenClips = true;
        yield return new WaitForSeconds(delay);
        inBetweenClips = false;
        DoPlay(fade);
    }

    IEnumerator SetVolume(float volume, float duration)
    {
        float initialVolume = AudioSystem.musicSource.volume;
        float elapsed = 0;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            AudioSystem.musicSource.volume = Mathf.Lerp(initialVolume, volume, t);
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        AudioSystem.musicSource.volume = volume;
    }
}

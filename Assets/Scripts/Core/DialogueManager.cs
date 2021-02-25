using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] DialogueSystem dialogueSystem;
    [SerializeField] [Range(0, 5)] float writeDuration = 1f;
    [SerializeField] AudioClip showDialogueSound;
    [SerializeField] AudioClip hideDialogueSound;

    new string name;
    Monologue monologue;
    int currentIndex;
    public static bool over { get => instance._over; }
    public bool _over = true;

    void Awake()
    {
        instance = this;
        dialogueSystem.Hide();
    }

    public static void Load(string _name, Monologue _monologue) { instance._Load(_name, _monologue); }
    void _Load(string _name, Monologue _monologue)
    {
        name = _name;
        monologue = _monologue;
        currentIndex = 0;
    }

    public static void Play() { instance._Play(); }
    void _Play()
    {
        InputManager.OnInteract.AddListener(DialogueNext);
        dialogueSystem.SetName(name);
        if (showDialogueSound)
            AudioSystem.sfxSource.PlayOneShot(showDialogueSound);
        _over = false;
        dialogueSystem.Show();
        DialogueNext(true);
    }

    void DialogueNext(bool b)
    {
        if (over) return;

        if (b)
        {
            if (currentIndex >= monologue.text.Length)
            {
                if (showDialogueSound)
                    AudioSystem.sfxSource.PlayOneShot(hideDialogueSound);
                dialogueSystem.Hide();
                _over = true;
                InputManager.OnInteract.RemoveListener(DialogueNext);
            }
            else
            {
                dialogueSystem.Write(_Pop(), writeDuration);
            }
        }
    }

    public static string Pop(string[] _monologue) { return instance._Pop(); }
    string _Pop()
    {
        if (over) return null;

        string next = monologue.text[currentIndex];
        currentIndex++;
        return next;
    }
}

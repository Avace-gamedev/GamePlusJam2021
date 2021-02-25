using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAgent : MonoBehaviour
{
    [SerializeField] new string name;
    [SerializeField] Monologue monologue;

    public void Play()
    {
        if (!DialogueManager.over) return;

        DialogueManager.Load(name, monologue);
        DialogueManager.Play();
    }
}

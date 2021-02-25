using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] GameObject bubble;
    [SerializeField] TextMeshProUGUI nameField;
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] AudioClip textSound;

    public void Show()
    {
        bubble.SetActive(true);
    }

    public void SetName(string name)
    {
        nameField.SetText(name);
    }

    public void Write(string dialogue, float duration = 0)
    {
        if (duration <= 0)
            textField.SetText(dialogue);
        else
        {
            StopAllCoroutines();
            StartCoroutine(DoWrite(dialogue, duration));
        }
    }

    IEnumerator DoWrite(string dialogue, float duration)
    {
        string curText = "";
        float elapsed = 0;
        int index = 0;

        textField.SetText("");

        while (index < dialogue.Length)
        {
            yield return new WaitForFixedUpdate();
            elapsed += Time.fixedDeltaTime;
            float percent = elapsed / duration;
            int currentN = (int)Mathf.Floor(percent * dialogue.Length);

            if (currentN > index)
            {
                for (int i = index; i < currentN && i < dialogue.Length; i++)
                    curText += dialogue[i];
                index = currentN;

                textField.SetText(curText);
                if (textSound)
                    AudioSystem.sfxSource.PlayOneShot(textSound);
            }
        }
        textField.SetText(dialogue);
    }

    public void Hide()
    {
        bubble.SetActive(false);
    }
}

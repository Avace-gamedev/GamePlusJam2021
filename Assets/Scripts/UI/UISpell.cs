using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISpell : MonoBehaviour
{
    [SerializeField] Image overlay;
    [SerializeField] TextMeshProUGUI nameField;
    [SerializeField] TextMeshProUGUI typeField;
    [SerializeField] TextMeshProUGUI keyField;
    [SerializeField] Image gamepadImage;
    [SerializeField] Image spellTypeIcon;

    void Awake()
    {
        overlay.gameObject.SetActive(true);
        SetOverlayFill(0);
        spellTypeIcon.gameObject.SetActive(false);
        typeField.gameObject.SetActive(false);
    }

    public void Load(Spell spell, string keyboardKey, Sprite sprite)
    {
        nameField.SetText(spell.name);
        typeField.SetText(Spell.StringOfType(spell.type));
        typeField.gameObject.SetActive(true);
        keyField.SetText(keyboardKey);
        gamepadImage.sprite = sprite;
    }

    public void Load(Spell spell, string keyboardKey, Sprite sprite, Sprite typeIcon)
    {
        Load(spell, keyboardKey, sprite);
        spellTypeIcon.sprite = typeIcon;
        spellTypeIcon.gameObject.SetActive(true);
        typeField.gameObject.SetActive(false);
    }

    void SetOverlayFill(float fill)
    {
        overlay.fillAmount = fill;
    }

    public void StartCooldown(float duration)
    {
        StartCoroutine(Cooldown(duration));
    }

    IEnumerator Cooldown(float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            SetOverlayFill(1 - elapsed / duration);
            yield return null;
            elapsed += Time.deltaTime;
        }

        SetOverlayFill(0);
    }

    public void Enable()
    {
        overlay.fillAmount = 0;
    }

    public void Disable()
    {
        overlay.fillAmount = 1;
    }
}

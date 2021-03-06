﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    [SerializeField] Spell[] spells;
    [SerializeField] Animator animator;
    [SerializeField] MovementSystem movementSystem;
    [SerializeField] Collider2D[] perOrientationCollider;
    [SerializeField] Transform trSpell;
    [SerializeField] UISpell[] uiSpells;
    [SerializeField] string[] spellKeys;
    [SerializeField] Sprite[] spellGamepadImages;
    [SerializeField] Sprite[] spellTypeIcons;

    float[] firerateTimer;
    bool[] trigger;
    int casting = -1;
    bool waitingForAnimation = false;

    void Awake()
    {
        trigger = new bool[spells.Length];
        firerateTimer = new float[spells.Length];

        for (int i = 0; i < spells.Length; i++)
        {
            trigger[i] = false;
            firerateTimer[i] = -1;
            spells[i].Setup(this);
            if (i < uiSpells.Length && i < spellKeys.Length && i < spellGamepadImages.Length)
                if ((int)spells[i].type < spellTypeIcons.Length)
                    uiSpells[i].Load(spells[i], spellKeys[i], spellGamepadImages[i], spellTypeIcons[(int)spells[i].type]);
                else
                    uiSpells[i].Load(spells[i], spellKeys[i], spellGamepadImages[i]);
        }

        for (int i = spells.Length; i < uiSpells.Length; i++)
            uiSpells[i].gameObject.SetActive(false);
    }

    void Start()
    {
        InputManager.OnSpell1.AddListener((b) => OnSpell(0, b));
        InputManager.OnSpell2.AddListener((b) => OnSpell(1, b));
        InputManager.OnSpell3.AddListener((b) => OnSpell(2, b));
        InputManager.OnSpell4.AddListener((b) => OnSpell(3, b));
    }

    void Update()
    {
        for (int i = 0; i < spells.Length; i++)
            if (firerateTimer[i] >= 0)
                firerateTimer[i] -= Time.deltaTime;

        if (casting >= 0)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
            {
                movementSystem.EnableMovements();

                for (int i = 0; i < uiSpells.Length; i++)
                    if (i != casting)
                        uiSpells[i].Enable();

                spells[casting].OnCastEnd();
                casting = -1;
            }
            else
            {
                for (int j = 0; j < uiSpells.Length; j++)
                    if (j != casting) // disable spells until end of cast
                        uiSpells[j].Disable();
                return;
            }
        }

        for (int i = 0; i < spells.Length; i++)
        {
            if (firerateTimer[i] < 0 && trigger[i])
            {
                Cast(i);

                // cancel other triggers
                for (int j = i + 1; j < spells.Length; j++)
                    trigger[j] = false;

                return;
            }
        }
    }

    void Cast(int i)
    {
        spells[i].OnCast();

        if (spells[i].castSound)
            AudioSystem.sfxSource.PlayOneShot(spells[i].castSound);

        animator.SetTrigger(spells[i].animatorTrigger);
        firerateTimer[i] = 1 / spells[i].firerate;
        trigger[i] = false;
        if (i < uiSpells.Length)
            uiSpells[i].StartCooldown(firerateTimer[i]);
        movementSystem.DisableMovements();
        casting = i;
        waitingForAnimation = true;
    }

    public void Hit(int orientation)
    {
        if (!waitingForAnimation) return;

        if (casting < 0)
        {
            Debug.LogWarning("Hit called while casting < 0");
            return;
        }

        if (spells[casting].hitSound)
            AudioSystem.sfxSource.PlayOneShot(spells[casting].hitSound);

        spells[casting].Hit(trSpell.position, movementSystem.DirectionOfOrientation(orientation), perOrientationCollider[orientation]);

        waitingForAnimation = false;
    }

    void OnSpell(int i, bool b)
    {
        if (i < 0 || i >= spells.Length) return;

        trigger[i] = trigger[i] || (b && firerateTimer[i] < 0);
    }
}

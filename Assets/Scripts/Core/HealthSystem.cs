using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [System.Serializable] public class CustomEvent : UnityEvent<float> { }

    [Range(1, 50)]
    [SerializeField] float _maxHealth = 5;
    public float maxHealth { get => _maxHealth; private set => _maxHealth = value; }

    [Header("Sounds")]
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip healSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip fullHealthSound;

    [Header("Callbacks")]
    public CustomEvent OnHealthChange = new CustomEvent();
    public UnityEvent OnFullHealth = new UnityEvent();
    public UnityEvent OnDeath = new UnityEvent();

    public float health { get; private set; }
    float tempDamageMultiplier = -1;

    void Awake()
    {
        health = maxHealth;
    }

    void ChangeHealth(float damage)
    {
        float actualDamage = damage * tempDamageMultiplier;
        if (actualDamage == 0) return;

        health = Mathf.Min(Mathf.Max(0, health - actualDamage), maxHealth);

        OnHealthChange.Invoke(health);

        if (actualDamage > 0)
        {
            // hit
            if (hitSound)
                AudioSystem.sfxSource.PlayOneShot(hitSound);
        }
        else
        {
            // heal
            if (healSound)
                AudioSystem.sfxSource.PlayOneShot(healSound);
        }

        if (health <= 0)
        {
            OnDeath.Invoke();
            if (deathSound)
                AudioSystem.sfxSource.PlayOneShot(deathSound);
        }
        else if (health >= maxHealth)
        {
            OnFullHealth.Invoke();
            if (fullHealthSound)
                AudioSystem.sfxSource.PlayOneShot(fullHealthSound);
        }
    }

    public void Heal(float heal)
    {
        if (heal < 0)
            Debug.LogWarning("Healing by a negative amount, ignored");
        else
            ChangeHealth(-heal);
    }

    public void Hit(float damage)
    {
        if (damage < 0)
            Debug.LogWarning("Hitting with a negative damage, ignored");
        else
            ChangeHealth(damage);
    }

    public void SetTemporaryDamageMultiplier(float multiplier)
    {
        tempDamageMultiplier = multiplier;
    }

    public void CancelTemporaryDamageMultiplier()
    {
        if (tempDamageMultiplier < 0) return;

        tempDamageMultiplier = -1;
    }
}

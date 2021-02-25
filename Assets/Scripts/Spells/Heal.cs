using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Spell/Heal")]
public class Heal : Spell
{
    [Header("Heal stats")]
    [SerializeField] float healAmount;

    HealthSystem healthSystem;

    public override void Setup(SpellSystem spellSystem)
    {
        healthSystem = spellSystem.transform.GetComponent<HealthSystem>();
    }

    public override void Hit(Vector3 position, Vector3 direction, Collider2D collider)
    {
        healthSystem.Heal(healAmount);
    }
}

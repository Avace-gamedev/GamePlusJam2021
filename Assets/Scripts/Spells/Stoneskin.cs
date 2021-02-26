using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stoneskin", menuName = "Spell/Stoneskin")]
public class Stoneskin : Spell
{
    HealthSystem healthSystem;

    public override void Setup(SpellSystem spellSystem)
    {
        healthSystem = spellSystem.transform.GetComponent<HealthSystem>();
    }

    public override void Hit(Vector3 position, Vector3 direction, Collider2D collider)
    {
        healthSystem.SetTemporaryDamageMultiplier(0);
    }

    public override void OnCastEnd()
    {
        healthSystem.CancelTemporaryDamageMultiplier();
    }
}

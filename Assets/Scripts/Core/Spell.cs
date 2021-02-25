using UnityEngine;


public class Spell : ScriptableObject
{
    public string animatorTrigger;
    public float firerate;

    [Header("Sounds")]
    public AudioClip castSound;
    public AudioClip hitSound;

    protected SpellSystem spellSystem;

    public virtual void Setup(SpellSystem spellSystem)
    {
        this.spellSystem = spellSystem;
    }
    public virtual void Hit(Vector3 position, Vector3 direction, Collider2D collider) { }
}
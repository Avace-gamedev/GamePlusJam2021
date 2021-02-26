using UnityEngine;

public enum SpellType : int
{
    MELEE = 0,
    RANGED = 1,
    SHIELD = 2,
    HEAL = 3,
}

public abstract class Spell : ScriptableObject
{
    new public string name;
    public SpellType type;

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

    public virtual void OnCast() { }
    public virtual void Hit(Vector3 position, Vector3 direction, Collider2D collider) { }
    public virtual void OnCastEnd() { }


    public static string StringOfType(SpellType type)
    {
        switch (type)
        {
            case SpellType.MELEE: return "MELEE";
            case SpellType.RANGED: return "RANGED";
            case SpellType.HEAL: return "HEAL";
            case SpellType.SHIELD: return "SHIELD";
            default: return "";
        }
    }
}
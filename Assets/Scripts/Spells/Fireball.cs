using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Spell/Fireball")]
public class Fireball : Spell
{
    [SerializeField] Transform pfBullet;
    [SerializeField] LayerMask targetLayer;

    override public void Hit(Vector3 position, Vector3 direction, Collider2D collider)
    {
        Transform bullet = Instantiate(pfBullet, position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Shoot(direction, targetLayer);
    }
}
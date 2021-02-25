using UnityEngine;

[CreateAssetMenu(fileName = "Gust", menuName = "Spell/Gust")]
public class Gust : Spell
{
    [SerializeField] int maxTargets;
    [SerializeField] LayerMask targetLayer;

    override public void Hit(Vector3 position, Vector3 direction, Collider2D collider)
    {
        Collider2D[] enemies = new Collider2D[maxTargets];
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(targetLayer);

        int n = collider.OverlapCollider(filter, enemies);
        for (int i = 0; i < n; i++)
        {
            Debug.Log("hit " + enemies[i]);
        }
    }
}
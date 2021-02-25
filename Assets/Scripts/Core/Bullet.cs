using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float bulletSpeed = 100;
    [SerializeField] float lifespan = 5f;
    [SerializeField] LayerMask obstacleLayer;

    LayerMask targetLayer;

    public void Shoot(Vector3 direction, LayerMask targetLayer)
    {
        this.targetLayer = targetLayer;
        Vector3 dirNormalized = direction.normalized;
        float angle = Mathf.Atan2(dirNormalized.y, dirNormalized.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        rb.velocity = dirNormalized * bulletSpeed;

        StartCoroutine(DestroySelfIn(lifespan));
    }

    IEnumerator DestroySelfIn(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (targetLayer == (targetLayer | (1 << collider.gameObject.layer)))
            Debug.Log("hit");

        if (obstacleLayer == (obstacleLayer | (1 << collider.gameObject.layer)))
            Destroy(gameObject);
    }
}
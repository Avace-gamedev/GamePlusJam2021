using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSystem : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 5)] float firerate = 1f;
    [SerializeField] Transform pfBullet;
    [SerializeField] Animator animator;
    [SerializeField] MovementSystem movementSystem;

    float firerateTimer = -1;
    bool doRanged = false;
    bool waitingForAnimation = false;
    bool movementDisabled = false;

    void Start()
    {
        InputManager.OnRanged.AddListener(OnRanged);
    }

    void FixedUpdate()
    {
        if (movementDisabled && (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Running")))
        {
            movementSystem.EnableMovements();
            movementDisabled = false;
        }

        if (firerateTimer > 0)
        {
            firerateTimer -= Time.fixedDeltaTime;
        }
        else
        {
            if (doRanged)
            {
                waitingForAnimation = true;
                animator.SetTrigger("ranged");
                firerateTimer = 1 / firerate;
                doRanged = false;
                movementSystem.DisableMovements();
                movementDisabled = true;
            }
        }
    }

    public void SpawnBullet(Vector3 position, Vector3 direction)
    {
        if (!waitingForAnimation) return;

        Transform bullet = Instantiate(pfBullet, position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Shoot(direction);

        waitingForAnimation = false;
    }

    void OnRanged(bool b)
    {
        doRanged = doRanged || (b && firerateTimer < 0);
    }
}

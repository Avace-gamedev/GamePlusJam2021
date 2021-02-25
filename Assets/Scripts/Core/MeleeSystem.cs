using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] MovementSystem movementSystem;
    [SerializeField] Collider2D[] perOrientationCollider;
    [SerializeField] LayerMask targetLayer;

    [Header("Weapon")]
    [SerializeField] [Range(0.1f, 5)] float firerate = 1f;
    [SerializeField] [Range(1, 50)] int maxTargets = 10;

    float firerateTimer = -1;
    bool doMelee = false;
    bool movementDisabled = false;
    bool waitingForAnimation = false;

    void Start()
    {
        InputManager.OnMelee.AddListener(OnMelee);
    }

    void FixedUpdate()
    {
        if (movementDisabled && (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Running")))
        {
            movementSystem.EnableMovements();
            movementDisabled = false;
        }

        if (firerateTimer > 0)
            firerateTimer -= Time.fixedDeltaTime;
        else
        {
            if (doMelee)
            {
                animator.SetTrigger("melee");
                firerateTimer = 1 / firerate;
                doMelee = false;
                movementSystem.DisableMovements();
                movementDisabled = true;
                waitingForAnimation = true;
            }
        }
    }

    public void Hit(Vector3 position, Vector3 direction)
    {
        if (!waitingForAnimation) return;

        Collider2D collider = perOrientationCollider[movementSystem.orientation];

        Collider2D[] enemies = new Collider2D[maxTargets];
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(targetLayer);

        int n = collider.OverlapCollider(filter, enemies);
        for (int i = 0; i < n; i++)
        {
            Debug.Log("hit " + enemies[i]);
        }

        waitingForAnimation = false;
    }

    void OnMelee(bool b)
    {
        doMelee = doMelee || (b && firerateTimer < 0);
    }
}

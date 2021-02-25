using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 5)] float firerate = 1f;
    [SerializeField] Animator animator;
    [SerializeField] MovementSystem movementSystem;

    float firerateTimer = -1;
    bool doMelee = false;
    bool movementDisabled = false;

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
            }
        }
    }

    void OnMelee(bool b)
    {
        doMelee = doMelee || (b && firerateTimer < 0);
    }
}

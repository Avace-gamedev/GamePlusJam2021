using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementSystem : MonoBehaviour
{
    [SerializeField] int nOrientations = 4;

    [Header("Movement")]
    [Range(0, 20)]
    [SerializeField] float speed = 1;
    [SerializeField] bool lockMovementToOrientation = false;

    [Header("Animations")]
    [SerializeField] Animator animator;

    [Header("Sounds")]
    [SerializeField] AudioClip footstepSound;
    [SerializeField] float footstepPeriod = 1f;

    Rigidbody2D rb;
    float initialDrag;
    bool moving;
    Vector2 moveDir;
    int orientation;
    bool movementsEnabled = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialDrag = rb.drag;
    }

    void Start()
    {
        InputManager.OnMove.AddListener(OnMove);
        InputManager.OnMove.AddListener(OnLook);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    public void DisableMovements()
    {
        movementsEnabled = false;
        SetVelocity(Vector3.zero);
        moving = false;
        StopAllCoroutines();
        animator.SetBool("running", false);
    }

    public void EnableMovements()
    {
        movementsEnabled = true;
    }

    void FixedUpdate()
    {
        if (moving && movementsEnabled)
        {
            Vector2 dir = lockMovementToOrientation ? GetDirection(orientation) : moveDir;
            SetVelocity(dir * speed * 50 * Time.fixedDeltaTime);
        }
        else
            SetVelocity(Vector3.zero);
    }

    Vector2 GetDirection(int orientation)
    {
        float angle = (float)orientation / (float)nOrientations * 2 * Mathf.PI;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    IEnumerator PlayFootstepSound()
    {
        if (!footstepSound) yield break;
        float deadline = Time.time;

        while (moving)
        {
            if (Time.time >= deadline)
            {
                AudioSystem.sfxSource.PlayOneShot(footstepSound);
                deadline += footstepPeriod;
            }
            yield return null;
        }
    }

    public void OnMove(Vector2 move)
    {
        if (!movementsEnabled) return;

        moveDir = move;

        if (animator)
            if (move.magnitude > 0)
            {
                moving = true;
                animator.SetBool("running", true);
                if (footstepSound)
                {
                    StopAllCoroutines();
                    StartCoroutine(PlayFootstepSound());
                }
            }
            else
            {
                moving = false;
                animator.SetBool("running", false);
                StopAllCoroutines();
            }
    }

    public void OnLook(Vector2 look)
    {
        if (!movementsEnabled) return;

        if (look.magnitude == 0) return;

        float angle = Mathf.Atan2(look.y, look.x);
        angle = angle < 0 ? angle + 2 * Mathf.PI : angle;

        if (angle <= Mathf.PI / nOrientations || angle > 2 * Mathf.PI - Mathf.PI / nOrientations)
            SetOrientation(0);
        else
            SetOrientation((int)Mathf.Ceil(0.5f * (angle * nOrientations / Mathf.PI - 1)));


    }

    void SetOrientation(int _orientation)
    {
        orientation = _orientation;

        if (animator)
            animator.SetFloat("orientation", orientation);
    }
}

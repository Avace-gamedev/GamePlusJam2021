using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementSystem : MonoBehaviour
{
    [Range(0, 20)]
    [SerializeField] float speed = 1;
    [SerializeField] int nOrientations = 4;

    //TMP
    [SerializeField] Transform visual;

    Rigidbody2D rb;
    Vector2 movement;
    int orientation;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * speed * 1000 * Time.fixedDeltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        movement = move;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>();
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
        visual.localEulerAngles = new Vector3(0, 0, orientation * 360 / nOrientations);
    }
}

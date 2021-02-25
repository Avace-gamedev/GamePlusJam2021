using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [System.Serializable] public class Vector2Event : UnityEvent<Vector2> { }
    [System.Serializable] public class BoolEvent : UnityEvent<bool> { }

    public static InputManager instance;
    [SerializeField] PlayerInput _playerInput;
    public static PlayerInput playerInput { get => instance._playerInput; }

    [SerializeField] Vector2Event _OnMove = new Vector2Event();
    public static Vector2Event OnMove { get => instance._OnMove; set => instance._OnMove = value; }

    [SerializeField] BoolEvent _OnInteract = new BoolEvent();
    public static BoolEvent OnInteract { get => instance._OnInteract; set => instance._OnInteract = value; }

    [SerializeField] BoolEvent _OnMelee = new BoolEvent();
    public static BoolEvent OnMelee { get => instance._OnMelee; set => instance._OnMelee = value; }

    [SerializeField] BoolEvent _OnRanged = new BoolEvent();
    public static BoolEvent OnRanged { get => instance._OnRanged; set => instance._OnRanged = value; }

    void Awake()
    {
        instance = this;
    }

    public void BroadcastOnMove(InputAction.CallbackContext context)
    {
        Vector2 v = context.ReadValue<Vector2>();
        OnMove.Invoke(v);
    }

    public void BroadcastOnInteract(InputAction.CallbackContext context)
    {
        bool b = context.ReadValue<float>() > 0;
        OnInteract.Invoke(b);
    }

    public void BroadcastOnMelee(InputAction.CallbackContext context)
    {
        bool b = context.ReadValue<float>() > 0;
        OnMelee.Invoke(b);
    }

    public void BroadcastOnRanged(InputAction.CallbackContext context)
    {
        bool b = context.ReadValue<float>() > 0;
        OnRanged.Invoke(b);
    }
}

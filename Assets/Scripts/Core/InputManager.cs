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

    [SerializeField] BoolEvent _OnSpell1 = new BoolEvent();
    public static BoolEvent OnSpell1 { get => instance._OnSpell1; set => instance._OnSpell1 = value; }

    [SerializeField] BoolEvent _OnSpell2 = new BoolEvent();
    public static BoolEvent OnSpell2 { get => instance._OnSpell2; set => instance._OnSpell2 = value; }

    [SerializeField] BoolEvent _OnSpell3 = new BoolEvent();
    public static BoolEvent OnSpell3 { get => instance._OnSpell3; set => instance._OnSpell3 = value; }

    [SerializeField] BoolEvent _OnSpell4 = new BoolEvent();
    public static BoolEvent OnSpell4 { get => instance._OnSpell4; set => instance._OnSpell4 = value; }

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

    public void BroadcastOnSpell1(InputAction.CallbackContext context)
    {
        bool b = context.ReadValue<float>() > 0;
        OnSpell1.Invoke(b);
    }

    public void BroadcastOnSpell2(InputAction.CallbackContext context)
    {
        bool b = context.ReadValue<float>() > 0;
        OnSpell2.Invoke(b);
    }

    public void BroadcastOnSpell3(InputAction.CallbackContext context)
    {
        bool b = context.ReadValue<float>() > 0;
        OnSpell3.Invoke(b);
    }

    public void BroadcastOnSpell4(InputAction.CallbackContext context)
    {
        bool b = context.ReadValue<float>() > 0;
        OnSpell4.Invoke(b);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UsefulCallbacks : MonoBehaviour
{
    [HideInInspector] Vector3 orientation;
    [System.Serializable] public class CustomEvent : UnityEvent<Vector3, Vector3> { }

    public CustomEvent OnDisableCallback = new CustomEvent();
    public CustomEvent OnEnableCallback = new CustomEvent();

    void OnDisable()
    {
        OnDisableCallback.Invoke(transform.position, orientation);
    }

    void OnEnable()
    {
        OnEnableCallback.Invoke(transform.position, orientation);
    }
}

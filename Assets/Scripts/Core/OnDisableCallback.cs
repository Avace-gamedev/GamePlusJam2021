using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDisableCallback : MonoBehaviour
{
    public Vector3 orientation;
    [System.Serializable] public class CustomEvent : UnityEvent<Vector3, Vector3> { }

    public CustomEvent callback = new CustomEvent();

    void OnDisable()
    {
        callback.Invoke(transform.position, orientation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationCallbacks : MonoBehaviour
{
    [HideInInspector] public Vector3 orientation;
    [System.Serializable] public class IntEvent : UnityEvent<int> { }

    public IntEvent intCallback = new IntEvent();

    public void IntCallback(int i)
    {
        intCallback.Invoke(i);
    }
}

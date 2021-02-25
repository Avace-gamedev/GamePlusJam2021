using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class AnimateDoor : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] UnityEvent OnOpened = new UnityEvent();
    [SerializeField] UnityEvent OnClosed = new UnityEvent();

    public void Open()
    {
        animator.SetTrigger("open");
        StartCoroutine(OnReachState("DoorOpened", OnOpened));
    }

    IEnumerator OnReachState(string name, UnityEvent OnComplete)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(name))
            yield return null;

        OnComplete.Invoke();
    }

    public void Close()
    {
        animator.SetTrigger("close");
        StartCoroutine(OnReachState("DoorClosed", OnClosed));
    }
}

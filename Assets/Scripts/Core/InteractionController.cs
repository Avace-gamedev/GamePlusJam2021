using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionController : MonoBehaviour
{
    [SerializeField] GameObject commandBubble;
    [SerializeField] bool hideBubbleOnInteract = true;
    [SerializeField] UnityEvent OnInteractCallback = new UnityEvent();

    void Start()
    {
        OutOfRange();
    }

    public void InRange()
    {
        commandBubble.SetActive(true);
        InputManager.OnInteract.AddListener(OnInteract);
    }

    void OnInteract(bool b)
    {
        if (b)
        {
            OnInteractCallback.Invoke();
            if (hideBubbleOnInteract)
                commandBubble.SetActive(false);
        }
    }

    public void OutOfRange()
    {
        commandBubble.SetActive(false);
        InputManager.OnInteract.RemoveListener(OnInteract);
    }
}

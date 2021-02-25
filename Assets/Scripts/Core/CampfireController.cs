using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] bool lightOnAwake = false;

    void Awake()
    {
        if (lightOnAwake)
            Light();
    }

    public void Light()
    {
        animator.SetBool("lit", true);
    }

    public void PutOut()
    {
        animator.SetBool("lit", false);
    }
}

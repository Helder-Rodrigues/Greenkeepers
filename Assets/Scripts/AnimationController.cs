using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        animator.enabled = true;
    }

    public void StopAnimation()
    {
        animator.enabled = false;
    }
}

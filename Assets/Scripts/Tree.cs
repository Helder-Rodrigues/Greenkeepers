using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private GameObject tree;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Grow()
    {
        animator.enabled = true;
        tree.SetActive(true);
    }
}

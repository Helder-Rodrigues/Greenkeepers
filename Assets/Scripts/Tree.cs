using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private DetectTrigger spellTrigger;
    [SerializeField] private GameObject tree;

    public void Grow()
    {
        Vector3 treePos = new Vector3(transform.position.x, 0.91f, transform.position.z);
        Instantiate(tree, treePos, Quaternion.identity, transform.parent);

        spellTrigger.RemoveObjectFromList(gameObject);
        Destroy(gameObject);
    }
}

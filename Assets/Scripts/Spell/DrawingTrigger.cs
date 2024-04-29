using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawingTrigger : MonoBehaviour
{
    private CraftSpell craftSpell;
    private Color originalColor = CraftSpell.originalColor;
    private Color triggerColor = new Color(255/163, 255/44, 255/201, 255/146);

    private Material material;

    private void Start()
    {
        craftSpell = GetComponentInParent<CraftSpell>();

        material = GetComponent<MeshRenderer>().material;

        material.SetColor("_Color", originalColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameController.OVRInteraction(other.name))
        {
            material.SetColor("_Color", triggerColor);
            craftSpell.ChildDetected(this, other);
        }
    }
}

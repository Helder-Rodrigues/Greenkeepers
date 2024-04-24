using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStuff : MonoBehaviour
{
    [SerializeField] private GameObject objectToShow;
    private GrabInteractable grab;

    private void Start()
    {
        grab = this.gameObject.GetComponent<GrabInteractable>();
    }

    private void Update()
    {
        if (grab.State == InteractableState.Select) {
            objectToShow.SetActive(true); 
        }
    }
}

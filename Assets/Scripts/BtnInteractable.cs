using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnInteractable : MonoBehaviour
{
    [SerializeField] private PokeInteractable interactable;

    [SerializeField] private Transform camTranform;
    [SerializeField] private GameObject cam;
    private bool onGround = true;
    private Vector3 groundPosition;

    [SerializeField] private MeshRenderer blackScreen; 
    private const float fadeSpeed = 0.5f;

    private bool btnCanBePressed = true;
    private bool BtnCliked => interactable.State == InteractableState.Select;


    private void Update()
    {
        if (btnCanBePressed && BtnCliked)
            StartCoroutine(SwitchCameraView());
    }

    private IEnumerator SwitchCameraView()
    {
        btnCanBePressed = false;

        while (blackScreen.material.color.a < 1)
        {
            Color currentColor = blackScreen.material.color;
            float newAlpha = Mathf.MoveTowards(currentColor.a, 1f, fadeSpeed * Time.deltaTime);
            blackScreen.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;
        }

        if (onGround)
        {
            groundPosition = camTranform.position;
            cam.transform.position = new Vector3(camTranform.position.x,
                                             camTranform.position.y + 4.5f, camTranform.position.z - 5f);
        }
        else
            //should i remove '+'?
            cam.transform.position += groundPosition - camTranform.position; 

        onGround = !onGround;

        while (blackScreen.material.color.a > 0)
        {
            Color currentColor = blackScreen.material.color;
            float newAlpha = Mathf.MoveTowards(currentColor.a, 0f, fadeSpeed * Time.deltaTime);
            blackScreen.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;
        }

        btnCanBePressed = true;
    }
}

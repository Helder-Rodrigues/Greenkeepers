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
    private Vector3 groundPosition = Vector3.zero;

    [SerializeField] private MeshRenderer blackScreen; 
    private const float fadeSpeed = 0.5f;

    private bool btnCanBePressed = true;
    private bool BtnCliked => interactable.State == InteractableState.Select;


    private void Update()
    {

        print("air cam: " + cam.transform.position);
        print("cam: " + camTranform.position);
        print("ground: " + groundPosition);
        print(groundPosition - camTranform.position);

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
            cam.transform.position += new Vector3(0, 4.5f, -5f);
            groundPosition = camTranform.position;
        }
        else
            cam.transform.position += groundPosition - camTranform.position + new Vector3(0,-4.5f, 5f);

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

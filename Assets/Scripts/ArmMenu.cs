using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private Transform armTransform;

    private const float menuRotValueTrigger = 0.85f;
    private bool menuShowing = false;

    private void Update()
    {
        if (armTransform.localRotation.z > menuRotValueTrigger || armTransform.localRotation.z < -menuRotValueTrigger)
            ShowMenu();
        else if (menuShowing)
            CloseMenu();
    }

    private void ShowMenu()
    {
        if (menuShowing)
            return;
        menuShowing = true;

        menu.SetActive(true);
    }

    private void CloseMenu()
    {
        menuShowing = false;
        
        menu.SetActive(false);
    }
}

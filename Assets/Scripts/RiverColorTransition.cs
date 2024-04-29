using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverColorTransition : MonoBehaviour
{
    [SerializeField] private BarController bar;
    
    private Color startColor = new Color(0.533f, 0.749f, 1f, 1f); // 88BFFF
    private Color endColor = new Color(0.812f, 0.722f, 0.373f, 1f); // CFB85F
    private Renderer renderer;
    private float transColorPerct;
    private bool IsClean => transColorPerct < 0.4f;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        StartCoroutine(TransitionColor(30f));
    }

    IEnumerator TransitionColor(float transDurationInSec)
    {
        float elapsedTime = 0f;
        while (elapsedTime < transDurationInSec)
        {
            transColorPerct = elapsedTime / transDurationInSec;
            renderer.material.color = Color.Lerp(startColor, endColor, transColorPerct);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        FullDirty();
    }

    public void CleanWater()
    {
        if (!IsClean)
        {
            renderer.material.color = startColor;
            transColorPerct = 0f;
        }
    }

    public void DirtyWater()
    {
        if (transColorPerct == 0f)
            StartCoroutine(TransitionColor(30f));
    }

    private void FullDirty() => renderer.material.color = endColor;


    private void Update()
    {
        if (!IsClean)
            bar.Value -= 0.005f;
    }
}

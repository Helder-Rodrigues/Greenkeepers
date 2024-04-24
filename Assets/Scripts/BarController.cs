using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    private readonly float maxValue = 100f;
    private float _value = 50f;
    public float Value
    {
        set 
        { 
            _value = Mathf.Clamp(value, 0f, 100f); 
            Resize(); 
        }
        get { return _value; }
    }


    private void Start(){ Resize(); }

    private void Resize()
    {
        float newSize = Mathf.Clamp01(Value / maxValue);

        Vector3 vHelper = transform.localScale;
        vHelper.z = newSize;
        transform.localScale = vHelper;

        vHelper = transform.localPosition;
        vHelper.z = CalculateNewPosition(newSize);
        transform.localPosition = vHelper;
    }
    private float CalculateNewPosition(float newSize) => -5 * newSize + 5;
}

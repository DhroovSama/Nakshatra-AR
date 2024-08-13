using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatingUI : MonoBehaviour
{
    [SerializeField]
    private GameObject loactionRevealUI;

    [SerializeField]
    [Range(0f, 5f)]
    private float amplitude = 1f; 

    [SerializeField]
    [Range(0f, 5f)]
    private float frequency = 1f; 

    private Vector3 originalPosition;

    private void Start()
    {
        if (loactionRevealUI != null)
        {
            originalPosition = loactionRevealUI.transform.localPosition;
        }
    }

    private void Update()
    {
        if (loactionRevealUI != null)
        {
            float newY = originalPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;

            loactionRevealUI.transform.localPosition = new Vector3(originalPosition.x, newY, originalPosition.z);
        }
    }
}

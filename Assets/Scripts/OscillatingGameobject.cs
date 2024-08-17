using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatingGameobject : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 5f)]
    private float amplitude = 1f;

    [SerializeField]
    [Range(0f, 5f)]
    private float frequency = 1f;

    private Vector3 originalPosition;

    private void Start()
    {
        // Store the original position of the GameObject
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        // Calculate the new Y position using sine wave oscillation
        float newY = originalPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the new position to the GameObject
        transform.localPosition = new Vector3(originalPosition.x, newY, originalPosition.z);
    }
}

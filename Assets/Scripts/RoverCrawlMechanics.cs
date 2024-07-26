using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoverCrawlMechanics : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Button Left;
    [SerializeField] private GameObject leftWheel1;
    [SerializeField] private int rotationSpeed = 10;

    private bool isPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        Debug.Log("true");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        Debug.Log("false");
    }

    private void Update()
    {
        if (isPressed)
        {
            RotateLeftWheel();
        }
    }

    public void RotateLeftWheel()
    {
        float rotationAngle = rotationSpeed * Time.deltaTime;
        leftWheel1.transform.Rotate(0, 0, rotationAngle);
    }
}

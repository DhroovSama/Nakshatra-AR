using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class PinchDetection : MonoBehaviour
{
    [SerializeField]
    private InputAction scrollAction, touch0pos, touch1pos;


    [SerializeField]
    private float speed = 0.01f;

    [SerializeField]
    private float previousMagnitude = 0f;

    [SerializeField]
    private int touchCount = 0;
    public int TouchCount { get { return touchCount; } }

    private void Start()
    {

        var touch0contact = new InputAction
            (
            type: InputActionType.Button,
            binding: "<Touchscreen>/touch0/press"
            );

        touch0contact.Enable();

        var touch1contact = new InputAction
            (
            type: InputActionType.Button,
            binding: "<Touchscreen>/touch1/press"
            );

        touch1contact.Enable();

        touch0contact.performed += _ => touchCount++;
        touch1contact.performed += _ => touchCount++;

        touch0contact.canceled += _ =>
        {
            touchCount--;
            previousMagnitude = 0f;
        };

        touch1contact.canceled += _ =>
        {
            touchCount--;
            previousMagnitude = 0f;
        };

        scrollAction.Enable();
        scrollAction.performed += ctx => CameraZoom(ctx.ReadValue<Vector2>().y * speed);

        touch0pos.Enable();

        touch1pos.Enable();
        touch1pos.performed += _ =>
        {
            if (touchCount < 2)
                return;

            var magnitude = (touch0pos.ReadValue<Vector2>() - touch1pos.ReadValue<Vector2>()).magnitude;

            if (previousMagnitude == 0)
                previousMagnitude = magnitude;

            var difference = magnitude - previousMagnitude;

            previousMagnitude = magnitude;

            CameraZoom(-difference * speed);
        };
    }

    private void CameraZoom(float increment) => Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView + increment, 20, 60);
}


using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Rotatable : MonoBehaviour
{
    [Header("Touch Settings")]

    [SerializeField]
    private InputAction pressed, axis, touch0pos, touch1pos;

    [SerializeField]
    private float rotationSpeed = 0.1f;

    [SerializeField]
    private float pinchSpeed = 0.01f;

    private bool isRotateAllowed;

    private Vector2 rotation;

    private float previousMagnitude = 0f;

    [SerializeField]
    private int touchCount = 0;

    [Header("Other References")]

    [SerializeField]
    [Space]
    private VibrationController vibrationController;

    private Button lockObject;

    private Button placeObject;

    private bool isInteractionAllowed = true;

    [SerializeField]
    private GameObject secondaryObject; // Add this field for the additional GameObject

    private void Awake()
    {
        lockObject = GlobalUIProvider.getResetObejctButton();

        lockObject.onClick.AddListener(ToggleInteraction);

        pressed.Enable();
        axis.Enable();
        touch0pos.Enable();
        touch1pos.Enable();

        pressed.performed += _ => { if (isInteractionAllowed) StartCoroutine(Rotate()); };
        pressed.canceled += _ => { isRotateAllowed = false; };

        axis.performed += context => { if (isInteractionAllowed) rotation = context.ReadValue<Vector2>(); };

        var touch0contact = new InputAction(
            type: InputActionType.Button,
            binding: "<Touchscreen>/touch0/press"
        );
        var touch1contact = new InputAction(
            type: InputActionType.Button,
            binding: "<Touchscreen>/touch1/press"
        );

        touch0contact.Enable();
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
    }

    private void Update()
    {
        if (isInteractionAllowed && touchCount == 2)
        {
            DetectPinch();
        }
    }

    private IEnumerator Rotate()
    {
        isRotateAllowed = true;

        while (isRotateAllowed)
        {
            if (isInteractionAllowed && touchCount < 2)
            {
                rotation *= rotationSpeed;
                transform.Rotate(-Vector3.up, rotation.x, Space.World);
            }

            yield return null;
        }
    }

    private void DetectPinch()
    {
        if (!isInteractionAllowed)
            return;

        Vector2 touch0 = touch0pos.ReadValue<Vector2>();
        Vector2 touch1 = touch1pos.ReadValue<Vector2>();

        float currentMagnitude = Vector2.Distance(touch0, touch1);

        if (previousMagnitude == 0)
        {
            previousMagnitude = currentMagnitude;
            return;
        }

        float difference = currentMagnitude - previousMagnitude;

        Vector3 scaleChange = new Vector3(difference, difference, difference) * pinchSpeed;
        Vector3 newScale = transform.localScale + scaleChange;

        if (newScale.x <= 0.1f || newScale.y <= 0.1f || newScale.z <= 0.1f)
        {
            if (vibrationController != null)
            {
                vibrationController.VibratePhone_Heavy();
            }

            Debug.Log("Scaling Blocked");

            newScale.x = Mathf.Max(newScale.x, 0.1f);
            newScale.y = Mathf.Max(newScale.y, 0.1f);
            newScale.z = Mathf.Max(newScale.z, 0.1f);
        }

        // Update the scale of the primary and secondary GameObjects
        transform.localScale = newScale;
        if (secondaryObject != null)
        {
            secondaryObject.transform.localScale = newScale;
        }

        previousMagnitude = currentMagnitude;
    }

    private void ToggleInteraction()
    {
        isInteractionAllowed = !isInteractionAllowed;

        ColorBlock colors = lockObject.colors;

        if (isInteractionAllowed)
        {
            colors.normalColor = Color.green;
            Debug.Log("Interaction Enabled");
        }
        else
        {
            colors.normalColor = Color.red;
            Debug.Log("Interaction Locked");
        }

        lockObject.colors = colors;
    }
}

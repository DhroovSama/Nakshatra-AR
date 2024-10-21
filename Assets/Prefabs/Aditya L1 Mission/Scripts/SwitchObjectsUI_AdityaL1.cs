using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchObjectsUI_AdityaL1 : MonoBehaviour
{
    [SerializeField]
    private Scrollbar scrollBar;
    [Space] 

    //[SerializeField]
    //private Button rightButton, leftButton;

    [Space]
    [SerializeField, Tooltip("Auto Assigned")]
    private GameObject Object_1, Object_2;

    [Space]
    [SerializeField]
    private GameObject selectionMenu;

    private void Start()
    {
        StartCoroutine(WaitForMoonSurface());

        UpdateObjects(scrollBar.value);

        scrollBar.onValueChanged.AddListener(UpdateObjects);
    }

    private IEnumerator WaitForMoonSurface()
    {
        while (!PlaceOnPlane.IsMoonSurfaceSpawned())
        {
            yield return null;
        }

        AssignObjects();

        UpdateObjects(scrollBar.value);

        selectionMenu.SetActive(true);
    }

    private void AssignObjects()
    {
        if (Object_1 == null)
        {
            Object_1 = GameObject.FindWithTag("Sattelite");
            if (Object_1 == null)
            {
                Debug.LogError("Object_1 (Rover) not found in the scene.");
            }
        }

        if (Object_2 == null)
        {
            Object_2 = GameObject.FindWithTag("Rocket");
            if (Object_2 == null)
            {
                Debug.LogError("Object_2 (Lander) not found in the scene.");
            }
        }

        if (Object_2 != null)
        {
            Object_2.SetActive(false);
        }
    }

    private void UpdateObjects(float value)
    {
        if (Object_1 == null || Object_2 == null)
            return;

        if (value <= 0.5f)
        {
            Object_1.SetActive(true);
            Object_2.SetActive(false);
        }
        else
        {
            Object_1.SetActive(false);
            Object_2.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        scrollBar.onValueChanged.RemoveListener(UpdateObjects);
    }
}

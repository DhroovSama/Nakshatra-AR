using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwitchObjectsUI : MonoBehaviour
{
    [SerializeField]
    private Scrollbar scrollBar;

    [Space]
    [SerializeField, Tooltip("Auto Assigned")]
    private GameObject Object_1, Object_2;

    [SerializeField]
    private VibrationController vibrationController;

    [SerializeField]
    private UISoundSO uISoundSO;

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
            Object_1 = GameObject.FindWithTag("Rover");
            if (Object_1 == null)
            {
                Debug.LogError("Object_1 (Rover) not found in the scene.");
            }
        }

        if (Object_2 == null)
        {
            Object_2 = GameObject.FindWithTag("Lander");
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
            //PerformUserFeedback();

            Object_1.SetActive(true);
            Object_2.SetActive(false);
        }
        else
        {
            //PerformUserFeedback();

            Object_1.SetActive(false);
            Object_2.SetActive(true);
        }
    }

    private void PerformUserFeedback()
    {
        vibrationController.VibratePhone_Light();
        uISoundSO.PlayTapSound();
    }

    private void OnDestroy()
    {
        scrollBar.onValueChanged.RemoveListener(UpdateObjects);
    }
}

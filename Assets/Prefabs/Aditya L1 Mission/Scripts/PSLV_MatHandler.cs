using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PSLV_MatHandler : MonoBehaviour
{

    [SerializeField]
    private AdityaL1_TutorialHandler adityaL1_TutorialHandler;

    public PSLV_MatSO materialScriptableObject;

    [SerializeField]
    private GlobalUIProvider_AdityaL1 globalUI;

    [Space]
    [SerializeField]
    private GameObject launchButton;

    [SerializeField]
    private VibrationController vibrationController;

    [SerializeField]
    private UISoundSO soundSO;

    [SerializeField]
    private bool canClickPslvBottom = false;

    [HideInInspector]
    public UnityEvent rocketBottomEvent = new UnityEvent();

    private void Awake()
    {
        ResetMaterials();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PerformRaycast(Input.mousePosition);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                PerformRaycast(touch.position);
            }
        }
    }

    public void EnablePslvBottomClick()
    {
        canClickPslvBottom = true;

        rocketBottomEvent?.Invoke();
    }

    void PerformRaycast(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && canClickPslvBottom)
        {
            if (hit.collider.CompareTag("PSLV Bottom"))
            {
                vibrationController.VibratePhone_Light();
                soundSO.PlayTapSound();

                adityaL1_TutorialHandler.EnableHoldButtonTutorial();

                globalUI.userTap.SetActive(false);

                launchButton.SetActive(true);

                if (materialScriptableObject.pslvMaterials.Count > 0)
                {
                    foreach (Material mat in materialScriptableObject.pslvMaterials)
                    {
                        // Set the _BaseColor to white with full opacity
                        Color color = Color.white;
                        color.a = 1f;

                        mat.SetColor("_BaseColor", color);
                    }
                }
                else
                {
                    Debug.LogWarning("No materials found in the ScriptableObject.");
                }
            }
            else
            {
                Debug.Log("The object clicked does not have the tag 'PSLV Bottom'.");
            }

            canClickPslvBottom = false;
        }
    }


    private void ResetMaterials()
    {
        // Reset the materials to black with alpha 0 when the script is disabled (e.g., on scene change)
        if (materialScriptableObject != null && materialScriptableObject.pslvMaterials.Count > 0)
        {
            foreach (Material mat in materialScriptableObject.pslvMaterials)
            {
                // Set the _BaseColor to black with zero opacity
                Color color = Color.black;
                color.a = 0f;

                mat.SetColor("_BaseColor", color);
            }
        }
    }

    void OnDisable()
    {
        ResetMaterials();
    }

    public void print()
    {
        Debug.Log("hello");
    }
}

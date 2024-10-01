using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PSLV_MatHandler : MonoBehaviour
{
    public PSLV_MatSO materialScriptableObject;

    [SerializeField]
    private GlobalUIProvider_AdityaL1 globalUI;

    [Space]
    [SerializeField]
    private GameObject launchButton;

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

    void PerformRaycast(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("PSLV Bottom"))
            {
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

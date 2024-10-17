using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSLV_MaterialHandler : MonoBehaviour
{
    [SerializeField]
    private PSLV_MatSO pSLV_MatSO;

    private void Awake()
    {
        SetPSLV_MaterialsToColor();
    }

    private void SetPSLV_MaterialsToColor()
    {
        if (pSLV_MatSO.pslvMaterials.Count > 0)
        {
            foreach (Material mat in pSLV_MatSO.pslvMaterials)
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

    private void SetPSLV_MaterialsToBlack()
    {
        // Reset the materials to black with alpha 0 when the script is disabled (e.g., on scene change)
        if (pSLV_MatSO != null && pSLV_MatSO.pslvMaterials.Count > 0)
        {
            foreach (Material mat in pSLV_MatSO.pslvMaterials)
            {
                // Set the _BaseColor to black with zero opacity
                Color color = Color.black;
                color.a = 0f;

                mat.SetColor("_BaseColor", color);
            }
        }
    }

    private void OnDestroy()
    {
        SetPSLV_MaterialsToBlack();
    }
}

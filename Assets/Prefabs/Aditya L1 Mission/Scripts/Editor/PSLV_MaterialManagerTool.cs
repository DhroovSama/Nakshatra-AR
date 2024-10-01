#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PSLV_MaterialManagerTool : EditorWindow
{
    public PSLV_MatSO materialScriptableObject;

    [MenuItem("Tools/PSLV Material Opacity Toggle")]
    public static void ShowWindow()
    {
        GetWindow<PSLV_MaterialManagerTool>("PSLV Material Opacity Toggle");
    }

    private void OnGUI()
    {
        GUILayout.Label("PSLV Material Opacity Toggle", EditorStyles.boldLabel);

        materialScriptableObject = (PSLV_MatSO)EditorGUILayout.ObjectField("Material ScriptableObject", materialScriptableObject, typeof(PSLV_MatSO), false);

        if (GUILayout.Button("Reset Materials to 1 Opacity"))
        {
            OpacityMaterials_1();
        }

        if (GUILayout.Button("Reset Materials to 0 Opacity"))
        {
            OpacityMaterials_0();
        }
    }

    private void OpacityMaterials_1()
    {
        if (materialScriptableObject != null && materialScriptableObject.pslvMaterials.Count > 0)
        {
            foreach (Material mat in materialScriptableObject.pslvMaterials)
            {
                Color color = Color.white;
                color.a = 1f;

                mat.SetColor("_BaseColor", color);
            }

            Debug.Log("Materials reset to black with zero opacity.");
        }
        else
        {
            Debug.LogWarning("No materials found in the ScriptableObject or the ScriptableObject is not assigned.");
        }
    }
    private void OpacityMaterials_0()
    {
        if (materialScriptableObject != null && materialScriptableObject.pslvMaterials.Count > 0)
        {
            foreach (Material mat in materialScriptableObject.pslvMaterials)
            {
                Color color = Color.black;
                color.a = 0f;

                mat.SetColor("_BaseColor", color);
            }

            Debug.Log("Materials reset to black with zero opacity.");
        }
        else
        {
            Debug.LogWarning("No materials found in the ScriptableObject or the ScriptableObject is not assigned.");
        }
    }
}
#endif

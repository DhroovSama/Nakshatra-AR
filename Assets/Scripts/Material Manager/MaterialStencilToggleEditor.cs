//using UnityEngine;
//using UnityEditor;
//using System.Collections.Generic;
//using UnityEditor.UIElements;

//public class MaterialStencilToggleEditor : EditorWindow
//{
//    private static bool toggleState = true;

//    private Vector2 scrollPosition = Vector2.zero;

//    private MaterialStencilToggleDataSO materialData;

//    [MenuItem("Tools/Toggle Material Stencil Test")]
//    public static void ShowWindow()
//    {
//        GetWindow<MaterialStencilToggleEditor>("Toggle Material Stencil Test");
//    }

//    private void OnEnable()
//    {
//        materialData = AssetDatabase.LoadAssetAtPath<MaterialStencilToggleDataSO>("Assets/Scripts/MaterialStencilToggleData.asset");
//        if (materialData == null)
//        {
//            materialData = ScriptableObject.CreateInstance<MaterialStencilToggleDataSO>();
//            AssetDatabase.CreateAsset(materialData, "Assets/Scripts/MaterialStencilToggleData.asset");
//            AssetDatabase.SaveAssets();
//        }
//    }

//    private void OnGUI()
//    {
//        EditorGUILayout.LabelField("Toggle Material Stencil Test", EditorStyles.boldLabel);

//        toggleState = EditorGUILayout.Toggle("Toggle STENCILTEST", toggleState);

//        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(600));

//        EditorGUILayout.LabelField("Materials to Toggle:");
//        DisplayAndManageMaterials(ref materialData.LanderBody, "Lander Body");
//        DisplayAndManageMaterials(ref materialData.LanderDoor, "Lander Door");
//        DisplayAndManageMaterials(ref materialData.RoverBody, "Rover Body");
//        DisplayAndManageMaterials(ref materialData.RoverSolarPanels, "Rover Solar Panels");
//        DisplayAndManageMaterials(ref materialData.Wheels, "Wheels");
//        DisplayAndManageMaterials(ref materialData.Terrain, "Terrain");
//        DisplayAndManageMaterials(ref materialData.NightSky, "Night Sky");
//        DisplayAndManageMaterials(ref materialData.AsteroidsOnTerrain, "Asteroids On Terrain");

//        EditorGUILayout.EndScrollView();

//        GUILayout.Space(20);
//        OnGUIApply();
//    }


//    private void DisplayAndManageMaterials(ref Material[] materials, string label)
//    {
//        bool foldout = EditorGUILayout.Foldout(true, label, true);
//        if (foldout)
//        {
//            for (int i = 0; i < materials.Length; i++)
//            {
//                materials[i] = (Material)EditorGUILayout.ObjectField(materials[i], typeof(Material), true);
//            }

//            if (GUILayout.Button("Add Material to " + label))
//            {
//                ArrayUtility.Add(ref materials, null);
//            }

//            if (GUILayout.Button("Remove Last Material from " + label))
//            {
//                if (materials.Length > 0)
//                {
//                    ArrayUtility.RemoveAt(ref materials, materials.Length - 1);
//                }
//            }
//        }
//    }

//    private void ApplyToSelectedMaterials(Material[] materials)
//    {
//        foreach (var material in materials)
//        {
//            if (material != null)
//            {
//                material.SetInt("stest", toggleState ? 3 : 6);
//            }
//        }
//    }

//    private void OnGUIApply()
//    {
//        if (GUILayout.Button("Apply to Selected Materials"))
//        {
//            ApplyToSelectedMaterials(materialData.LanderBody);
//            ApplyToSelectedMaterials(materialData.LanderDoor);
//            ApplyToSelectedMaterials(materialData.RoverBody);
//            ApplyToSelectedMaterials(materialData.RoverSolarPanels);
//            ApplyToSelectedMaterials(materialData.Wheels);
//            ApplyToSelectedMaterials(materialData.Terrain);
//            ApplyToSelectedMaterials(materialData.NightSky);
//            ApplyToSelectedMaterials(materialData.AsteroidsOnTerrain);
//        }
//    }

//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MaterialStencilPropertymanager : MonoBehaviour
{
    [SerializeField]
    private MaterialStencilToggleDataSO materialStencilToggleDataSO;

    private void Awake()
    {
        SetStencil_Equal_ToAllMaterials();
    }

    private void SetStencil_Equal_ToAllMaterials()
    {
        Material[] allMaterials = materialStencilToggleDataSO.GetAllMaterials();

        foreach (Material material in allMaterials)
        {
            material.SetInt("stest", (int)CompareFunction.Equal);
        }
    }

    private void SetStencil_NotEqual_ToAllMaterials()
    {
        Material[] allMaterials = materialStencilToggleDataSO.GetAllMaterials();

        foreach (Material material in allMaterials)
        {
            material.SetInt("stest", (int)CompareFunction.NotEqual);
        }
    }

    public void getSetStencil_Equal_ToAllMaterials()
    {
        SetStencil_Equal_ToAllMaterials();
    }

    public void getSetStencil_NotEqual_ToAllMaterials()
    {
        SetStencil_NotEqual_ToAllMaterials();
    }
}

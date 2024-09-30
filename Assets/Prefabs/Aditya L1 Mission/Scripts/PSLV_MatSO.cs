using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PSLV Materials", menuName = "ScriptableObjects/PSLV Mat", order = 1)]
public class PSLV_MatSO : ScriptableObject
{
    public List<Material> pslvMaterials = new List<Material>();
}

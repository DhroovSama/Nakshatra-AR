using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TransformTextDebugger : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI transformText;

    void Start()
    {
        if (transformText == null)
        {
            Debug.LogError("TransformTextDebugger: TextMeshProUGUI component is not assigned.");
        }
    }

    void Update()
    {
        if (transformText != null)
        {
            transformText.text = $"Position: {transform.position}";
        }
    }
}

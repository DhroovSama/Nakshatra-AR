using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    [SerializeField]
    private bool isEnabled = true;

    [SerializeField]
    private Button toggleButton;

    [SerializeField]
    private GameObject infoCanvas;

    [SerializeField]
    private VoiceOverData infoVO;

    void Start()
    {
        toggleButton.onClick.AddListener(ToggleScriptState);
    }

    public void ToggleScriptState()
    {
        if (isEnabled)
        {
            DisableScript();
        }
        else
        {
            infoVO.PlayVoiceOver();
            EnableScript();
        }
    }

    private void DisableScript()
    {
        isEnabled = false;
        infoCanvas.SetActive(false);
    }

    private void EnableScript()
    {
        isEnabled = true;
        infoCanvas.SetActive(true);

        StartCoroutine(DisableCanvasAfterDelay(10f)); 
    }

    private IEnumerator DisableCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        infoCanvas.SetActive(false);
    }
}

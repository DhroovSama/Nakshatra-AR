using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstTimeRoverTutorialHandler : MonoBehaviour
{
    [SerializeField, Tooltip("Auto Assigned")]
    private GameObject terrainScanButtonHandTapUI;

    [SerializeField, Tooltip("Auto Assigned")]
    private Button terrainScanButton;

    [SerializeField]
    private VoiceOverData pointCameraVO;

    private bool isButtonClicked = false;

    private void Start()
    {
        if(terrainScanButton == null)
        {
            terrainScanButton = LanderControlsUIManager.getTerrainScannerControlButton().GetComponent<Button>();
        }

        if(terrainScanButtonHandTapUI == null)
        {
            terrainScanButtonHandTapUI = LanderControlsUIManager.GetTerrainScannerControlButtonHandTouchUI();
        }

    }

    public void TriggerLoactionPopupTutorial()
    {
        VoiceOverManager.Instance.TriggerVoiceOver(pointCameraVO);

        if (!isButtonClicked)
        {
            terrainScanButton.onClick.AddListener(OnTerrainScanButtonClick);
        }
    }

    private void OnTerrainScanButtonClick()
    {
        if (!isButtonClicked)
        {
            isButtonClicked = true;

            terrainScanButtonHandTapUI.SetActive(false);

            terrainScanButton.onClick.RemoveListener(OnTerrainScanButtonClick);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteAIVoice : MonoBehaviour
{
    [SerializeField]
    private AudioSource AI_audioSource;

    [SerializeField]
    private Button muteUnButton;

    [SerializeField]
    private Sprite muteSprite, unMuteSprite;

    [SerializeField]
    private bool isAiMute = false;

    private Image buttonImage;

    private void Start()
    {
        // Attempt to get the Image component
        buttonImage = muteUnButton.GetComponent<Image>();

        // Check if Image component was found
        if (buttonImage == null)
        {
            Debug.LogError("Image component not found on the Button.");
            return; // Exit if Image is not found to prevent further errors
        }

        UpdateButtonSprite();
    }

    public void MuteUnmuteAIFunction()
    {
        if (buttonImage == null) return; // Prevent further actions if Image is null

        if (isAiMute)
        {
            AIVoice_UnMute();
            isAiMute = false;
        }
        else
        {
            AIVoice_Mute();
            isAiMute = true;
        }

        UpdateButtonSprite();
    }

    private void AIVoice_Mute()
    {
        AI_audioSource.mute = true;
    }

    private void AIVoice_UnMute()
    {
        AI_audioSource.mute = false;
    }

    private void UpdateButtonSprite()
    {
        if (isAiMute)
        {
            buttonImage.sprite = muteSprite;
        }
        else
        {
            buttonImage.sprite = unMuteSprite;
        }
    }
}

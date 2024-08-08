using UnityEngine;

public class VoiceOverManager : MonoBehaviour
{
    public static VoiceOverManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerVoiceOver(VoiceOverData voiceOverData)
    {
        // Play the audio clip associated with this voice-over data
        AudioManager.Instance.PlayAudioClip(voiceOverData.voiceOverClip);

        // Pass the subtitles, button options, and next VoiceOverData to the SubtitleManager
        SubtitleManager.Instance.DisplaySubtitles(
            voiceOverData.subtitles,
            voiceOverData.showButtonAtEnd,
            voiceOverData.buttonText,
            voiceOverData.nextVoiceOverData 
        );
    }
}

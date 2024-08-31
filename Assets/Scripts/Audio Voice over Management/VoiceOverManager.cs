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
        AudioManager.Instance.PlayAudioClip(voiceOverData.voiceOverClip);

        SubtitleManager.Instance.DisplaySubtitles(
            voiceOverData.subtitles,
            voiceOverData.showButtonAtEnd,
            voiceOverData.buttonText,
            voiceOverData.nextVoiceOverData 
        );
    }
}

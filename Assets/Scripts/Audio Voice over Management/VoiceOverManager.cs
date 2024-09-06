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
        AudioClip clipToPlay;

        if (LanguageManager.Instance.useHindiAudio && voiceOverData.hindiVoiceOverClip != null)
        {
            clipToPlay = voiceOverData.hindiVoiceOverClip;
        }
        else
        {
            clipToPlay = voiceOverData.englishVoiceOverClip;
        }

        AudioManager.Instance.PlayAudioClip(clipToPlay);

        SubtitleManager.Instance.DisplaySubtitles(
            voiceOverData.subtitles,
            voiceOverData.showButtonAtEnd,
            voiceOverData.buttonText,
            voiceOverData.nextVoiceOverData
        );
    }
}

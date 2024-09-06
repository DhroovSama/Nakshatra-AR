using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "VoiceOverData", menuName = "ScriptableObjects/VoiceOverData", order = 1)]
public class VoiceOverData : ScriptableObject
{
    public AudioClip englishVoiceOverClip; // English audio clip
    public AudioClip hindiVoiceOverClip;   // Hindi audio clip

    [System.Serializable]
    public class SubtitleLine
    {
        [TextArea]
        public string sentence; // The subtitle sentence
        public float displayDuration; // Duration to display this sentence
    }

    public List<SubtitleLine> subtitles = new List<SubtitleLine>();

    // Fields for the button
    public bool showButtonAtEnd;
    public string buttonText;

    // Reference to another VoiceOverData to play after the button is clicked
    public VoiceOverData nextVoiceOverData;

    public void PlayVoiceOver()
    {
        VoiceOverManager.Instance.TriggerVoiceOver(this);
    }
}

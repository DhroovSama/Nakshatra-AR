using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "VoiceOverData", menuName = "ScriptableObjects/VoiceOverData", order = 1)]
public class VoiceOverData : ScriptableObject
{
    public AudioClip voiceOverClip;

    [System.Serializable]
    public class SubtitleLine
    {
        [TextArea]
        public string sentence; // The subtitle sentence
        public float displayDuration; // Duration to display this sentence
    }

    public List<SubtitleLine> subtitles = new List<SubtitleLine>();
}

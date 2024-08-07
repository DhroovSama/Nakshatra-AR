using UnityEngine;

[CreateAssetMenu(fileName = "VoiceOverData", menuName = "ScriptableObjects/VoiceOverData", order = 1)]
public class VoiceOverData : ScriptableObject
{
    public AudioClip voiceOverClip;
    public string subtitle;
}

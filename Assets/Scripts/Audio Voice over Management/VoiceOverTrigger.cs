using UnityEngine;

public class VoiceOverTrigger : MonoBehaviour
{
    public VoiceOverData voiceOverData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VoiceOverManager.Instance.TriggerVoiceOver(voiceOverData);
        }
    }
}

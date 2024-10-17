using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FactCountVOHandler : MonoBehaviour
{
    [SerializeField]
    private FactsCollectedGlobal factsCollectedGlobal;
    [Space]

    [SerializeField]
    private TextMeshProUGUI factCount; // For UI display of fact count

    [SerializeField]
    private VoiceOverData factsCollectedVO_1;
    [SerializeField]
    private VoiceOverData factsCollectedVO_2;
    [SerializeField]
    private VoiceOverData factsCollectedVO_3;
    [SerializeField]
    private VoiceOverData factsCollectedVO_4;
    [SerializeField]
    private VoiceOverData factsCollectedVO_5;
    [Space]

    [SerializeField]
    private bool playedVO_1, playedVO_2, playedVO_3, playedVO_4, playedVO_5 = false;

    private void Update()
    {
        factsCollectedChecker();
    }

    private void factsCollectedChecker()
    {
        if(factsCollectedGlobal.FactsCollected == 1 && !playedVO_1)
        {
            VoiceOverManager.Instance.TriggerVoiceOver(factsCollectedVO_1);

            playedVO_1 = true;
        }
        else if(factsCollectedGlobal.FactsCollected == 2 && !playedVO_2)
        {
            VoiceOverManager.Instance.TriggerVoiceOver(factsCollectedVO_2);

            playedVO_2 = true;
        }
        else if (factsCollectedGlobal.FactsCollected == 3 && !playedVO_3)
        {
            VoiceOverManager.Instance.TriggerVoiceOver(factsCollectedVO_3);

            playedVO_3 = true;
        }
        else if (factsCollectedGlobal.FactsCollected == 4 && !playedVO_4)
        {
            VoiceOverManager.Instance.TriggerVoiceOver(factsCollectedVO_4);

            playedVO_4 = true;
        }
        else if(factsCollectedGlobal.FactsCollected == 5 && !playedVO_5)
        {
            VoiceOverManager.Instance.TriggerVoiceOver(factsCollectedVO_5);

            playedVO_5 = true;
        }
    }
}

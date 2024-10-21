using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PSLV_CountdownManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI countDownText;
    
    [SerializeField]
    private Button pslvLaunchButton;

    [Header("SFX")]
    [SerializeField]
    private AudioClip countDownSound;

    [SerializeField]
    private AudioClip zeroSound;

    private bool isCountingDown = false;

    private void Start()
    {

        if (countDownText != null)
        {
            countDownText.text = ""; 
        }
        else
        {
            Debug.LogError("Countdown Text is not assigned in the inspector.");
        }
    }

    public IEnumerator StartCountdown()  
    {
        if (!isCountingDown)
        {
            yield return StartCoroutine(CountdownRoutine());  
        }
    }


    private IEnumerator CountdownRoutine()
    {
        isCountingDown = true;

        countDownText.gameObject.SetActive(true);

        pslvLaunchButton.gameObject.SetActive(false);

        int countdown = 5; 

        while (countdown >= 0)
        {
            if (countDownText != null)
            {
                countDownText.text = countdown.ToString();
            }

            GlobalAudioPlayer.GetPlaySound(countDownSound);

            yield return new WaitForSeconds(1f); 
            countdown--;

            if (countdown == 0)
            {
                GlobalAudioPlayer.GetPlaySound(zeroSound);
            }
        }

        isCountingDown = false; 

        countDownText.gameObject.SetActive(false);
    }
}

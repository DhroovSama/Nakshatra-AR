using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FactsCollectedGlobal : MonoBehaviour
{
    private static FactsCollectedGlobal instance;

    [SerializeField]
    private GameObject factsCountUI;

    [SerializeField]
    private TextMeshProUGUI factsCountUI_text;

    [SerializeField]
    private int factsCollected;

    [SerializeField]
    private GameObject GameEndScreen;

    private void Awake()
    {
         instance = this;   
    }

    private void Update()
    {
        if(factsCollected !=5)
        {
            factsCountUI_text.text = factsCollected.ToString() + "/5";
        }
        else
        {
            Debug.Log("All Facts Are Collected By The Player");

            GameEndScreen.SetActive(true);
        }
        
    }

    private void IncrementFactsCollected(int count)
    {
        factsCollected += count;
    }

    public static void getIncrementFactsCollected(int factsCount)
    {
        instance.IncrementFactsCollected(factsCount);
    }


    public static GameObject getFactsCountUI()
    {
        return instance.factsCountUI;   
    }

    public static TextMeshProUGUI getFactsCountUI_text()
    {
        return instance.factsCountUI_text;
    }
}

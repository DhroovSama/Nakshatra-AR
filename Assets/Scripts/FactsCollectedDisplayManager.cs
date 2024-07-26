using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactsCollectedDisplayManager : MonoBehaviour
{
    [SerializeField]
    private RawImage factDisplay;

    [SerializeField]
    private UI_Manager manager;

    private void Start()
    {
        SharedGameManagerEventSystem.OnNewFactToDisplay += HandleNewFactToDisplay;
    }

    private void OnDestroy()
    {
        SharedGameManagerEventSystem.OnNewFactToDisplay -= HandleNewFactToDisplay;
    }

    //private void Update()
    //{
    //    if (manager.HasLanderSpawned)
    //    {

    //    }
    //}

    private void DisplayFactTriggered()
    {

    }

    private void HandleNewFactToDisplay(Texture newFactTexture)
    {
        factDisplay.gameObject.SetActive(true);

        factDisplay.texture = newFactTexture;

        StartCoroutine(DisableFactDisplay());
    }

    private IEnumerator DisableFactDisplay()
    {
        yield return new WaitForSeconds(4.0f);

        factDisplay.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollapsibleMenu_Main : MonoBehaviour
{
    [SerializeField]
    private Button arrowButton;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private GameObject collapsibleMenu;

    [SerializeField]
    private GameObject optionsUI;

    [SerializeField]
    private Toggle languageToggle;

    [SerializeField]
    private float timeToWaitTillMenuCollapse;

    [SerializeField]
    private bool isRotated = false;

    private RectTransform rectTransform;

    void Start()
    {
        CollapseMenu();

        if (arrowButton != null)
        {
            arrowButton.onClick.AddListener(OnArrowButtonClick);
        }

        if (languageToggle != null)
        {
            languageToggle.onValueChanged.AddListener(delegate { ChangeLanguage(languageToggle.isOn); });
        }

        rectTransform = arrowButton.GetComponent<RectTransform>();
    }

    private void OnArrowButtonClick()
    {
        if (!isRotated)
        {
            RotateArrowButton();
            MoveCollapsibleMenuForward();
        }
        else
        {
            RotateArrowButtonBackward();
            MoveCollapsibleMenuBackward();
        }
    }

    private void RotateArrowButton()
    {
        rectTransform.localEulerAngles = new Vector3(rectTransform.localEulerAngles.x, rectTransform.localEulerAngles.y, 90f);
        isRotated = true;
    }

    private void RotateArrowButtonBackward()
    {
        rectTransform.localEulerAngles = new Vector3(rectTransform.localEulerAngles.x, rectTransform.localEulerAngles.y, -90f);
        isRotated = false;
    }

    private void MoveCollapsibleMenuForward()
    {
        RectTransform menuRectTransform = collapsibleMenu.GetComponent<RectTransform>();
        menuRectTransform.localPosition += offset;

        optionsUI.SetActive(false);
    }

    private void MoveCollapsibleMenuBackward()
    {
        RectTransform menuRectTransform = collapsibleMenu.GetComponent<RectTransform>();
        menuRectTransform.localPosition -= offset;

        optionsUI.SetActive(true);
    }

    public void CollapseMenu()
    {
        StartCoroutine(CollapseAfterSomeTime());
    }

    private IEnumerator CollapseAfterSomeTime()
    {
        yield return new WaitForSeconds(timeToWaitTillMenuCollapse);

        RotateArrowButton();
        MoveCollapsibleMenuForward();
    }

    private void ChangeLanguage(bool isOn)
    {
        LanguageManager.Instance.SetLanguage(isOn);
    }

    public void GoToMainMenuScene()
    {
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollapsibleUI : MonoBehaviour
{
    [SerializeField]
    private Button arrowButton;

    [SerializeField]
    private GameObject collapsibleMenu;

    [SerializeField]
    private GameObject backToMenuButton;

    [SerializeField]
    private float timeToWaitTillMenuCollapse;

    [SerializeField]
    private bool isRotated = false;

    private RectTransform rectTransform;

    void Start()
    {
        if (arrowButton != null)
        {
            arrowButton.onClick.AddListener(OnArrowButtonClick);
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
        menuRectTransform.localPosition += new Vector3(200, 0, 0); // Adds 200 to the X position

        backToMenuButton.SetActive(false);
    }

    private void MoveCollapsibleMenuBackward()
    {
        RectTransform menuRectTransform = collapsibleMenu.GetComponent<RectTransform>();
        menuRectTransform.localPosition -= new Vector3(200, 0, 0); // Subtracts 200 to the X position

        backToMenuButton.SetActive(true);
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
}

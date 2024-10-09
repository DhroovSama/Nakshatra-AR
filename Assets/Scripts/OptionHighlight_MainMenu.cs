using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionHighlight_MainMenu : MonoBehaviour
{
    [SerializeField]
    private List<Button> menuOptions = new List<Button>();

    private Dictionary<Button, Color> defaultColors = new Dictionary<Button, Color>();
    private Color highlightColor = new Color(0f, 1f, 1f); // Cyan

    void Start()
    {
        // Store default colors of each button's image
        foreach (Button btn in menuOptions)
        {
            Image img = btn.GetComponent<Image>();
            if (img != null)
            {
                defaultColors[btn] = img.color;
            }

            // Add our highlighting listener without overriding existing onClick events
            Button capturedBtn = btn; // Capture the current button
            btn.onClick.AddListener(() => OnButtonClicked(capturedBtn));
        }

        // Highlight the first button
        if (menuOptions.Count > 0)
        {
            HighlightButton(menuOptions[0]);
        }
    }

    private void OnButtonClicked(Button clickedButton)
    {
        // Unhighlight all buttons
        foreach (Button btn in menuOptions)
        {
            UnhighlightButton(btn);
        }

        // Highlight the clicked button
        HighlightButton(clickedButton);

        // Any other functions assigned to the button's onClick event in the Inspector will also be called
    }

    private void HighlightButton(Button btn)
    {
        Image img = btn.GetComponent<Image>();
        if (img != null)
        {
            img.color = highlightColor;
        }
    }

    private void UnhighlightButton(Button btn)
    {
        Image img = btn.GetComponent<Image>();
        if (img != null && defaultColors.ContainsKey(btn))
        {
            img.color = defaultColors[btn];
        }
    }
}

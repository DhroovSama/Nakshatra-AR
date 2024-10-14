using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField]
    private Button option_1, option_2, option_3;

    [Space]
    [SerializeField]
    private TextMeshProUGUI OptionText_1, OptionText_2, OptionText_3;

    [Space]
    [SerializeField]
    private Texture2D rightOption;

    [SerializeField]
    private Texture2D wrongOption;

    [Space]
    [SerializeField]
    private int correctOptionIndex = 0;

    [SerializeField]
    private UISoundSO uISoundSO;

    void Start()
    {
        option_1.onClick.AddListener(() => CheckOption(0));
        option_2.onClick.AddListener(() => CheckOption(1));
        option_3.onClick.AddListener(() => CheckOption(2));
    }

    void CheckOption(int optionIndex)
    {
        bool isCorrect = optionIndex == correctOptionIndex;

        Sprite rightSprite = Sprite.Create(rightOption, new Rect(0, 0, rightOption.width, rightOption.height), new Vector2(0.5f, 0.5f));

        Sprite wrongSprite = Sprite.Create(wrongOption, new Rect(0, 0, wrongOption.width, wrongOption.height), new Vector2(0.5f, 0.5f));

        switch (optionIndex)
        {
            case 0:
                if (isCorrect)
                {
                    uISoundSO.PlayCorrectSound();

                    option_1.image.sprite = rightSprite;
                    OptionText_1.color = Color.white;

                    option_2.image.sprite = wrongSprite;
                    option_3.image.sprite = wrongSprite;

                    OptionText_2.color = Color.white;
                    OptionText_3.color = Color.white;
                }
                else
                {
                    uISoundSO.PlayWrongSound();

                    option_1.image.sprite = wrongSprite;

                    HighlightCorrectAnswer();
                }
                break;

            case 1:
                if (isCorrect)
                {
                    uISoundSO.PlayCorrectSound();

                    option_2.image.sprite = rightSprite;
                    OptionText_2.color = Color.white;

                    option_1.image.sprite = wrongSprite;
                    option_3.image.sprite = wrongSprite;

                    OptionText_1.color = Color.white;
                    OptionText_3.color = Color.white;
                }
                else
                {
                    uISoundSO.PlayWrongSound();

                    option_2.image.sprite = wrongSprite;
                    HighlightCorrectAnswer();
                }
                break;

            case 2:
                if (isCorrect)
                {
                    uISoundSO.PlayCorrectSound();

                    option_3.image.sprite = rightSprite;
                    OptionText_3.color = Color.white;

                    option_1.image.sprite = wrongSprite;
                    option_2.image.sprite = wrongSprite;

                    OptionText_1.color = Color.white;
                    OptionText_2.color = Color.white;
                }
                else
                {
                    uISoundSO.PlayWrongSound();

                    option_3.image.sprite = wrongSprite;
                    HighlightCorrectAnswer();
                }
                break;
        }
    }

    private void HighlightCorrectAnswer()
    {
        Sprite rightSprite = Sprite.Create(rightOption, new Rect(0, 0, rightOption.width, rightOption.height), new Vector2(0.5f, 0.5f));

        Sprite wrongSprite = Sprite.Create(wrongOption, new Rect(0, 0, wrongOption.width, wrongOption.height), new Vector2(0.5f, 0.5f));

        switch (correctOptionIndex)
        {
            case 0:
                option_1.image.sprite = rightSprite;

                OptionText_1.color = Color.white;

                OptionText_2.color = Color.white;
                OptionText_3.color = Color.white;

                option_2.image.sprite= wrongSprite;
                option_3.image.sprite = wrongSprite;
                break;
            case 1:
                option_2.image.sprite = rightSprite;

                OptionText_2.color = Color.white;

                OptionText_3.color = Color.white;
                OptionText_1.color = Color.white;

                option_3.image.sprite = wrongSprite;
                option_1.image.sprite = wrongSprite;
                break;
            case 2:
                option_3.image.sprite = rightSprite;

                OptionText_3.color = Color.white;

                OptionText_1.color = Color.white;
                OptionText_2.color = Color.white;

                option_1.image.sprite = wrongSprite;
                option_2.image.sprite = wrongSprite;
                break;
        }
    }


}

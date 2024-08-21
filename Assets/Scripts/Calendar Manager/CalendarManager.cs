using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CalendarManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Dropdown monthDropdown;
    [SerializeField] private TMP_Dropdown yearDropdown;
    [SerializeField] private GridLayoutGroup calendarGrid;
    [SerializeField] private GameObject dayTextPrefab;
    [SerializeField] private GameObject dayNamePrefab;

    [Header("Special Dates")]
    [SerializeField] private SpecialDatesSO specialDates;

    [Header("Date Info Display")]
    [SerializeField] private GameObject dateInfoDisplayPrefab;
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI linkText;

    [Header("Navigation Buttons")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;

    [Header("Haptic Feedback")]
    [SerializeField] private VibrationController vibrationController; 

    private readonly string[] dayNames = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

    private void Start()
    {
        PopulateYearDropdown();
        PopulateDayNames();
        monthDropdown.onValueChanged.AddListener(delegate { UpdateCalendar(); });
        yearDropdown.onValueChanged.AddListener(delegate { UpdateCalendar(); });

        backButton.onClick.AddListener(PreviousMonth);
        nextButton.onClick.AddListener(NextMonth);

        UpdateCalendar();
    }

    private void PopulateYearDropdown()
    {
        yearDropdown.ClearOptions();
        int startYear = 2000;
        int endYear = 2024;

        for (int i = startYear; i <= endYear; i++)
        {
            yearDropdown.options.Add(new TMP_Dropdown.OptionData(i.ToString()));
        }

        int currentYear = DateTime.Now.Year;
        yearDropdown.value = (currentYear >= startYear && currentYear <= endYear) ? currentYear - startYear : endYear - startYear;
    }

    private void PopulateDayNames()
    {
        foreach (Transform child in calendarGrid.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (string dayName in dayNames)
        {
            GameObject dayNameText = Instantiate(dayNamePrefab, calendarGrid.transform);
            dayNameText.GetComponent<TextMeshProUGUI>().text = dayName;
        }
    }

    private void UpdateCalendar()
    {
        // Clear previous calendar dates (skip the first 7 children, which are the day names)
        for (int i = 7; i < calendarGrid.transform.childCount; i++)
        {
            Destroy(calendarGrid.transform.GetChild(i).gameObject);
        }

        int month = monthDropdown.value + 1;
        int year = int.Parse(yearDropdown.options[yearDropdown.value].text);

        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        int daysInMonth = DateTime.DaysInMonth(year, month);
        int startDay = (int)firstDayOfMonth.DayOfWeek;

        // Fill in the days before the 1st of the month
        for (int i = 0; i < startDay; i++)
        {
            Instantiate(dayTextPrefab, calendarGrid.transform).GetComponent<TextMeshProUGUI>().text = "";
        }

        List<SerializableDate> highlightedDates = specialDates.datesToHighlight;
        for (int day = 1; day <= daysInMonth; day++)
        {
            GameObject dayText = Instantiate(dayTextPrefab, calendarGrid.transform);
            TextMeshProUGUI dayTMP = dayText.GetComponent<TextMeshProUGUI>();
            dayTMP.text = day.ToString();

            DateTime currentDate = new DateTime(year, month, day);
            SerializableDate specialDate = highlightedDates.Find(date => date.ToDateTime() == currentDate);

            if (specialDate != null)
            {
                dayTMP.color = Color.red;

                Button dayButton = dayText.AddComponent<Button>();
                dayButton.onClick.AddListener(() => {
                    ShowHighlightedDateInfo(specialDate);
                    vibrationController?.VibratePhone_Light(); // Trigger light haptic feedback
                });
            }
        }
    }


    private void ShowHighlightedDateInfo(SerializableDate specialDate)
    {
        dateText.text = specialDate.ToDateTime().ToShortDateString();
        descriptionText.text = specialDate.description;

        linkText.text = "(Click Here)";

        dateInfoDisplayPrefab.SetActive(true);

        Button linkButton = linkText.GetComponent<Button>();
        linkButton.onClick.RemoveAllListeners();

        linkButton.onClick.AddListener(() => {
            vibrationController?.VibratePhone_Light(); 
            Application.OpenURL(specialDate.link);
        });
    }

    private void PreviousMonth()
    {
        int month = monthDropdown.value;
        int year = yearDropdown.value;

        if (month == 0) // January
        {
            if (year > 0) // Prevents going below the minimum year
            {
                yearDropdown.value--;
                monthDropdown.value = 11; // December
            }
        }
        else
        {
            monthDropdown.value--;
        }

        UpdateCalendar();
    }

    private void NextMonth()
    {
        int month = monthDropdown.value;
        int year = yearDropdown.value;

        if (month == 11) // December
        {
            if (year < yearDropdown.options.Count - 1) // Prevents going above the maximum year
            {
                yearDropdown.value++;
                monthDropdown.value = 0; // January
            }
        }
        else
        {
            monthDropdown.value++;
        }

        UpdateCalendar();
    }
}

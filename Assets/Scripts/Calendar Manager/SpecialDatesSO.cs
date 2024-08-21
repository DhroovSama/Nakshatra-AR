using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialDates", menuName = "ScriptableObjects/SpecialDates", order = 1)]
public class SpecialDatesSO : ScriptableObject
{
    public List<SerializableDate> datesToHighlight = new List<SerializableDate>();

    public List<DateTime> GetHighlightedDates()
    {
        List<DateTime> dateTimes = new List<DateTime>();
        foreach (var serializableDate in datesToHighlight)
        {
            dateTimes.Add(serializableDate.ToDateTime());
        }
        return dateTimes;
    }
}

using System;
using UnityEngine;

[Serializable]
public class SerializableDate
{
    [Range(1, 31)]
    public int day;
    [Range(1, 12)]
    public int month;
    public int year;
    public string description; 
    public string link; 

    public DateTime ToDateTime()
    {
        return new DateTime(year, month, day);
    }
}

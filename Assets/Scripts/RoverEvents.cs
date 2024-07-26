using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public static class RoverEvents 
{
    public static event Action OnRoverForward;

    public static event Action OnRoverBackward;

    public static event Action OnRotateRoverLeft;

    public static event Action OnRotateRoverRight;

    public static void RotateRoverLeft()
    {
        OnRotateRoverLeft?.Invoke();
    }

    public static void RotateRoverRight()
    {
        OnRotateRoverRight?.Invoke();
    }

    public static void RoverForward()
    {
        OnRoverForward?.Invoke();
    }

    public static void RoverBackward()
    {
        OnRoverBackward?.Invoke();
    }
}


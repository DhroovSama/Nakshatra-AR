using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class RoverControlsUIButtonManager : MonoBehaviour
{
    [SerializeField]
    private bool isLeftButtonPressed, isRightButtonPressed, isForwardButtonPressed, isBackwardButtonPressed = false;

    private void Update()
    {
        if (isLeftButtonPressed)
        {
            CallRotateRoverLeft();
        }

        if(isRightButtonPressed)
        {
            CallRotateRoverRight();
        }

        if(isForwardButtonPressed)
        {
            CallMoveRoverForward();
        }

        if(isBackwardButtonPressed)
        {
            CallMoveRoverBackward();
        }
    }

    public void isLeftButtonPressed_True()
    {
        isLeftButtonPressed = true;
    }

    public void isLeftButtonPressed_false()
    {
        isLeftButtonPressed = false;
    }

    public void isRightButtonPressed_True()
    {
        isRightButtonPressed = true;
    }
    public void isRightButtonPressed_False()
    {
        isRightButtonPressed = false;
    }

    public void isForwardButtonPressed_True()
    {
        isForwardButtonPressed = true;
    }

    public void isForwardButtonPressed_false()
    {
        isForwardButtonPressed = false;
    }

    public void isBackwardButtonPressed_True()
    {
        isBackwardButtonPressed = true;
    }

    public void isBackwardButtonPressed_false()
    {
        isBackwardButtonPressed = false;
    }

    private void CallRotateRoverLeft()
    {
        RoverEvents.RotateRoverLeft();
    }

    private void CallRotateRoverRight()
    {
        RoverEvents.RotateRoverRight();
    }

    private void CallMoveRoverForward()
    {
        RoverEvents.RoverForward();
    }

    private void CallMoveRoverBackward()
    {
        RoverEvents.RoverBackward();
    }
}

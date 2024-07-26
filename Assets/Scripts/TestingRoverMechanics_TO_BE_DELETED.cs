using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingRoverMechanics_TO_BE_DELETED : MonoBehaviour
{

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    [Range(-1f, 1f)]
    private float forceAmount = 0f;

    [SerializeField]
    private GameObject RoverParentChild;

    [SerializeField]
    private Vector3 forcePosition;

    [SerializeField]
    private Vector3 forwardDirection;

    private void Update()
    {
        MoveRoverForward();
        //MoveRoverForward1();
    }

    private void MoveRoverForward()
    {
        Vector3 forceVector = new Vector3(0, 0, forceAmount);

        rb.AddForce(forceVector);
    }
    private void MoveRoverForward1()
    {
        // Calculate the forward direction based on the rover's orientation.
        forwardDirection = RoverParentChild.transform.forward;

        // Calculate the force vector.
        Vector3 forceVector = forwardDirection * forceAmount;

        // Define the position at which to apply the force. This could be any point on the rover.
        // For example, to apply the force at the base of the rover, you might use the rover's position.
        forcePosition = RoverParentChild.transform.position;

        // Apply the force to the Rigidbody at the specified position.
        rb.AddForceAtPosition(forceVector, forcePosition);
    }

}

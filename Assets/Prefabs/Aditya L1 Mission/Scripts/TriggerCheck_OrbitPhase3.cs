using UnityEngine;

public class TriggerCheck_OrbitPhase3 : MonoBehaviour
{
    [SerializeField]
    private string specificTag = "AdityaL1"; // Use for specific tag check (optional)

    private void OnTriggerEnter(Collider other)
    {
        // Check if it collided with any object
        Debug.Log($"Collided with object: {other.gameObject.name}");

        // If you also want to check for a specific tag, you can do so:
        if (other.CompareTag(specificTag))
        {
            Debug.Log($"Collided with object having tag: {specificTag}");
            OnSpecificObjectEnter(other.gameObject);
        }
    }

    private void OnSpecificObjectEnter(GameObject enteredObject)
    {
        // Example behavior when colliding with the specific object
        Debug.Log($"Specific behavior for {enteredObject.name}.");
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ScaleCanvasRelationToCameraDistance : MonoBehaviour
{
    [Header("GameObjects to Scale")]
    [Tooltip("List of GameObjects that will scale based on the reference object's distance from the Main Camera.")]
    [SerializeField]
    private List<GameObject> objectsToScale = new List<GameObject>();

    [Header("Reference Object")]
    [Tooltip("The GameObject from which distance to Main Camera is measured.")]
    [SerializeField]
    private GameObject referenceObject;

    [Header("Scaling Settings")]
    [Tooltip("Minimum scale factor for the GameObjects.")]
    [SerializeField]
    private Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);

    [Tooltip("Maximum scale factor for the GameObjects.")]
    [SerializeField]
    private Vector3 maxScale = new Vector3(2f, 2f, 2f);

    [Tooltip("The reference distance at which objects are at their base scale.")]
    [SerializeField]
    private float referenceDistance = 10f;

    [Tooltip("The scaling factor determining how scale changes with distance.")]
    [SerializeField]
    private float scalingFactor = 1f;

    [Tooltip("Speed of scaling interpolation.")]
    [SerializeField]
    private float scalingSpeed = 5f;

    // Reference to the Main Camera
    private Camera mainCamera;

    // Store the original scale of each object
    private Dictionary<GameObject, Vector3> baseScales = new Dictionary<GameObject, Vector3>();

    private void Start()
    {
        // Find and assign the Main Camera
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Please ensure a camera is tagged as 'MainCamera'.");
        }

        // Validate objects to scale
        if (objectsToScale.Count == 0)
        {
            Debug.LogWarning("No GameObjects assigned to scale. Please add GameObjects to the 'Objects To Scale' list.");
        }

        // Validate referenceObject
        if (referenceObject == null)
        {
            Debug.LogError("Reference Object not assigned. Please assign a GameObject to the 'Reference Object' field.");
        }

        // Store the base scales
        foreach (GameObject obj in objectsToScale)
        {
            if (obj != null && !baseScales.ContainsKey(obj))
            {
                baseScales[obj] = obj.transform.localScale;
            }
            else if (obj == null)
            {
                Debug.LogWarning("One of the GameObjects in 'Objects To Scale' list is null and will be ignored.");
            }
        }
    }

    private void Update()
    {
        // Ensure all necessary references are assigned
        if (mainCamera == null || objectsToScale.Count == 0 || referenceObject == null)
            return;

        // Calculate distance between referenceObject and Main Camera
        float distance = Vector3.Distance(referenceObject.transform.position, mainCamera.transform.position);

        // Calculate scale multiplier based on distance
        // Objects farther than referenceDistance will be larger, closer will be smaller
        float scaleMultiplier = 1 + ((distance - referenceDistance) / referenceDistance) * scalingFactor;

        // Optional: Clamp scaleMultiplier to prevent extreme scaling
        // This ensures that scaling does not cause objects to become too small or too large
        // Adjust these clamps as needed based on your specific requirements
        scaleMultiplier = Mathf.Clamp(scaleMultiplier,
                                      minScale.x / baseScales[objectsToScale[0]].x,
                                      maxScale.x / baseScales[objectsToScale[0]].x);

        foreach (GameObject obj in objectsToScale)
        {
            if (obj == null)
                continue;

            // Calculate target scale based on original scale and scaleMultiplier
            Vector3 targetScale = baseScales[obj] * scaleMultiplier;

            // Clamp each axis individually to maintain limits
            targetScale.x = Mathf.Clamp(targetScale.x, minScale.x, maxScale.x);
            targetScale.y = Mathf.Clamp(targetScale.y, minScale.y, maxScale.y);
            targetScale.z = Mathf.Clamp(targetScale.z, minScale.z, maxScale.z);

            // Smoothly interpolate to the target scale
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, targetScale, Time.deltaTime * scalingSpeed);
        }
    }
}

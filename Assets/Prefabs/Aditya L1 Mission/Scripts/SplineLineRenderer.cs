using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(LineRenderer))]
public class SplineLineRenderer : MonoBehaviour
{
    public SplineContainer splineContainer; // Reference to the SplineContainer
    public int resolution = 100; // Number of points to draw along the spline
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (splineContainer != null)
        {
            DrawSplinePath();
        }
        else
        {
            Debug.LogWarning("SplineContainer reference is missing.");
        }
    }

    void DrawSplinePath()
    {
        lineRenderer.positionCount = resolution;

        for (int i = 0; i < resolution; i++)
        {
            float t = i / (float)(resolution - 1); // Normalize the t value between 0 and 1
            Vector3 position = splineContainer.EvaluatePosition(t); // Get the position on the spline at t
            lineRenderer.SetPosition(i, position); // Set the position for the LineRenderer
        }
    }
}

using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

public class MultipleSplineLineRenderer : MonoBehaviour
{
    public SplineContainer splineContainer; // Reference to the SplineContainer
    public Material lineMaterial;           // Material for the LineRenderer
    public float lineWidth = 0.1f;          // Width of the line

    void Start()
    {
        if (splineContainer != null)
        {
            DrawAllSplines();
        }
        else
        {
            Debug.LogWarning("SplineContainer reference is missing.");
        }
    }

    void DrawAllSplines()
    {
        IReadOnlyList<Spline> splines = splineContainer.Splines;

        if (splines == null || splines.Count == 0)
        {
            Debug.LogWarning("No splines found in the SplineContainer.");
            return;
        }

        for (int splineIndex = 0; splineIndex < splines.Count; splineIndex++)
        {
            Spline spline = splines[splineIndex];

            // Create a new GameObject for each spline
            GameObject splineGO = new GameObject("SplineLineRenderer_" + splineIndex);
            splineGO.transform.SetParent(this.transform);

            // Add a LineRenderer to the GameObject
            LineRenderer lineRenderer = splineGO.AddComponent<LineRenderer>();

            // Set LineRenderer properties
            lineRenderer.material = lineMaterial;
            lineRenderer.widthMultiplier = lineWidth;
            lineRenderer.useWorldSpace = true; // Use world space coordinates

            // Draw the spline using knots
            DrawSplinePath(spline, lineRenderer);
        }
    }

    void DrawSplinePath(Spline spline, LineRenderer lineRenderer)
    {
        int knotCount = spline.Count;
        lineRenderer.positionCount = knotCount;

        for (int i = 0; i < knotCount; i++)
        {
            // Get the knot at index i
            BezierKnot knot = spline[i];

            // Get the local position of the knot
            Vector3 localPosition = knot.Position;

            // Transform the local position to world position
            Vector3 worldPosition = splineContainer.transform.TransformPoint(localPosition);

            // Set the position in the LineRenderer
            lineRenderer.SetPosition(i, worldPosition);
        }
    }
}

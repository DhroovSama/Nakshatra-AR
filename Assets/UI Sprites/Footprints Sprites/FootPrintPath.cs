using UnityEngine;

public class FootPrintPath : MonoBehaviour
{
    [SerializeField]
    private GameObject footprintPrefab1;

    [SerializeField]
    private GameObject footprintPrefab2;

    [SerializeField]
    private Vector3[] pathPoints;

    [SerializeField]
    private Quaternion[] footprintRotations;

    [SerializeField]
    private bool isPortalDoorCrossed = false;
    public bool IsPortalDoorCrossed { get { return isPortalDoorCrossed; } set { isPortalDoorCrossed = value; } }

    private bool footprintsSpawned = false;

    [SerializeField]
    private int pathPointsIndex = 0;

    private GameObject currentFootprint;

    private bool spawnPrefab1Next = true;

    private bool isLastFootpathReached = false;

    private void Update()
    {
        if (!isLastFootpathReached)
        {
            if (isPortalDoorCrossed && !footprintsSpawned)
            {
                Debug.Log("trueeeee");
                SpawnFootprints();
                footprintsSpawned = true;
            }
        }
    }

    public void SpawnFootprints()
    {
        if (pathPoints.Length > 0)
        {
            if (currentFootprint != null)
            {
                Destroy(currentFootprint);
            }

            GameObject prefabToSpawn = spawnPrefab1Next ? footprintPrefab1 : footprintPrefab2;
            Quaternion rotation = footprintRotations[pathPointsIndex];
            currentFootprint = Instantiate(prefabToSpawn, pathPoints[pathPointsIndex], rotation);

            spawnPrefab1Next = !spawnPrefab1Next;

            if (pathPointsIndex == pathPoints.Length - 1)
            {
                Debug.Log("Reached to the path");
                isLastFootpathReached = true;
            }
            else
            {
                pathPointsIndex = (pathPointsIndex + 1) % pathPoints.Length;
            }
        }
        else
        {
            Debug.Log("No path points defined.");
        }
    }

    public Vector3[] GetPathPoints()
    {
        return pathPoints;
    }

    public int GetPathPointsIndex()
    {
        return pathPointsIndex;
    }
}

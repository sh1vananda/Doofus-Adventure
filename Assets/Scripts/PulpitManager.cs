using UnityEngine;
using System.Collections.Generic;

public class PulpitManager : MonoBehaviour
{
    public GameObject pulpitPrefab; // Assign your Pulpit prefab in the Inspector
    public int maxPulpits = 2;
    private List<GameObject> pulpits = new List<GameObject>();
    private Vector3 lastRemovedPulpitPosition = Vector3.positiveInfinity; // Initialize with a non-reachable position

    void Start()
    {
        // Create the initial Pulpit at the starting position
        Vector3 initialPosition = Vector3.zero;
        SpawnInitialPulpit(initialPosition);

        // Start spawning new Pulpits after a delay of 2 seconds, then every 3 seconds
        InvokeRepeating("SpawnNewPulpit", 2f, 3f);
    }

    private void SpawnInitialPulpit(Vector3 position)
    {
        GameObject initialPulpit = Instantiate(pulpitPrefab, position, Quaternion.identity);
        pulpits.Add(initialPulpit);
    }

    public void SpawnNewPulpit()
    {
        // Check if the maximum number of Pulpits is reached
        if (pulpits.Count >= maxPulpits)
        {
            DestroyOldestPulpit(); // Destroy the oldest Pulpit
        }

        Vector3 newPosition;
        if (pulpits.Count > 0)
        {
            do
            {
                newPosition = GenerateAdjacentPosition(pulpits[pulpits.Count - 1].transform.position);
            } while (newPosition == lastRemovedPulpitPosition); // Ensure new Pulpit doesn't spawn at the last removed position
        }
        else
        {
            newPosition = Vector3.zero; // Default position if no pulpits exist
        }

        GameObject newPulpit = Instantiate(pulpitPrefab, newPosition, Quaternion.identity);
        pulpits.Add(newPulpit);

        // Optionally destroy the new Pulpit after a certain time
        Destroy(newPulpit, 5.0f); // Adjust the time as needed
    }

    private Vector3 GenerateAdjacentPosition(Vector3 previousPosition)
    {
        float offset = 9.0f; // Assuming Pulpit size is 9
        List<Vector3> directions = new List<Vector3>
        {
            new Vector3(offset, 0, 0),  // Right
            new Vector3(-offset, 0, 0), // Left
            new Vector3(0, 0, offset),  // Forward
            new Vector3(0, 0, -offset)  // Backward
        };

        // Generate potential new positions
        List<Vector3> potentialPositions = new List<Vector3>();
        foreach (var direction in directions)
        {
            Vector3 potentialPosition = previousPosition + direction;
            if (potentialPosition != lastRemovedPulpitPosition)
            {
                potentialPositions.Add(potentialPosition);
            }
        }

        // Choose a random available position
        return potentialPositions[Random.Range(0, potentialPositions.Count)];
    }

    private void DestroyOldestPulpit()
    {
        if (pulpits.Count > 0)
        {
            GameObject pulpitToDestroy = pulpits[0];
            lastRemovedPulpitPosition = pulpitToDestroy.transform.position; // Update last removed position before destroying
            pulpits.RemoveAt(0);
            Destroy(pulpitToDestroy);
        }
    }
}
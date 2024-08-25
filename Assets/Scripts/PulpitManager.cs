using UnityEngine;
using System.Collections.Generic;

public class PulpitManager : MonoBehaviour
{
    public GameObject pulpitPrefab; // Assign your Pulpit prefab in the Inspector
    public int maxPulpits = 2; // Maximum number of Pulpits allowed at a time
    private List<GameObject> pulpits = new List<GameObject>(); // List to hold active Pulpits

    void Start()
    {
        // Create the initial Pulpit at starting position
        Vector3 initialPosition = new Vector3(0, 0, 0);
        SpawnInitialPulpit(initialPosition);

        // Start spawning new pulpits after a delay of 2 seconds, then every 3 seconds
        InvokeRepeating("SpawnNewPulpit", 2.0f, 3.0f);
    }

    // Method to spawn the initial Pulpit
    private void SpawnInitialPulpit(Vector3 position)
    {
        GameObject initialPulpit = Instantiate(pulpitPrefab, position, Quaternion.identity);
        pulpits.Add(initialPulpit);
    }

    // Method to spawn a new Pulpit
    public void SpawnNewPulpit()
    {
        // Check if the maximum number of Pulpits is reached
        if (pulpits.Count >= maxPulpits)
        {
            DestroyOldestPulpit(); // Destroy the oldest Pulpit
        }

        Vector3 newPosition = GenerateAdjacentPosition(pulpits[pulpits.Count - 1].transform.position);
        GameObject newPulpit = Instantiate(pulpitPrefab, newPosition, Quaternion.identity);
        pulpits.Add(newPulpit);

        // Optionally destroy the new Pulpit after a period
        Destroy(newPulpit, 5.0f); // Destroy after 5 seconds, adjust as needed
    }

    // Method to generate a new random adjacent position
    private Vector3 GenerateAdjacentPosition(Vector3 previousPosition)
    {
        float offset = 9.0f; // Assuming Pulpit size is 9
        Vector3[] directions = new Vector3[]
        {
            new Vector3(offset, 0, 0),  // Right
            new Vector3(-offset, 0, 0), // Left
            new Vector3(0, 0, offset),  // Forward
            new Vector3(0, 0, -offset)  // Backward
        };

        // Choose a random direction
        Vector3 direction = directions[Random.Range(0, directions.Length)];
        return previousPosition + direction;
    }

    // Method to destroy the oldest Pulpit
    private void DestroyOldestPulpit()
    {
        if (pulpits.Count > 0)
        {
            GameObject pulpitToDestroy = pulpits[0];
            pulpits.RemoveAt(0);
            Destroy(pulpitToDestroy);
        }
    }
}
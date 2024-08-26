using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PulpitManager : MonoBehaviour
{
    public GameObject pulpitPrefab; // Assign your Pulpit prefab in the Inspector
    public float spawnInterval = 3.0f; // Time interval between spawns
    public float scaleDuration = 0.3f; // Duration of the scaling animation
    private List<GameObject> pulpits = new List<GameObject>(); // List to hold active Pulpits

    void Start()
    {
        // Spawn the initial Pulpit at the starting position
        SpawnPulpit(Vector3.zero);

        // Start the coroutine to spawn new Pulpits
        InvokeRepeating(nameof(SpawnNewPulpit), spawnInterval, spawnInterval);
    }

    private void SpawnNewPulpit()
    {
        // Determine the position for the new Pulpit
        Vector3 newPosition = GenerateNewPosition();

        // Spawn the new Pulpit with an initial small scale
        GameObject newPulpit = Instantiate(pulpitPrefab, newPosition, Quaternion.identity);
        newPulpit.transform.localScale = Vector3.zero; // Start at zero scale
        pulpits.Add(newPulpit);

        // Start the scaling animation to grow the pulpit to a 9x1x9 slab
        StartCoroutine(ScalePulpit(newPulpit, Vector3.zero, new Vector3(9, 1, 9), scaleDuration));

        // Destroy the oldest Pulpit 1.5 seconds after the new one spawns if there are more than 1 pulpit
        if (pulpits.Count > 1)
        {
            Destroy(pulpits[0], 1.5f);
            pulpits.RemoveAt(0);
        }

        // Set the countdown time on the PulpitTimer if it exists
        PulpitTimer timer = newPulpit.GetComponent<PulpitTimer>();
        if (timer != null)
        {
            timer.SetCountdownTime(spawnInterval + 1.5f); // Set the timer to match the destruction time
        }
    }

    private void SpawnPulpit(Vector3 position)
    {
        GameObject newPulpit = Instantiate(pulpitPrefab, position, Quaternion.identity);
        pulpits.Add(newPulpit);
    }

    private Vector3 GenerateNewPosition()
    {
        float offset = 9.0f; // Size of the pulpit slab (9x9)
        Vector3[] directions = new Vector3[]
        {
            new Vector3(offset, 0, 0),  // Right
            new Vector3(-offset, 0, 0), // Left
            new Vector3(0, 0, offset),  // Forward
            new Vector3(0, 0, -offset)  // Backward
        };

        // Choose a random direction for the new Pulpit
        Vector3 lastPulpitPosition = pulpits.Count > 0 ? pulpits[pulpits.Count - 1].transform.position : Vector3.zero;
        Vector3 direction = directions[Random.Range(0, directions.Length)];
        return lastPulpitPosition + direction;
    }

    private IEnumerator ScalePulpit(GameObject pulpit, Vector3 fromScale, Vector3 toScale, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            pulpit.transform.localScale = Vector3.Lerp(fromScale, toScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        pulpit.transform.localScale = toScale; // Ensure it reaches the final size
    }
}
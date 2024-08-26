using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PulpitManager : MonoBehaviour
{
    public GameObject pulpitPrefab; // Assign your Pulpit prefab in the Inspector
    public float initialPulpitTime = 3.0f; // Time before the first pulpit spawns another
    public float scaleDuration = 0.3f; // Duration of the scaling animation
    public float destroyDelay = 1.5f; // Delay before the old pulpit disappears
    private List<GameObject> pulpits = new List<GameObject>(); // List to hold active Pulpits

    private float minPulpitDestroyTime;
    private float maxPulpitDestroyTime;
    private float pulpitSpawnTime;

    void Start()
    {
        LoadPulpitDataFromJson();

        // Spawn the initial Pulpit at the starting position
        SpawnPulpit(Vector3.zero);

        // Start the coroutine to manage the initial pulpit's lifecycle
        StartCoroutine(ManageInitialPulpit());
    }

    private void LoadPulpitDataFromJson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "parameters.JSON");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            GameData gameData = JsonUtility.FromJson<GameData>(jsonData);

            if (gameData != null && gameData.pulpit_data != null)
            {
                minPulpitDestroyTime = gameData.pulpit_data.min_pulpit_destroy_time;
                maxPulpitDestroyTime = gameData.pulpit_data.max_pulpit_destroy_time;
                pulpitSpawnTime = gameData.pulpit_data.pulpit_spawn_time;
            }
            else
            {
                Debug.LogError("Pulpit data is missing in JSON.");
            }
        }
        else
        {
            Debug.LogError("JSON file not found.");
        }
    }

    private IEnumerator ManageInitialPulpit()
    {
        // Wait for the initial pulpit time
        yield return new WaitForSeconds(initialPulpitTime);

        // Create a new pulpit adjacent to the initial one
        Vector3 newPosition = GenerateNewPosition();
        CreateNewPulpit(newPosition);
    }

    private void CreateNewPulpit(Vector3 position)
    {
        // Start coroutine to destroy the current pulpit after a delay
        if (pulpits.Count > 0)
        {
            StartCoroutine(DestroyPulpitAfterDelay(pulpits[0], destroyDelay));
            pulpits.RemoveAt(0);
        }

        // Instantiate a new pulpit at the specified position
        GameObject newPulpit = Instantiate(pulpitPrefab, position, Quaternion.identity);
        newPulpit.transform.localScale = Vector3.zero; // Start at zero scale
        pulpits.Add(newPulpit);

        // Start the scaling animation to grow the pulpit to a 9x1x9 slab
        StartCoroutine(ScalePulpit(newPulpit, Vector3.zero, new Vector3(9, 1, 9), scaleDuration));

        // Calculate a random lifetime for the pulpit
        float lifetime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);

        // Start the coroutine to manage subsequent pulpits
        StartCoroutine(ManagePulpitLifecycle(newPulpit, lifetime));

        // Set the countdown time on the PulpitTimer if it exists
        PulpitTimer timer = newPulpit.GetComponent<PulpitTimer>();
        if (timer != null)
        {
            // Set the timer to match the pulpit's active lifetime
            timer.SetCountdownTime(lifetime);
        }
    }

    private IEnumerator ManagePulpitLifecycle(GameObject pulpit, float lifetime)
    {
        // Calculate the time to spawn the next pulpit before the current one expires
        float spawnBeforeExpire = 1.0f; // Time before the current pulpit's lifetime ends to spawn a new one
        float timeToNextSpawn = lifetime - spawnBeforeExpire;

        // Wait for the calculated time to spawn the next pulpit
        yield return new WaitForSeconds(timeToNextSpawn);

        // Create a new pulpit adjacent to the current one
        Vector3 newPosition = GenerateNewPosition();
        CreateNewPulpit(newPosition);

        // Wait for the remaining lifetime before destroying the current pulpit
        yield return new WaitForSeconds(spawnBeforeExpire);
    }

    private IEnumerator DestroyPulpitAfterDelay(GameObject pulpit, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the pulpit
        Destroy(pulpit);
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
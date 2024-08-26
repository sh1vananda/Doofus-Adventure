using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public float speed;
}

[System.Serializable]
public class PulpitData
{
    public float min_pulpit_destroy_time;
    public float max_pulpit_destroy_time;
    public float pulpit_spawn_time;
}

[System.Serializable]
public class GameParameters
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}

public class JSONReader : MonoBehaviour
{
    public GameParameters gameParameters;

    void Awake()
    {
        LoadJSON();
    }

    private void LoadJSON()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "parameters.JSON");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameParameters = JsonUtility.FromJson<GameParameters>(dataAsJson);
        }
        else
        {
            Debug.LogError("Cannot find JSON file!");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string nextSpawnID = "Default";

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void SetNextSpawn(string spawnID)
    {
        nextSpawnID = spawnID;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UnityEngine.Debug.Log($"Scene Load: {scene.name}, Mode: {mode}");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CameraFollow cam = FindObjectOfType<CameraFollow>();

        if (player != null && cam != null)
            cam.target = player.transform;

        SpawnPoint[] points = GameObject.FindObjectsOfType<SpawnPoint>();
        foreach (var point in points)
        {
            if (point.spawnID == SpawnManager.nextSpawnID)
            {
                player.transform.position = point.transform.position;
                break;
            }
        }
    }

}

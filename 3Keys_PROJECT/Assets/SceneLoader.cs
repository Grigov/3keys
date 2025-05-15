using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public string targetSpawnID;
    public int healthHeal;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadTargetScene);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadTargetScene()
    {
        SpawnManager.SetNextSpawn(targetSpawnID);
        SceneManager.LoadScene(sceneName);
        DataPlayer.health += healthHeal;
    }

    public void LoadTargetSceneWOPlayer()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UnityEngine.Debug.Log($"Scene Load: {scene}, Load scene mode: {mode}");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CameraFollow cam = FindObjectOfType<CameraFollow>();

        if (player != null && cam != null)
        {
            cam.target = player.transform;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

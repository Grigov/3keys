using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public string targetSpawnID;
    public int healthHeal;
    public static SceneLoader Instance;

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

    public void LoadTargetSceneDOL()
    {
        SpawnManager.SetNextSpawn(targetSpawnID);
        SceneManager.LoadScene(sceneName);
        DestroyAllDontDestroyObjects();
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

    private void DestroyAllDontDestroyObjects()
    {
        GameObject temp = new GameObject("TempSceneLoader");
        DontDestroyOnLoad(temp);

        Scene dontDestroyScene = temp.scene;
        Destroy(temp);

        GameObject[] rootObjects = dontDestroyScene.GetRootGameObjects();

        foreach (GameObject go in rootObjects)
        {
            Destroy(go);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

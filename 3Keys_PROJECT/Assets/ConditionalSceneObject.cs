using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionalSceneObject : MonoBehaviour
{
    [Tooltip("Сцена, в которой объект должен быть активен")]
    public string targetSceneName = "Forest";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetSceneName)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        // На старте проверим, где мы находимся, чтобы правильно включиться/выключиться
        if (SceneManager.GetActiveScene().name != targetSceneName)
        {
            gameObject.SetActive(false);
        }
    }
}

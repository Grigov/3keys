using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDataManager : MonoBehaviour
{
    public static SceneDataManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
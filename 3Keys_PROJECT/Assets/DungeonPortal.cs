using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DungeonPortal : MonoBehaviour
{
    public string nextSceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(nextSceneName);
        }
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);

        while (!asyncLoad.isDone)
        {
            // Обновление прогресс-бара
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log($"Loading: {progress * 100}%");
            yield return null;
        }
    }
}
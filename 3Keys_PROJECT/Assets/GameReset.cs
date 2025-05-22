using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour
{
    public string startSceneName = "GameMenu";

    public void RestartGame()
    {
        if (PlayerHealth.Instance != null)
        {
            PlayerHealth.Instance.ResetHealth();
        }

        PlayerPrefs.DeleteAll();
        ClearStatics();
        DataPlayer.ResetData();
        SceneManager.LoadScene(startSceneName);
    }

    private void ClearStatics()
    {
        SpawnManager.nextSpawnID = "Load";

        SceneStateManager.Instance?.ResetState();
    }
}

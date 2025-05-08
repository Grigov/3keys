using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadTargetScene);
    }

    public void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            //Debug.LogError("Scene name is not set!");
        }
    }
}
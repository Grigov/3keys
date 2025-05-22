using System.Collections.Generic;
using UnityEngine;

public class SceneStateManager : MonoBehaviour
{
    public static SceneStateManager Instance;
    private HashSet<string> destroyedObjects = new HashSet<string>();

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

    public void ResetState()
    {
        destroyedObjects.Clear();
    }

    public void MarkDestroyed(string id)
    {
        destroyedObjects.Add(id);
    }

    public bool IsDestroyed(string id)
    {
        return destroyedObjects.Contains(id);
    }
}

using UnityEngine;

public class SceneObject : MonoBehaviour
{
    public string uniqueID;

    void Start()
    {
        if (SceneStateManager.Instance != null && SceneStateManager.Instance.IsDestroyed(uniqueID))
        {
            Destroy(gameObject); // если объект уже уничтожался ранее
        }
    }

    public void MarkAsDestroyed()
    {
        SceneStateManager.Instance?.MarkDestroyed(uniqueID);
    }
}

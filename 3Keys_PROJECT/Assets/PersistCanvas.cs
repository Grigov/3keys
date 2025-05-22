using UnityEngine;

public class PersistCanvas : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsOfType<PersistCanvas>().Length > 1)
        {
            Destroy(gameObject); // Удаляем дубликат
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}

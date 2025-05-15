using UnityEngine.UI;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class InteractionStairs : MonoBehaviour
{
    public GameObject stairs;
    public GameObject fElement;
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            stairs.SetActive(!stairs.activeSelf);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            stairs.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            fElement.SetActive(false);
        }
    }
}
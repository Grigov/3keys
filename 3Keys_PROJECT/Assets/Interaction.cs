using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject fElement;
    public GameObject enter0;
    public GameObject enter1;
    public GameObject enter2;
    private bool isPlayerNear = false;
    private bool isShopUINear = false;
    private bool isEnterNear = false;
    private bool isEnter1Near = false;
    private bool isEnter2Near = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && isShopUINear)
        {
            shopUI.SetActive(!shopUI.activeSelf);
        }

        if(isPlayerNear && Input.GetKeyDown(KeyCode.F) && isEnterNear)
        {
            enter0.SetActive(!enter0.activeSelf);
        }

        if(isPlayerNear && Input.GetKeyDown(KeyCode.F) && isEnter1Near)
        {
            enter1.SetActive(!enter1.activeSelf);
        }

        if(isPlayerNear && Input.GetKeyDown(KeyCode.F) && isEnter2Near)
        {
            enter2.SetActive(!enter2.activeSelf);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shop") || other.CompareTag("Enter") || other.CompareTag("Enter1") || other.CompareTag("Enter2"))
        {
            isPlayerNear = true;
            fElement.SetActive(true);
            if (other.CompareTag("Shop"))
            {
                isShopUINear = true;
            }
            else if (other.CompareTag("Enter"))
            {
                isEnterNear = true;
            }
            else if (other.CompareTag("Enter1"))
            {
                isEnter1Near = true;
            }
            else if (other.CompareTag("Enter2"))
            {
                isEnter2Near = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Shop") || other.CompareTag("Enter") || other.CompareTag("Enter1") || other.CompareTag("Enter2"))
        {
            isPlayerNear = false;
            fElement.SetActive(false);
            if (other.CompareTag("Shop"))
            {
                shopUI.SetActive(false);
                isShopUINear = false;
            }
            else if (other.CompareTag("Enter"))
            {
                enter0.SetActive(false);
                isEnterNear = false;
            }
            else if (other.CompareTag("Enter1"))
            {
                enter1.SetActive(false);
                isEnter1Near = false;
            }
            else if (other.CompareTag("Enter2"))
            {
                enter2.SetActive(false);
                isEnter2Near = false;
            }
        }
    }
}
